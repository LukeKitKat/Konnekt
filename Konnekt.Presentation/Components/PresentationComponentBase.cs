using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components
{
    public class PresentationComponentBase : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; } = default!;

        [CascadingParameter]
        public MainLayout? Layout { get; set; }
    }
}
