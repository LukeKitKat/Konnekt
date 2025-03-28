using Azure.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Input
{
    public partial class Input<T> : InputBase<T>
    {
        [Parameter, EditorRequired]
        public Expression<Func<T>> For { get; set; } = default!;

        [Parameter]
        public string? Label { get; set; }

        protected override bool TryParseValueFromString(string? value, out T result, out string validationErrors)
        {
            result = (T?)Convert.ChangeType(value, typeof(T));
            validationErrors = null;
            return true;
        }
    }
}
