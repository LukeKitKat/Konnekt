using BlazorComponentUtilities;
using Konnekt.Presentation.Components.Base;
using Konnekt.Presentation.Components.KonnektButton.Models;
using Konnekt.Presentation.Components.KonnektIcon.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.KonnektIcon
{
    public partial class KonnektIcon : PresentationBase
    {
        [Parameter]
        public string Icon { get; set; } = string.Empty;
        [Parameter]
        public string IconClass { get; set; } = string.Empty;
        [Parameter]
        public IconSize IconSize { get; set; } = IconSize.Small;

        /// <summary>
        /// CssBuilder to handle the class string creation for the icon.
        /// </summary>
        private string _iconClass => new CssBuilder("konnekt-icon")
            .AddClass(Icon, !string.IsNullOrEmpty(Icon))
            .AddClass(IconClass, !string.IsNullOrEmpty(IconClass))
            .AddClass(_classIconSize)
            .Build();

        private string _classIconSize = string.Empty;

        protected override void OnParametersSet()
        {
            switch (IconSize)
            {
                case IconSize.Small:
                    _classIconSize = "konnekt-icon__width_small";
                    break;

                case IconSize.Medium:
                    _classIconSize = "konnekt-icon__width_medium";
                    break;

            }

            base.OnParametersSet();
        }
    }
}
