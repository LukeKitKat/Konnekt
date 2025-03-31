using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Popup.Models
{
    internal class PopupRenderModel
    {
        internal Popup? ElementReference { get; set; }
        internal PopupType PopupType { get; set; } = default;
        internal PopupModel PopupModel { get; set; } = new PopupModel();
    }
}
