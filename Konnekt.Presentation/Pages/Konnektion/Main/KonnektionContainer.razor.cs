using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.Services.ServerManagerService;
using Konnekt.Presentation.Components.Base;
using Konnekt.Presentation.Components.Popup.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Konnekt.Presentation.Pages.Konnektion.Main
{
    public partial class KonnektionContainer() : PresentationBase
    {
        [Parameter]
        public string? ServerId { get; set; }

        [Inject]
        private ServerManager ServerManager { get; set; } = default!;

        private Server? ServerModel { get; set; }
        public string? ActiveChannelId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ActivityObserver.SystemActivityFuncs += InternalRefreshAsync;
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await PopulateServerModelAsync();
            await base.OnParametersSetAsync();
        }

        private async Task AddNewChannelClickedAsync()
        {
            if (Layout?.PopupController is not null && ServerModel is not null)
            {
                var createResult = await Layout.PopupController.OpenPopupAsync<ServerChannel>(PopupType.CreateChannel, CreatingChannelFragment);
                if (createResult.PopupResponseState is PopupResponseState.None or PopupResponseState.ClosedWithCancellation)
                    return;

                if (createResult.Data is ServerChannel channel)
                {
                    var resp = ServiceHandler(await ServerManager.CreateNewServerChannelAsync(ServerModel, channel));
                    if (!resp.Success)
                        return;

                    await ActivityObserver.NotifySystemActivityAsync();
                }
            }
        }

        private async Task InternalRefreshAsync()
        {
            await InvokeAsync(StateHasChanged);
        }

        private async Task PopulateServerModelAsync()
        {
            if (ServerId is not null && ServerId != ServerModel?.Id)
            {
                var serverResp = ServiceHandler(await ServerManager.GetServerByIdAsync(ServerId));
                if (!serverResp.Success)
                    return;

                ServerModel = serverResp.Result!;
            }
        }

        private void OnChannelClicked(string channelId)
        {
            ActiveChannelId = channelId;
            StateHasChanged();
        }
    }
}
