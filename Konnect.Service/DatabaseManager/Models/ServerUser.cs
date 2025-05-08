using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Konnect.Service.DatabaseManager.Models
{
    [Table("ServerUsers")]
    [Index(nameof(Id), Name = "IX_ServerUsers_Id", IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    public class ServerUser : EntityBase
    {
        [Key]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;
        public virtual User? User { get; set; }

        [Key]
        [ForeignKey(nameof(Server))]
        public string ServerId { get; set; } = string.Empty;
        public virtual Server? Server { get; set; }

        public int ServerOrder { get; set; }
    }
}
