using Konnect.Service.DatabaseManager.Models;
using Konnekt.Presentation.Components.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Pages.Konnektion.Components
{
    public partial class KonnektImageCollage : PresentationBase
    {
        [Parameter, EditorRequired]
        public ICollection<ServerMessageFile> ServerMessageFiles { get; set; } = [];

        private List<List<ServerMessageFile>> VirtualizedFiles { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
            VirtualizeMessages();
            await base.OnInitializedAsync();
        }

        private void VirtualizeMessages()
        {
            VirtualizedFiles.Add(ServerMessageFiles.Take(9).ToList());

            if (VirtualizedFiles.Count < ServerMessageFiles.Count)
            {
                VirtualizeMessages();
            }
        }

        private List<List<ServerMessageFile>> StackFiles(List<ServerMessageFile> virtualizedGroup)
        {
            var rtn = new List<List<ServerMessageFile>>();

            for (int i = 0; i < virtualizedGroup.Count; i += 3)
            {
                rtn.Add(virtualizedGroup.Skip(i).Take(i + 3).ToList());
            }

            return rtn;
        }
    }
}
