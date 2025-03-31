using BlazorComponentUtilities;
using Konnekt.Presentation.Components.Input;
using Konnekt.Presentation.Components.Popup.Models;
using Konnekt.Presentation.Pages.Server.Joining;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.ServerBar
{
    public partial class ServerBar
    {
        [CascadingParameter]
        public MainLayout? Layout { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private bool _collapsed = true;
        private string _collapsedClasses => new CssBuilder("fa-regular")
            .AddClass("fa-square-caret-left", _collapsed == false)
            .AddClass("fa-square-caret-right", _collapsed == true)
            .Build();

        private string _serverBarClasses => new CssBuilder("server-bar")
            .AddClass("server-bar--collapsed", _collapsed == true)
            .AddClass("d-flex flex-column")
            .Build();

        private List<string> _servers = new List<string>();

        public ServerBar()
        {
            _servers.Add("MASSIVE SERVER NAME AAAAAAAAAAAAAA");

            for (int i = 0; i < 25; i++)
                _servers.Add($"Server Number {i + 1}");
        }

        private void ToggleCollapse()
        {
            _collapsed = !_collapsed;
            StateHasChanged();
        }

        private async Task AddNewKonnektionClickedAsync()
        {
            if (Layout?.PopupController is not null)
            {
                var result = await Layout.PopupController.OpenPopupAsync<string>(PopupType.JoinServer, JoiningFragment);
                if (result.PopupResponseState == PopupResponseState.ClosedWithConfirmation)
                {
                    NavigationManager.NavigateTo($"/Server/Join/{result.Data}", false, true);
                }
            }
        }
    }
}
