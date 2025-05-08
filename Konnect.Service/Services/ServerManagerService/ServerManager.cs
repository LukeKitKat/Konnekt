using Konnect.Service.DatabaseManager;
using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.Models;
using Konnect.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Konnect.Service.ServerNavigator
{
    public class ServerManager(IDbContextFactory<KonnektContext> dbContextFactory, ILogger<ServerManager> logger) : ServiceBase<ServerManager>(dbContextFactory, logger)
    {
        public async Task<ServiceResponse<Server?>> GetServerByIdAsync(string serverId)
        {
            return await ExecAsync<Server?>(async (db, resp) =>
            {
                return await db.Servers.AsNoTracking()
                                       .Include(x => x.ServerChannels)
                                       .FirstOrDefaultAsync(x => x.Id == serverId);
            });
        }

        public async Task<ServiceResponse<List<Server>>> GetUserServersAsync(User? user)
        {
            return await ExecAsync<List<Server>>(async (db, resp) =>
            {
                if (user is null)
                    throw new Exception("User was null");

                var servers = await db.ServerUsers.AsNoTracking()
                                           .Include(x => x.Server)
                                           .Where(x => x.UserId == user.Id)
                                           .OrderBy(x => x.ServerOrder)
                                           .Select(x => x.Server)
                                           .ToListAsync();

                if (servers.Any(x => x == null))
                    throw new Exception("One or more servers within the list were null");

                return servers!;
            });
        }

        public async Task<ServiceResponse<List<ServerChannel>>> GetServerChannelsAsync(Server server)
        {
            return await ExecAsync<List<ServerChannel>>(async (db, resp) =>
            {
                return await db.ServerChannels.AsNoTracking()
                                              .Where(x => x.ServerId == server.Id)
                                              .ToListAsync();
            });
        }

        public async Task<ServiceResponse> CreateNewServerChannelAsync(Server server, ServerChannel channel)
        {
            return await ExecAsync(async (db, resp) =>
            {
                ServerChannel model = new()
                {
                    ChannelName = channel.ChannelName,
                    ChannelOrder = server.ServerChannels.Count == 0 ? 0 : server.ServerChannels.Max(x => x.ChannelOrder) + 1,
                    ServerId = server.Id,
                };

                await db.ServerChannels.AddAsync(model);
                await db.SaveChangesAsync();
            });
        }

        public async Task<ServiceResponse<Server?>> CreateNewServerAsync(string? serverName, User? user)
        {
            return await ExecAsync<Server?>(async (db, resp) =>
            {
                if (user is null)
                    throw new Exception("User was null");

                var generatedJoinCode = await GenerateServerInviteAsync();
                if (string.IsNullOrEmpty(generatedJoinCode))
                    throw new Exception($"{nameof(generatedJoinCode)} was null");

                Server server = new()
                {
                    ServerName = serverName,
                    OwnerId = user.Id,
                    ServerJoinCodes = [new ServerJoinCode()
                    {
                        JoinCode = generatedJoinCode,
                    }],
                    ServerUsers = [new ServerUser {
                        UserId = user.Id,
                        ServerOrder = user.ServerUsers.Count == 0 ? 0 : user.ServerUsers.Max(x => x.ServerOrder) + 1
                    }]
                };

                await db.AddAsync(server);
                await db.SaveChangesAsync();

                return server;
            });
        }

        public async Task<ServiceResponse<Server?>> AddUserToServerAsync(string? joinCode, User? user)
        {
            return await ExecAsync<Server?>(async (db, resp) =>
            {
                if (user is null)
                    throw new Exception("User was null");

                if (string.IsNullOrEmpty(joinCode))
                    throw new Exception($"{nameof(joinCode)} was null");

                var matches = db.ServerJoinCodes.AsNoTracking()
                                                .Include(x => x.Server)
                                                .Where(x => x.JoinCode == joinCode);
                if (matches.Any())
                {
                    var server = matches.First().Server!;

                    db.ServerUsers.Add(new()
                    {
                        ServerId = server.Id,
                        UserId = user.Id,
                        ServerOrder = user.ServerUsers.Count == 0 ? 0 : user.ServerUsers.Max(x => x.ServerOrder) + 1
                    });

                    await db.SaveChangesAsync();
                    return server;
                }
                else
                {
                    return null;
                }
            });
        }

        private async Task<string?> GenerateServerInviteAsync()
        {
            return (await ExecAsync<string>(async (db, resp) =>
            {
                var generatedLink = new string(Guid.NewGuid().ToString().Take(8).ToArray());
                if (generatedLink is null)
                    throw new Exception("Link generated by system was null at creation");

                if (generatedLink is not null && !db.ServerJoinCodes.Any(x => x.JoinCode.Equals(generatedLink)))
                    return generatedLink;
                else
                    return await GenerateServerInviteAsync();
            })).Result;
        }
    }
}
