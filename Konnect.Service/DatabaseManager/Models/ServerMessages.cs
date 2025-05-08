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
    [Table("ServerMessages")]
    [Index(nameof(Id), Name = "IX_ServerMessages_Id", IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    public class ServerMessages : EntityBase
    {
        [Key]
        [ForeignKey(nameof(ServerChannel))]
        public string ChannelId { get; set; } = string.Empty;

        [Required]
        public string? MessageBody { get; set; }

        #region Relationships
        public virtual ServerChannel? Channel { get; set; }
        #endregion
    }
}
