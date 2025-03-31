using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Button
{
    public partial class Button : PresentationComponentBase
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
        public string Icon { get; set; } = string.Empty;
        [Parameter]
        public string IconClass { get; set; } = string.Empty;
        [Parameter]
        public ButtonType ButtonType { get; set; }
        [Parameter]
        public ButtonSize ButtonSize { get; set; }
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// CssBuilder to handle the class string creation for the button itself.
        /// </summary>
        private string _class => new CssBuilder("button")
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .AddClass(_classButtonSize)
            .AddClass("p-2 m-0 d-flex flex-row border-0")
            .Build();

        /// <summary>
        /// CssBuilder to handle the class string creation for the label.
        /// </summary>
        private string _labelClass => new CssBuilder()
            .AddClass(LabelClass, !string.IsNullOrEmpty(Label))
            .AddClass("m-0")
            .AddClass("ps-2 text-truncate", !string.IsNullOrEmpty(Label))
            .Build();

        /// <summary>
        /// CssBuilder to handle the class string creation for the icon.
        /// </summary>
        private string _iconClass => new CssBuilder("button-icon")
            .AddClass(Icon, !string.IsNullOrEmpty(Icon))
            .AddClass(IconClass, !string.IsNullOrEmpty(IconClass))
            .Build();

        private string _htmlButtonType = string.Empty;
        private string _classButtonSize = string.Empty;

        protected override void OnParametersSet()
        {
            switch (ButtonType)
            {
                case ButtonType.OnClick:
                    _htmlButtonType = "button";
                    break;

                case ButtonType.Submit:
                    _htmlButtonType = "submit";
                    break;
            }

            switch (ButtonSize)
            {
                case ButtonSize.Max:
                    _classButtonSize = "button__width_max";
                    break;

                case ButtonSize.Square:
                    _classButtonSize = "button__width_square";
                    break;

                case ButtonSize.Small:
                    _classButtonSize = "button__width_small";
                    break;

                case ButtonSize.Medium:
                    _classButtonSize = "button__width_medium";
                    break;

                case ButtonSize.Large:
                    _classButtonSize = "button__width_large";
                    break;
            }

            base.OnParametersSet();
        }

        /// <summary>
        /// A simple OnClick handler so that whenever OnClick is called, the page re-renders changes.
        /// </summary>
        private void OnClickHandler()
        {
            OnClick.InvokeAsync();
            StateHasChanged();
        }
    }
}
