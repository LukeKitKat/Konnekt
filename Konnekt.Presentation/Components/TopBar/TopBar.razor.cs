using Konnect.Service.DatabaseManager.Models;
using Konnekt.Presentation.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.TopBar
{
    public partial class TopBar : PresentationBase
    {
        private string currentUserString = string.Empty;
        private bool authorized = false;

        private void ProfileClicked()
        {
            if (authorized)
                NavigationManager.NavigateTo("/Account/Manage");
            else
                NavigationManager.NavigateTo("/Account/Login");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (string.IsNullOrEmpty(currentUserString))
            {
                var user = await GetCurrentUserAsync();
                authorized = user is not null;

                if (!authorized)
                    currentUserString = "Not signed in";
                else
                    currentUserString = $"Signed in as: {user?.UserName}";

                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
