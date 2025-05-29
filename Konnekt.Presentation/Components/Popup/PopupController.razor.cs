using Konnekt.Presentation.Components.Base;
using Konnekt.Presentation.Components.Popup.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Popup
{
    public partial class PopupController : PresentationBase
    {
        [CascadingParameter]
        public EditContext? CascadedEditContext { get; set; }

        public async Task<PopupResult> OpenPopupAsync(PopupType popupType) =>
            await RenderPopupAsync(popupType, null, null);

        public async Task<PopupResult> OpenPopupAsync<TEditContextValue>(PopupType popupType, RenderFragment<object>? rf = null) =>
            await RenderPopupAsync(popupType, rf, typeof(TEditContextValue));

        private async Task<PopupResult> RenderPopupAsync(PopupType popupType, RenderFragment<object>? rf = null, Type? editContextType = null)
        {
            var popup = PopupRenderModels.FirstOrDefault(x => x.PopupType == popupType);

            if (popup is not null)
            {
                popup.PopupModel.Result.PopupResponseState = PopupResponseState.None;

                if (popup.ElementReference is not null)
                    return await popup.ElementReference.ToggleVisibilityAsync(true, rf, editContextType);
            }

            return new();
        }

        private List<PopupRenderModel> PopupRenderModels = new()
        {
            new()
            {
                PopupType = PopupType.JoinOrCreateServer,
                PopupModel = new PopupModel()
                {
                    TitleText = "Adding a new Konnektion",
                    BodyText = "Please select whether you wish to create a new Konnektion or join an existing one.",
                    Option1Text = "Join",
                    Option2Text = "Create",
                }
            },
            new()
            {
                PopupType = PopupType.JoinServer,
                PopupModel = new PopupModel()
                {
                    TitleText = "Joining existing Konnektion",
                    BodyText = $"Please enter the Konnektion reference. {Environment.NewLine} (References are case sensitive).",
                }
            },
            new()
            {
                PopupType = PopupType.CreateServer,
                PopupModel = new PopupModel()
                {
                    TitleText = "Creating new Konnektion",
                    BodyText = "Please enter a name for the Konnektion.",
                }
            },
            new()
            {
                PopupType = PopupType.CreateChannel,
                PopupModel = new PopupModel()
                {
                    TitleText = "Creating new Channel",
                    BodyText = "Please enter a name for the Channel.",
                }
            },
            new ()
            {
                PopupType = PopupType.DeleteMessage,
                PopupModel = new PopupModel()
                {
                    TitleText = "Delete message?",
                    BodyText = "Are you sure you wish to delete the following message?",
                    Option1Text = "Delete",
                    CancelButtonText = "Cancel"
                }
            }
        };
    }
}

