using Konnect.Service.DatabaseManager;
using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Konnect.Service.ServerNavigator
{
    public class ServerNavigator : ServiceBase
    {
        public ServerNavigator(KonnektContext dbContext)
            : base(dbContext) { }

    }
}
