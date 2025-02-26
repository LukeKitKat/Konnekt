using Konnect.Service.DatabaseManager.Models;
using Konnekt.Client.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Konnect.Service.DatabaseManager
{
    public class KonnektContext(DbContextOptions<KonnektContext> options) : IdentityDbContext<User>(options)
    {
    }
}
