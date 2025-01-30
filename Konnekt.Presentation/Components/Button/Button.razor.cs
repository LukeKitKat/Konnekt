using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Button
{
    public partial class Button
    {
        [Parameter]
        public EventCallback OnClick { get; set; }
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public string LabelClass { get; set; } = string.Empty;
        [Parameter]
        public ButtonTypes ButtonType { get; set; } = ButtonTypes.OnClick;

        private string _class => new CssBuilder("button")
            .AddClass(Class)
            .Build();

        private string _labelClass => new CssBuilder("button__label")
            .AddClass(LabelClass)
            .Build();

        private string _htmlButtonType = string.Empty;

        public Button()
        {
            switch (ButtonType)
            {
                case ButtonTypes.OnClick:
                    _htmlButtonType = "button";
                    break;

                case ButtonTypes.Submit:
                    _htmlButtonType = "button";
                    break;
            }
        }

        private void OnClickHandler()
        {
            OnClick.InvokeAsync();
            StateHasChanged();
        }
    }
}
