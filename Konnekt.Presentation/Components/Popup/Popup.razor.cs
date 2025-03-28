using BlazorComponentUtilities;
using Konnekt.Presentation.Components.Popup.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Components.Popup
{
    public partial class Popup
    {
        [Parameter]
        public PopupModel? Model { get; set; }
        [Parameter]
        public RenderFragment<object>? RenderFragment { get; set; }

        private EditContext EditContext { get; set; } = new EditContext(new object());

        private CancellationTokenSource _suspensionToken = new CancellationTokenSource();
        private Action _cancelSuspension = () => { };

        protected override void OnAfterRender(bool firstRender)
        {
            if (Model is not null && firstRender)
            {
                Model.ToggleVisibilityAsync += ToggleVisibilityAsync;
            }

            base.OnAfterRender(firstRender);
        }

        private async Task<PopupResult> ToggleVisibilityAsync(bool state, RenderFragment<object>? rf = null, Type? editContextType = null)
        {
            if (Model is not null)
            {
                await Task.Run(() =>
                {
                    Model.IsVisible = state;
                    
                    if (rf is not null && editContextType is not null)
                    {
                        RenderFragment = rf;

                        var model = Activator.CreateInstance(editContextType);
                        if (model is null)
                            throw new NullReferenceException($"Activator tried to create an instance of {nameof(editContextType)} but no instantiation was found.");

                        EditContext = new EditContext(model);
                    }
                    else
                    {
                        throw new NullReferenceException("Either a RenderFragment was provided, but the type of the EditContext model was not, or vice versa.");
                    }

                    InvokeAsync(StateHasChanged);

                    while (!_suspensionToken.IsCancellationRequested)
                    {
                        _cancelSuspension = _suspensionToken.Cancel;
                    }
                });
            }

            return Model?.Result ?? new();
        }

        private async Task PopupCloseControlClickedAsync(PopupResponseState responseState)
        {
            if (Model is not null)
            {
                if (EditContext.Validate() || responseState is not PopupResponseState.ClosedWithConfirmation)
                {
                    _cancelSuspension.Invoke();
                    Model.Result.PopupResponseState = responseState;
                    await ToggleVisibilityAsync(false);
                }

                StateHasChanged();
            }
        }
    }
}
