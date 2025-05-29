using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.Models;
using Konnect.Service.Services.ActivityObserverService;
using Konnekt.Presentation.Components.Layout;
using Konnekt.Presentation.Components.Layout.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Konnekt.Presentation.Components.Base
{
    [Authorize]
    public partial class PresentationBase : LayoutComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject]
        public ActivityObserver ActivityObserver { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        internal UserManager<User> UserManager { get; set; } = default!;
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [CascadingParameter]
        public MainLayout? Layout { get; set; }

        [JSInvokable("EndSession")]
        public async Task CallSessionEndAsync(int navigationType)
        {
            if (navigationType == 0)
                return; 

            User? currentUser = await GetCurrentUserAsync();
            await ActivityObserver.TryAddOrRemoveUserActivity(false, currentUser);
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return await UserManager.GetUserAsync(state.User);
        }

        public ServiceResponse<T> ServiceHandler<T>(ServiceResponse<T> resp)
        {
            if (Layout is not null)
            {
                foreach (var error in resp.Errors)
                {
                    Layout.RegisterNewNotification(NotificationType.Error, error);
                }
            }

            return resp;
        }

        public ServiceResponse ServiceHandler(ServiceResponse resp)
        {
            if (Layout is not null)
            {
                foreach (var error in resp.Errors)
                {
                    Layout.RegisterNewNotification(NotificationType.Error, error);
                }
            }

            return resp;
        }
    }
}
