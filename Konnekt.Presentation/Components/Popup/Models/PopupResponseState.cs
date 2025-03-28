using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Popup.Models
{
    public enum PopupResponseState
    {
        None = 1,
        ClosedWithConfirmation = 2,
        ClosedWithRejection = 3,
        ClosedWithCancellation = 4,
    }
}
