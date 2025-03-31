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
    public partial class PopupController : PresentationPageBase
    {
        [CascadingParameter]
        private EditContext? CascadedEditContext { get; set; }

        public async Task<PopupResult> OpenPopupAsync(PopupType popupType) =>
            await RenderPopupAsync(popupType, null, null);

        public async Task<PopupResult> OpenPopupAsync<TEditContextValue>(PopupType popupType, RenderFragment<object>? rf = null) =>
            await RenderPopupAsync(popupType, rf, typeof(TEditContextValue));

        private async Task<PopupResult> RenderPopupAsync(PopupType popupType, RenderFragment<object>? rf = null, Type? editContextType = null)
        {
            var popup = PopupRenderModels.FirstOrDefault(x => x.PopupType == popupType);

            if (popup is not null)
            {
                if (popup.ElementReference is not null)
                    return await popup.ElementReference.ToggleVisibilityAsync(true, rf, editContextType);
            }

            return new();
        }

        private RenderFragment BuildPopup(PopupModel model) => __builder =>
        {
            __builder.OpenComponent<Popup>(1);
            __builder.AddAttribute(2, "PopupModel", model);
            __builder.AddAttribute(3, "ChildContent", null as string);
            __builder.CloseComponent();
        };

        private List<PopupRenderModel> PopupRenderModels = new()
        {
            new()
            {
                PopupType = PopupType.JoinServer,
                PopupModel = new PopupModel()
                {
                    TitleText = "Adding Konnektion...",
                    BodyText = "Please enter the konnektion reference below: ",
                }
            }
        };
    }
}

