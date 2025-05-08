using Konnect.Service.ActivityObserver;
using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.ServerNavigator;
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

namespace Konnekt.Presentation.Pages.Konnektion.Main
{
    public partial class KonnektionContainer() : PresentationBase, IDisposable
    {
        [Parameter]
        public string? ServerId { get; set; }

        [Inject]
        private ServerManager ServerManager { get; set; } = default!;

        private Server? ServerModel { get; set; }
        private List<User> OnlineUsers { get; set; }
        private List<User> OfflineUsers { get; set; }

        private bool disposed = false;

        protected override async Task OnInitializedAsync()
        {
            ActivityObserver.SystemActivityFuncs += InternalRefresh;
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await CheckForServerChangesAsync();
            await base.OnAfterRenderAsync(firstRender);
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

                    await ActivityObserver.NotifySystemActivity();
                }
            }
        }

        private void InternalRefresh()
            => InvokeAsync(StateHasChanged);

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

        private async Task CheckForServerChangesAsync()
        {
            if (ServerModel is not null)
            {
                var channelResp = ServiceHandler(await ServerManager.GetServerChannelsAsync(ServerModel));
                if (!channelResp.Success)
                    return;

                if (ServerModel.ServerChannels.Count != channelResp.Result!.Count)
                {
                    ServerModel.ServerChannels = channelResp.Result!;
                }

                StateHasChanged();
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
#pragma warning disable CS8601 // Possible null reference assignment.
                    ActivityObserver.SystemActivityFuncs -= InternalRefresh;
#pragma warning restore CS8601 // Possible null reference assignment.
                }

                disposed = true;
            }
        }

        ~KonnektionContainer()
        {
            Dispose(disposing: false);
        }
    }
}
