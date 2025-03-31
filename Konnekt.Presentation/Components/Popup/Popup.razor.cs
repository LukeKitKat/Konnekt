using BlazorComponentUtilities;
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
    public partial class Popup
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
            if (PopupModel is not null)
            {
                await Task.Run(() =>
                {
                    PopupModel.IsVisible = state;

                    if (rf is not null && editContextType is not null)
                    {
                        object? model = null;

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
            }

            return PopupModel?.Result ?? new();
        }

        private async Task PopupCloseControlClickedAsync(PopupResponseState responseState)
        {
            if (PopupModel is not null && CascadedEditContext is not null)
            {
                if (CascadedEditContext.Validate() || responseState is not PopupResponseState.ClosedWithConfirmation)
                {
                    _cancelSuspension.Invoke();
                    PopupModel.Result.PopupResponseState = responseState;
                    PopupModel.Result.Data = CascadedEditContext.Model;
                    await ToggleVisibilityAsync(false);
                }

                StateHasChanged();
            }
        }
    }
}
