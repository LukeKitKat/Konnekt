using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.ServerNavigator;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Pages.Home
{
    public partial class Home
    {
        [Inject]
        private ServerNavigator ServerNavigator { get; set; } = default!;

        private List<User> Users { get; set; } = new List<User>();

        protected override async Task OnInitializedAsync()
        {
            await RefreshListAsync();
            await base.OnInitializedAsync();
        }

        private async Task RefreshListAsync()
        {
            var result = await ServerNavigator.ReadServersAsync();

            if (result.Success)
                Users = result.Result!;
        }

        private async Task NavigateAsync()
        {
            var result = await ServerNavigator.AddToServersAsync("Name Test");

            if (result.Success)
            {
                await RefreshListAsync();
                StateHasChanged();
            }
        }
    }
}
