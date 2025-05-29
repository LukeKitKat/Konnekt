using BlazorComponentUtilities;
using Konnekt.Presentation.Components.Base;
using Konnekt.Presentation.Components.KonnektImageRenderer.Enums;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.KonnektImageRenderer
{
    public partial class KonnektImageRenderer : PresentationBase
    {
        [Parameter]
        public byte[]? ImageSource { get; set; }
        [Parameter]
        public string? ImageAltText { get; set; }
        [Parameter]
        public string? Class { get; set; }
        [Parameter]
        public ImageType ImageType { get; set; } = ImageType.Profile;

        private string _imageType = string.Empty;
        private string _classes => new CssBuilder("konnekt-image-renderer")
            .AddClass(Class)
            .AddClass(_imageType, !string.IsNullOrEmpty(_imageType))
            .Build();

        protected override void OnInitialized()
        {
            switch (ImageType)
            {
                case ImageType.Profile:
                    _imageType = "konnekt-image-renderer__profile-image";
                    break;

                case ImageType.Message:
                    _imageType = "konnekt-image-renderer__message-image";
                    break;
            }
            base.OnInitialized();
        }
    }
}
