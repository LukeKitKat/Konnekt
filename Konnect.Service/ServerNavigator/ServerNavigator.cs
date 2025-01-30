using Konnect.Service.BaseServices;
using Konnect.Service.DatabaseManager.Models;
using Microsoft.EntityFrameworkCore;

namespace Konnect.Service.ServerNavigator
{
    public class ServerNavigator : ServiceBase
    {
        private CryptoManager CryptoManager { get; set; } = default!;

        public ServerNavigator(CryptoManager cryptoManager)
        {
            CryptoManager = cryptoManager;
        }

        public async Task<ServiceResponse<List<User>>> ReadServersAsync()
        {
            return await ExecAsync<List<User>>(async (db, resp) =>
            {
                return await db.Users.ToListAsync();
            });
        }

        public async Task<ServiceResponse> AddToServersAsync(string name)
        {
            return await ExecAsync(async (db, resp) =>
            {
                var loginServiceResult = CryptoManager.ProcessHash("Test Password");

                User user = new()
                {
                    AccountName = name,
                    DisplayName = name,

                    PasswordHash = loginServiceResult.ProcessedHash,
                    PasswordSalt = loginServiceResult.Salt,
                    LastLogin = null,
                    LastLoginLocation = null, 
                };

                await db.AddAsync(user);
                await db.SaveChangesAsync();
            });
        }
    }
}
