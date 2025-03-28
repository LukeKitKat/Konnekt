using Konnect.Service.DatabaseManager;
using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Konnect.Service.ServerNavigator
{
    public class ServerManager : ServiceBase
    {
        public ServerManager(KonnektContext dbContext)
            : base(dbContext) { }


        public async Task<ServiceResponse> TestServiceAsync()
        {
            return await ExecAsync(async (db, resp) =>
            {
                return;
            });
        }
    }
}
