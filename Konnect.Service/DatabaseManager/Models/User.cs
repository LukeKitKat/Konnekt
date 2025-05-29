using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Konnect.Service.DatabaseManager.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        [Required]
        [Column(TypeName = "nvarchar(64)")]
        public string? DisplayName { get; set; }

        [Column(TypeName = "varbinary(MAX)")]
        public byte[]? ProfilePicture { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string? ProfileStatus { get; set; }

        #region Relationships
        public virtual ICollection<ServerUser> ServerUsers { get; set; } = [];
        public virtual ICollection<ServerMessage> ServerMessages { get; set; } = [];
        #endregion
    }
}