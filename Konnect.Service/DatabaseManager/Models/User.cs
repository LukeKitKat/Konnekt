using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Konnect.Service.DatabaseManager.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {

        #region Relationships
        public virtual ICollection<ServerUser> ServerUsers { get; set; } = [];
        #endregion
    }
}