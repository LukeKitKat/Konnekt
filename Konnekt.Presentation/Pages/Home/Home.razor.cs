using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.ServerNavigator;
using Konnekt.Presentation.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Pages.Home
{
    public partial class Home : PresentationPageBase
    {
        [Inject]
        private ServerManager ServerNavigator { get; set; } = default!;

        private string? Test { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            var layout = Layout;

            return base.OnAfterRenderAsync(firstRender);
        }
    }
}
