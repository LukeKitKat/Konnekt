using Konnect.Service.ServerNavigator;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;

namespace Konnekt.Presentation.Components
{
    public partial class PresentationPageBase : LayoutComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; } = default!;

        [CascadingParameter]
        public MainLayout? Layout { get; set; }
    }
}
