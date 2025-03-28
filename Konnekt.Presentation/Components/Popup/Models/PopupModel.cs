using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Popup.Models
{
    public class PopupModel
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public PopupResult Result { get; set; } = new();
        //
        internal string? TitleText { get; set; } = null;
        internal string? BodyText { get; set; } = null;
        internal string? ConfirmButtonText { get; set; } = "Ok";
        internal string? RejectButtonText { get; set; } = null;
        internal string? CancelButtonText { get; set; } = "Cancel";
        internal bool IsVisible { get; set; }
        internal Func<bool, RenderFragment<object>?, Type?, Task<PopupResult>>? ToggleVisibilityAsync { get; set; }
    }

    public class PopupResult
    {
        public PopupResponseState PopupResponseState { get; set; }
        public object? Data { get; set; }
    }
}
