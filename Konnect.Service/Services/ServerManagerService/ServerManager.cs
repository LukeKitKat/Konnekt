using Konnect.Service.Constants;
using Konnect.Service.DatabaseManager;
using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.Models;
using Konnect.Service.Services;
using Konnect.Service.Services._Internal;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace Konnect.Service.Services.ServerManagerService
{
    public class ServerManager(IDbContextFactory<KonnektContext> dbContextFactory, ILogger<ServerManager> logger)
        : ServiceBase<ServerManager>(dbContextFactory, logger)
    {
        internal CommonFileHandler FileHandler { get; set; } = new();

        public async Task<ServiceResponse<Server?>> GetServerByIdAsync(string serverId)
        {
            return await ExecAsync<Server?>(async (db, resp) =>
            {
                return await db.Servers.AsNoTracking()
                                       .AsSplitQuery()
                                       .Include(x => x.ServerChannels)
                                       .Include(x => x.ServerUsers)
                                            .ThenInclude(x => x.User)
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

        public async Task<ServiceResponse<ServerChannel>> GetChannelContentAsync(string channelId)
        {
            return await ExecAsync<ServerChannel>(async (db, resp) =>
            {
                return await db.ServerChannels.AsNoTracking()
                                              .Include(x => x.ServerMessages)
                                                .ThenInclude(x => x.Sender)
                                              .Include(x => x.ServerMessages)
                                                .ThenInclude(x => x.ServerMessageFiles)
                                              .FirstOrDefaultAsync(x => x.Id == channelId);
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

        public async Task<ServiceResponse> AddMessageToChannelAsync(string channelId, string senderId, string messageBody, IReadOnlyList<IBrowserFile> messageFiles)
        {
            return await ExecAsync(async (db, resp) =>
            {
                ServerMessage message = new()
                {
                    SenderId = senderId,
                    ChannelId = channelId,
                    MessageBody = messageBody,
                    TimeSent = DateTime.UtcNow,
                };

                await db.ServerMessages.AddAsync(message);

                foreach (var browserFile in messageFiles)
                {
                    if (MimeTypes.CombinedImageMimes.All(x => x.Value != browserFile.ContentType))
                        throw new Exception("Mime type of the given file is not allowed.");

                    byte[] bytes = await FileHandler.GetFileBytesAsync(browserFile);

                    await db.ServerMessagesFiles.AddAsync(new()
                    {
                        FileContent = bytes,
                        FileName = browserFile.Name,
                        FileType = browserFile.ContentType,
                        ServerMessageId = message.Id,
                        ServerMessage = message,
                    });
                }

                await db.SaveChangesAsync();
            });
        }

        public async Task<ServiceResponse> DeleteMessageFromChannelAsync(string messageId)
        {
            return await ExecAsync(async (db, resp) =>
            {
                var msg = await db.ServerMessages.AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.Id == messageId);

                if (msg == null)
                    throw new Exception($"Could not find a message with the Id: {messageId}");

                db.Remove(msg);
                await db.SaveChangesAsync();
            });
        }

        public async Task<ServiceResponse> EditMessageInChannelAsync(ServerMessage messageModel)
        {
            return await ExecAsync(async (db, resp) =>
            {
                messageModel.TimeEdited = DateTime.UtcNow;

                db.Update(messageModel);
                await db.SaveChangesAsync();
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
