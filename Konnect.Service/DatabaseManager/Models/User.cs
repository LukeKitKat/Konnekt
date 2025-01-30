using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnect.Service.DatabaseManager.Models
{
    [Table("Users")]
    [Index(nameof(Id), Name = "IX_Users_Id", IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    public class User : EntityBase
    {
        #region Display Properties

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? AccountName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? DisplayName { get; set; }

        #endregion

        #region System Properties

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string PasswordSalt { get; set; } = string.Empty;

        public DateTime? LastLogin { get; set; }

        public string? LastLoginLocation { get; set; } = string.Empty;

        #endregion
    }
}
