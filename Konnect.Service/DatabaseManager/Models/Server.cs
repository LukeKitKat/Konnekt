using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        #region Relationships
        public virtual ICollection<ServerUser> ServerUsers { get; set; } = [];
        #endregion
    }
}
