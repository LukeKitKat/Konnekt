using Konnekt.Presentation.Components.Popup;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components
{
    public partial class MainLayout : LayoutComponentBase
    {
        public PopupController? PopupController { get; set; }

        public void Refresh() => StateHasChanged();
    }
}
