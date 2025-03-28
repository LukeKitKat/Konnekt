using Konnekt.Presentation.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Pages.Server.Joining
{
    public partial class JoinServer : PresentationPageBase
    {
        [Parameter]
        public string JoinCode { get; set; }
    }
}
