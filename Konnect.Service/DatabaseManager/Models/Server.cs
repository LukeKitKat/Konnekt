using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnect.Service.DatabaseManager.Models
{
    [Table("Servers")]
    [Index(nameof(Id), Name = "IX_Servers_Id", IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    public class Server : EntityBase
    {
        [Required]
        public string? ServerName { get; set; }

        [Required]
        public string? OwnerId { get; set; }

        public DateTime DateCreated { get; } = DateTime.UtcNow;

        #region Relationships
        public virtual ICollection<ServerUser> ServerUsers { get; set; } = [];
        public virtual ICollection<ServerJoinCode> ServerJoinCodes { get; set; } = [];
        #endregion
    }
}
