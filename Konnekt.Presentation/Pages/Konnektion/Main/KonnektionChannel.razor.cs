using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.Services.ServerManagerService;
using Konnekt.Presentation.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
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
        public string? ChannelId { get; set; }
        [Parameter]
        public Server? ServerModel { get; set; }

        [Inject]
        private ServerManager ServerManager { get; set; } = default!;

        private ServerChannel? ChannelModel { get; set; }
        private List<User> OnlineUsers { get; set; } = [];
        private List<User> OfflineUsers { get; set; } = [];

        private EditContext? MessageContext;
        private DateTime LastMessageDate = DateTime.MinValue;

        protected override void OnInitialized()
        {
            MessageContext = new EditContext(MessageInput);
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("scrollToBottom");
            await base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnParametersSetAsync()
        {
            if (ServerModel is not null && !string.IsNullOrEmpty(ChannelId))
            {
                OnlineUsers.Clear();
                OfflineUsers.Clear();
                foreach (var serverUser in ServerModel.ServerUsers)
                {
                    if (ActivityObserver.IsActiveUser(serverUser.UserId))
                        OnlineUsers.Add(serverUser.User);
                    else
                        OfflineUsers.Add(serverUser.User);
                }

                var resp = ServiceHandler(await ServerManager.GetChannelContentAsync(ChannelId));
                if (!resp.Success)
                    return;

                ChannelModel = resp.Result;
            }

            await base.OnParametersSetAsync();
        }

        private async Task SendMessageAsync()
        {
            if (ChannelModel is not null)
            {
                var user = await GetCurrentUserAsync();
                if (user is null)
                    return;

                var resp = ServiceHandler(await ServerManager.AddMessageToChannelAsync(ChannelModel.Id, user.Id, MessageInput.MessageBody ?? string.Empty, MessageInput.Files ?? []));
                if (!resp.Success)
                    return;

                MessageInput = new();
                MessageContext = new(MessageInput);

                await ActivityObserver.NotifySystemActivityAsync();
            }
        }

        private void HandleMessageFile(InputFileChangeEventArgs args)
            => MessageInput.Files = args.GetMultipleFiles();
    }
}
