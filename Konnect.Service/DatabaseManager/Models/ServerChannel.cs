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
    [Table("ServerChannels")]
    [Index(nameof(Id), Name = "IX_ServerChannels_Id", IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    public class ServerChannel : EntityBase
    {
        [Key]
        [ForeignKey(nameof(Server))]
        public string ServerId { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(64)")]
        [MaxLength(64)]
        public string? ChannelName { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        [MaxLength(512)]
        public string? ChannelDescription { get; set; }

        public int ChannelOrder { get; set; }

        #region Relationships
        public virtual Server? Server { get; set; }
        #endregion
    }
}