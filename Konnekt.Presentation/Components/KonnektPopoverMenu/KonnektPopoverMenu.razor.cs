using BlazorComponentUtilities;
using Konnekt.Presentation.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Konnekt.Presentation.Components.KonnektPopoverMenu
{
    public partial class KonnektPopoverMenu : PresentationBase
    {
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string? Label { get; set; }

        private bool _popoverVisible = false;
        private string _popoverDisplayClass => new CssBuilder("popover-menu")
            .AddClass("popover-menu__display_all", _popoverVisible == true)
            .Build();

        private void TogglePopoverState()
             { _popoverVisible = !_popoverVisible; StateHasChanged(); }
    }
}
