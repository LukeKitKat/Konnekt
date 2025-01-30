using Konnect.Service.DatabaseManager.Models;
using Konnect.Service.UserManager;
using Konnekt.Presentation.Pages.Login.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Konnekt.Presentation.Pages.Login
{
    public partial class Login
    {
        [Inject]
        private UserManager UserManager { get; set; } = default!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private bool Registering;

        private EditContext? EditContext { get; set; }
        private ValidationMessageStore? messageStore;

        private LoginModel LoginModel = new();

        private User RegisteringUser = new();
        private string RegisteringPassword = string.Empty;
        private string RegisteringConfirmPassword = string.Empty;

        protected override void OnInitialized()
        {
            EditContext = new(new LoginModel());
        }

        private async Task RegisterNewUserAsync()
        {
            if (RegisteringPassword == RegisteringConfirmPassword)
            {
                var resp = await UserManager.RegisterNewUserAsync(RegisteringUser, RegisteringPassword);

                if (resp.Success)
                    return;
            }
        }

        private async Task LoginToExistingAccount()
        {
            var resp = await UserManager.ValidateUserLoginAsync(LoginModel.LoginUserName, LoginModel.LoginPassword);

            if (!resp.Success)
                return;

            if (resp.Result)
                NavigationManager.NavigateTo("/");
        }

        private void ToggleRegisterState()
        {
            Registering = !Registering;
            StateHasChanged();
        }
    }
}
