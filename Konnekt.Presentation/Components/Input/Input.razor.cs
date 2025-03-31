using Azure.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Konnekt.Presentation.Components.Input
{
    public partial class Input<T>
        where T : class
    {
        [Parameter, EditorRequired]
        public Expression<Func<T>> For { get; set; } = default!;

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? Value { get; set; }
        
        [Parameter]
        public EventCallback<T> ValueChanged { get; set; }
    }
}
