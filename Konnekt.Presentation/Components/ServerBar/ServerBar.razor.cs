using BlazorComponentUtilities;
using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.ServerNavigator;
using Konnekt.Presentation.Components.Input;
using Konnekt.Presentation.Components.Popup.Models;
using Konnekt.Presentation.Components.ServerBar.Models;
using Konnekt.Presentation.Pages.Server.Joining;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
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
    public partial class ServerBar : PresentationComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        private ServerManager ServerManager { get; set; } = default!;
        [Inject]
        private UserManager<User> UserManager { get; set; } = default!;
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;


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
                var joinOrCreatingResult = await Layout.PopupController.OpenPopupAsync(PopupType.JoinOrCreateServer);
                if (joinOrCreatingResult.PopupResponseState is PopupResponseState.None or PopupResponseState.ClosedWithCancellation)
                    return;

                string serverId = string.Empty;
                var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = await UserManager.GetUserAsync(state.User);

                if (joinOrCreatingResult.PopupResponseState is PopupResponseState.ClosedWithOption1)
                {
                    var joiningResult = await Layout.PopupController.OpenPopupAsync<JoiningServerModel>(PopupType.JoinServer, JoiningServerFragment);
                    
                    if (joiningResult.Data is JoiningServerModel jsmData)
                    {
                        var serverResp = await ServerManager.AddUserToServerAsync(jsmData.JoinCode, user);
                        if (!serverResp.Success)
                            return;

                        serverId = serverResp.Result.Id;
                    }
                }
                else if (joinOrCreatingResult.PopupResponseState is PopupResponseState.ClosedWithOption2)
                {                    
                    var creatingResult = await Layout.PopupController.OpenPopupAsync<CreatingServerModel>(PopupType.CreateServer, CreatingServerFragment);

                    if (creatingResult.Data is CreatingServerModel csmData)
                    {
                        var serverResp = await ServerManager.CreateNewServerAsync(csmData.ServerName!, user);
                        if (!serverResp.Success)
                            return;

                        serverId = serverResp.Result.Id;
                    }
                }

                if (!string.IsNullOrEmpty(serverId))
                    NavigationManager.NavigateTo($"/Server/{serverId}");
            }
        }
    }
}
