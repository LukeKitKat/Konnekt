using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnect.Service.DatabaseManager.Models
{
    public class EntityBase
    {
        [Column(TypeName = "int")]
        public int Id { get; }
    }
}
