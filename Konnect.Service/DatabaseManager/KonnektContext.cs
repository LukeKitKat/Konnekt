using Konnect.Service.DatabaseManager.Models;
using Konnekt.Client.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Konnect.Service.DatabaseManager
{
    public class KonnektContext(DbContextOptions<KonnektContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<ServerJoinCode> ServerJoinCodes { get; set; }
        public DbSet<ServerUser> ServerUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var hasher = new PasswordHasher<User>();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "743da3b6-e3a4-40fb-ae3a-6773b103ee1a",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole()
                {
                    Id = "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });
        }
    }
}
