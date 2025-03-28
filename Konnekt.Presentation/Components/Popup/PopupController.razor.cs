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
        public async Task<PopupResult> OpenPopupAsync(PopupType popupType)
        {
            return await Popups[popupType].ToggleVisibilityAsync!.Invoke(true, null, null);
        }

        public async Task<PopupResult> OpenPopupAsync<TEditContextValue>(PopupType popupType, RenderFragment<object>? rf = null)
        {
            return await Popups[popupType].ToggleVisibilityAsync!.Invoke(true, rf, typeof(TEditContextValue));
        }

        private RenderFragment BuildPopup(PopupModel model) => __builder =>
        {
            __builder.OpenComponent<Popup>(1);
            __builder.AddAttribute(2, "Model", model);
            __builder.AddAttribute(3, "RenderFragment", null as string);
            __builder.CloseComponent();
        };

        private Dictionary<PopupType, PopupModel> Popups = new()
        {
            [PopupType.JoinServer] = new PopupModel()
            {
                TitleText = "Adding Konnektion...",
                BodyText = "Please enter the konnektion reference below: ",
            }
        };
    }
}

