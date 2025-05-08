using Konnekt.Presentation.Components.Layout.Enums;
using Konnekt.Presentation.Components.Layout.Models;
using Konnekt.Presentation.Components.Popup;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;

        public PopupController? PopupController { get; set; }

        private List<NotificationModel> _notifications { get; set; } = [];
        public List<NotificationModel> Notifications { get => _notifications; }

        public void Refresh() => StateHasChanged();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Notifications.Any(x => x.NotificationType == NotificationType.Error))
                NavigationManager.NavigateTo("/Error");

            await base.OnAfterRenderAsync(firstRender);
        }

        public void RegisterNewNotification(NotificationType type, string message)
        {
            _notifications.Add(new()
            {
                NotificationType = type,
                NotificationMessage = message,
            });

            StateHasChanged();
        }
    }
}
