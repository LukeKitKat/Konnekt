using Konnect.Service.BaseServices;
using Konnect.Service.DatabaseManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnect.Service.UserManager
{
    public class UserManager : ServiceBase
    {
        private CryptoManager CryptoManager { get; set; } = default!;

        public UserManager(CryptoManager cryptoManager)
        {
            CryptoManager = cryptoManager;
        }

        public async Task<ServiceResponse> RegisterNewUserAsync(User user, string password)
        {
            return await ExecAsync(async (db, resp) =>
            {
                var loginServiceResult = CryptoManager.ProcessHash(password);

                user.DisplayName = user.AccountName;
                user.PasswordHash = loginServiceResult.ProcessedHash;
                user.PasswordSalt = loginServiceResult.Salt;

                await db.AddAsync(user);
                await db.SaveChangesAsync();
            });
        }

        public async Task<ServiceResponse<bool>> ValidateUserLoginAsync(string username, string attemptedPassword)
        {
            return await ExecAsync<bool>(async (db, resp) =>
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.AccountName == username);

                if (user is not null)
                    return CryptoManager.ValidateHash(attemptedPassword, user.PasswordHash, user.PasswordSalt);

                return false;
            });
        }
    }
}
