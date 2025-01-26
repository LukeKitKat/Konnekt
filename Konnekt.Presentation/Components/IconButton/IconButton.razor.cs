using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.IconButton
{
    public partial class IconButton
    {
        [Parameter]
        public EventCallback OnClick { get; set; }
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string IconClass { get; set; } = string.Empty;

        private string _class => new CssBuilder("icon-button")
            .AddClass(Class)
            .Build();

        private string _iconClass => new CssBuilder("icon-button-icon")
            .AddClass(IconClass)
            .Build();

        private void OnClickHandler()
        {
            OnClick.InvokeAsync();
            StateHasChanged();
        }
    }
}
