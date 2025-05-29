using Konnect.Service.DatabaseManager.Models;
using Konnekt.Presentation.Components.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Konnekt.Presentation.Pages.Home
{
    public partial class Home : PresentationBase
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await JSRuntime.InvokeVoidAsync("setDotNetHelper", DotNetObjectReference.Create(this));

            User? currentUser = await GetCurrentUserAsync();
            var result = await ActivityObserver.TryAddOrRemoveUserActivity(true, currentUser);
            if (result.Exception == true || result.Success == false)
                NavigationManager.NavigateTo("/Account/Signout/", false, true);

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
