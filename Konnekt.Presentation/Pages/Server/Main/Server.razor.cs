using Konnekt.Presentation.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Pages.Server.Main
{
    [Authorize(Policy =)]
    public partial class Server : PresentationPageBase
    {
        [Parameter]
        public string ServerId { get; set; }
    }
}
