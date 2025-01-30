using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.TopBar
{
    public partial class TopBar
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void ProfileClicked()
        {
            NavigationManager.NavigateTo("/Login");
        }
    }
}
