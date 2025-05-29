using BlazorComponentUtilities;
using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.Services.ServerManagerService;
using Konnekt.Presentation.Components.Base;
using Konnekt.Presentation.Components.Popup.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Pages.Konnektion.Components
{
    public partial class KonnektionMessage : PresentationBase
    {
        [Parameter]
        public ServerMessage? MessageModel { get; set; }

        [Inject]
        private ServerManager ServerManager { get; set; } = default!;

        private bool editingState = false;

        private string _class = new CssBuilder("d-flex")
            .Build();

        private string _messageHeader = string.Empty;

        protected override void OnParametersSet()
        {
            var strBuilder = new StringBuilder($"{MessageModel?.Sender?.DisplayName} - {MessageModel?.TimeSent}");
            if (MessageModel?.TimeEdited is not null)
                strBuilder.Append($" - (Edited): {MessageModel?.TimeEdited}");

            _messageHeader = strBuilder.ToString();
            base.OnParametersSet();
        }

        private EditContext MessageEditContext = new(new ServerMessage());
        private void ToggleEditState()
        {
            if (MessageModel is not null)
            {
                MessageEditContext = new EditContext(MessageModel);
                editingState = !editingState;
                StateHasChanged();
            }
        }

        private async Task OnEditValidSubmitAsync()
        {
            if (MessageModel is not null)
            {
                if (string.IsNullOrEmpty(MessageModel.MessageBody))
                {
                    await OnDeleteClickedAsync();
                    return;
                }
                else
                {
                    var resp = ServiceHandler(await ServerManager.EditMessageInChannelAsync(MessageModel));
                    if (!resp.Success)
                        return;

                    ToggleEditState();
                    await ActivityObserver.NotifySystemActivityAsync();
                }
            }
        }

        private async Task OnDeleteClickedAsync()
        {
            if (Layout?.PopupController is not null && MessageModel is not null)
            {
                var deletePopupResult = await Layout.PopupController.OpenPopupAsync(PopupType.DeleteMessage);
                if (deletePopupResult.PopupResponseState is PopupResponseState.None or PopupResponseState.ClosedWithCancellation)
                    return;

                if (deletePopupResult.PopupResponseState is PopupResponseState.ClosedWithOption1)
                {
                    var resp = ServiceHandler(await ServerManager.DeleteMessageFromChannelAsync(MessageModel.Id));
                    if (!resp.Success)
                        return;

                    await ActivityObserver.NotifySystemActivityAsync();
                }
            }
        }
    }
}
