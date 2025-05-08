using BlazorComponentUtilities;
using Konnekt.Presentation.Components.Base;
using Konnekt.Presentation.Components.Popup.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Popup
{
    public partial class Popup : PresentationBase
    {
        [Parameter]
        public PopupModel? PopupModel { get; set; }
        [Parameter]
        public RenderFragment<object>? ChildContent { get; set; }

        private EditContext? CascadedEditContext { get; set; }
        private CancellationTokenSource _suspensionToken = new CancellationTokenSource();
        private Action _cancelSuspension = () => { };

        internal async Task<PopupResult> ToggleVisibilityAsync(bool state, RenderFragment<object>? rf = null, Type? editContextType = null)
        {
            if (PopupModel is null)
                return new();

            object? model = new object();
            CascadedEditContext = new EditContext(model);
            _suspensionToken = new CancellationTokenSource();

            await Task.Factory.StartNew(() =>
            {
                PopupModel.IsVisible = state;

                if (rf is not null && editContextType is not null)
                {
                    if (!editContextType.Namespace?.StartsWith("System") ?? false)
                    {
                        model = Activator.CreateInstance(editContextType);
                    }

                    if (model is null)
                        throw new NullReferenceException($"Activator tried to create an instance of {nameof(editContextType)} but no instantiation was found.");

                    CascadedEditContext = new EditContext(model);
                    ChildContent = rf;
                }

                InvokeAsync(StateHasChanged);

                while (!_suspensionToken.IsCancellationRequested)
                {
                    _cancelSuspension = _suspensionToken.Cancel;
                }
            });

            return PopupModel.Result;
        }

        private async Task PopupCloseControlClickedAsync(PopupResponseState responseState)
        {
            if (PopupModel is not null)
            {
                if (CascadedEditContext is not null && responseState is not PopupResponseState.ClosedWithCancellation)
                {
                    if (CascadedEditContext.Validate())
                        PopupModel.Result.Data = CascadedEditContext.Model;
                    else
                        return;
                }

                PopupModel.Result.PopupResponseState = responseState;

                _cancelSuspension.Invoke();
                await ToggleVisibilityAsync(false);
            }
        }
    }
}
