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
    [Table("ServerJoinCodes")]
    [Index(nameof(Id), Name = "IX_ServerJoinCodes_Id", IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    public class ServerJoinCode : EntityBase
    {
        [Column(TypeName = "nvarchar(8)")]
        [MaxLength(8)]
        public string JoinCode { get; set; } = string.Empty;

        [Key]
        [ForeignKey(nameof(Server))]
        public string ServerId { get; set; } = string.Empty;

        #region Relationships
        public virtual Server? Server { get; set; }
        #endregion
    }
}
