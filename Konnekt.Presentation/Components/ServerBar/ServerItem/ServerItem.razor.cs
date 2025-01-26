using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.ServerBar.ServerItem
{
    public partial class ServerItem
    {
        [Parameter]
        public string ServerName { get; set; } = string.Empty;
    }
}
