using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.ServerBar
{
    public partial class ServerBar
    {
        private bool _collapsed = true;
        private string _collapsedClasses => new CssBuilder("fa-regular")
            .AddClass("fa-square-caret-left", _collapsed == false)
            .AddClass("fa-square-caret-right", _collapsed == true)
            .Build();

        private string _serverBarClasses => new CssBuilder("server-bar")
            .AddClass("server-bar--collapsed", _collapsed == true)
            .Build();

        private List<string> _servers = new List<string>();

        public ServerBar()
        {
            _servers.Add("MASSIVE SERVER NAME AAAAAAAAAAAAAA");

            for (int i = 0; i < 25; i++)
                _servers.Add($"Server Number {i + 1}");
        }

        private void ToggleCollapse() => _collapsed = !_collapsed;
        
    }
}
