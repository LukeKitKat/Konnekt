using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.DatabaseManager;
using Konnect.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Konnect.Service.Constants;
using Konnect.Service.Services._Internal;

namespace Konnect.Service.Services.UserManagerHelperService
{
    public class UserManagerHelper(IDbContextFactory<KonnektContext> dbContextFactory, ILogger<UserManagerHelper> logger)
        : ServiceBase<UserManagerHelper>(dbContextFactory, logger)
    {
        internal CommonFileHandler FileHandler { get; set; } = new();

        public async Task<ServiceResponse<User>> UpdateDisplayName(User user, string inputDisplayName)
        {
            return await ExecAsync<User>(async (db, resp) =>
            {
                if (!await db.Users.AnyAsync(x => x.DisplayName == inputDisplayName))
                {
                    user.DisplayName = inputDisplayName;
                }
                else
                {
                    throw new Exception("Username already exists");
                }

                db.Update(user);
                await db.SaveChangesAsync();

                return user;
            });
        }

        public async Task<ServiceResponse> UpdateUserProfileImage(User user, IBrowserFile browserFile)
        {
            return await ExecAsync(async (db, resp) =>
            {
                if (MimeTypes.CombinedImageMimes.All(x => x.Value != browserFile.ContentType))
                    throw new Exception("Mime type of the given file is not allowed.");

                byte[] bytes = await FileHandler.GetFileBytesAsync(browserFile);
                user.ProfilePicture = bytes;

                db.Update(user);
                await db.SaveChangesAsync();
            });
        }
    }
}
