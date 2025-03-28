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
    public class EntityBase
    {
        public EntityBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Column(TypeName = "NVARCHAR(36)")]
        public string Id { get; }
    }
}
