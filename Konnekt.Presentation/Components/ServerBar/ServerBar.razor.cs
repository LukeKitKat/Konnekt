using BlazorComponentUtilities;
using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.Services.ServerManagerService;
using Konnekt.Presentation.Components.Base;
using Konnekt.Presentation.Components.Popup.Models;
using Konnekt.Presentation.Components.ServerBar.Models;
using Microsoft.AspNetCore.Components;

namespace Konnekt.Presentation.Components.ServerBar
{
    public partial class ServerBar : PresentationBase
    {
        [Inject]
        private ServerManager ServerManager { get; set; } = default!;


        private bool _collapsed = false;
        private string CollapsedClasses => new CssBuilder("fa-regular")
            .AddClass("fa-square-caret-left", _collapsed == false)
            .AddClass("fa-square-caret-right", _collapsed == true)
            .Build();

        private string ServerBarClasses => new CssBuilder("server-bar py-2 col-1")
            .AddClass("server-bar--collapsed", _collapsed == true)
            .Build();

        private List<Server> _servers = [];

        protected override async Task OnInitializedAsync()
        {
            await PopulateServerListAsync();
            await base.OnInitializedAsync();
        }

        private void ToggleCollapse()
        {
            _collapsed = !_collapsed;
            StateHasChanged();
        }

        private async Task PopulateServerListAsync()
        {
            User? user = await GetCurrentUserAsync();
            var resp = ServiceHandler(await ServerManager.GetUserServersAsync(user));
            if (!resp.Success)
                return;

            _servers = resp.Result!;
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
                User? user = await GetCurrentUserAsync();

                if (joinOrCreatingResult.PopupResponseState is PopupResponseState.ClosedWithOption1)
                {
                    var joiningResult = await Layout.PopupController.OpenPopupAsync<JoiningServerModel>(PopupType.JoinServer, JoiningServerFragment);
                    
                    if (joiningResult.Data is JoiningServerModel jsmData)
                    {
                        var serverResp = ServiceHandler(await ServerManager.AddUserToServerAsync(jsmData.JoinCode, user));
                        if (!serverResp.Success)
                            return;

                        serverId = serverResp.Result!.Id;
                    }
                }
                else if (joinOrCreatingResult.PopupResponseState is PopupResponseState.ClosedWithOption2)
                {                    
                    var creatingResult = await Layout.PopupController.OpenPopupAsync<CreatingServerModel>(PopupType.CreateServer, CreatingServerFragment);

                    if (creatingResult.Data is CreatingServerModel csmData)
                    {
                        var serverResp = ServiceHandler(await ServerManager.CreateNewServerAsync(csmData.ServerName!, user));
                        if (!serverResp.Success)
                            return;

                        serverId = serverResp.Result!.Id;
                    }
                }

                await PopulateServerListAsync();

                if (!string.IsNullOrEmpty(serverId))
                    NavigationManager.NavigateTo($"/Server/{serverId}");
            }
        }
    }
}
