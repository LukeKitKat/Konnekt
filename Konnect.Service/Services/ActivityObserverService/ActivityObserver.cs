using Konnect.Service.DatabaseManager;
using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.Models;
using Konnect.Service.ServerNavigator;
using Konnect.Service.Services;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Konnect.Service.ActivityObserver
{
    public class ActivityObserver()
    {
        private protected ConcurrentDictionary<string, (User User, DateTime Expiry)> ActiveUsers { get; set; } = new(new Dictionary<string, (User User, DateTime Expiry)>());
        public Action SystemActivityFuncs { get; set; } = default!;

        public async Task<bool> TryAddOrRemoveUserActivity(bool registering, User? user)
        {
            bool success;
            if (user is null)
                return false;

            if (registering)
            {
                ActiveUsers.AddOrUpdate(user.Id, (user, DateTime.UtcNow.AddMinutes(1)), (key, oldValue) => oldValue = (user, DateTime.UtcNow.AddMinutes(1)));
                success = true;
            }
            else
            { 
                success = ActiveUsers.Remove(user.Id, out _);
            }

            await NotifySystemActivity();
            return success;
        }

        public int CountActiveUsers()
            => ActiveUsers.Count;

        public async Task NotifySystemActivity()
        {
            if (SystemActivityFuncs is not null)
                SystemActivityFuncs.Invoke();

            await Task.Delay(1);
        }
    }
}
