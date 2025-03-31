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
        ClosedWithOption1 = 2,
        ClosedWithOption2 = 3,
        ClosedWithCancellation = 4,
    }
}
