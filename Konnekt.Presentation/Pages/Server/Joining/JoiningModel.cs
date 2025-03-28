using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Pages.Server.Joining
{
    public class JoiningModel
    {
        [Required]
        public string? JoinCode { get; set; }
    }
}
