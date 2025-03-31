using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.ServerBar.Models
{
    public class CreatingServerModel
    {
        [Required]
        public string? ServerName { get; set; }
    }
}
