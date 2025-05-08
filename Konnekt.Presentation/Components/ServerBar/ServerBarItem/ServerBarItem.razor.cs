using Konnect.Service.DatabaseManager.Models;
using Konnekt.Presentation.Components.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.ServerBar.ServerBarItem
{
    public partial class ServerBarItem : PresentationBase
    {
        [Parameter]
        public Server Server { get; set; } = new();
        [Parameter]
        public bool RenderServerName { get; set; } = false;

        private void OnServerClicked()
            => NavigationManager.NavigateTo($"/Server/{Server.Id}", false, true);
    }
}
