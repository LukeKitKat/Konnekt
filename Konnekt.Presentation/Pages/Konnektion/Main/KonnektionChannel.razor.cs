using Konnect.Service.DatabaseManager.Models;
using Konnekt.Presentation.Components.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Pages.Konnektion.Main
{
    public partial class KonnektionChannel : PresentationBase
    {
        [Parameter]
        public ServerChannel? ChannelModel { get; set; }
    }
}
