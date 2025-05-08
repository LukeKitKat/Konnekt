using Konnekt.Presentation.Components.Layout.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Layout.Models
{
    public class NotificationModel
    {
        public string NotificationId { get; } = Guid.NewGuid().ToString();
        public NotificationType NotificationType { get; set; } = NotificationType.Error;
        public string NotificationMessage { get; set; } = string.Empty;

    }
}
