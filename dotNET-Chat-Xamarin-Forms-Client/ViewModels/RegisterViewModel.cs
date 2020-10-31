using dotNET_Chat_Xamarin_Forms_Client.Models.Response;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using dotNET_Chat_Xamarin_Forms_Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace dotNET_Chat_Xamarin_Forms_Client.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        private string userName;
        private string email;
        private string password;
        private string confirmPassword;
        private readonly IDialogService dialogService;
        private readonly IAuthenticationService authenticationService;
        private readonly IPropertiesService propertiesService;

        public Command RegisterCommand { get; }

        public string UserName
        {
            get => userName;
            set
            {
                SetProperty(ref userName, value);
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => email;
            set
            {
                SetProperty(ref email, value);
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                SetProperty(ref confirmPassword, value);
                OnPropertyChanged();
            }
        }

        public RegisterViewModel()
        {
            dialogService = DependencyService.Get<IDialogService>();
            authenticationService = DependencyService.Get<IAuthenticationService>();
            propertiesService = DependencyService.Get<IPropertiesService>();
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            if (await FieldsNullOrEmptyAsync())
            {
                return;
            }
            if (Password != ConfirmPassword)
            {
                await dialogService.ShowAlert("Passwords do not match");
                return;
            }

            AuthenticationResponseModel responseModel = await authenticationService.Register(UserName, Email, Password);
            if (!responseModel.Success)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Errors: \n");
                responseModel.Errors.ForEach(it => stringBuilder.Append(it).Append("\n"));
                await dialogService.ShowAlert(stringBuilder.ToString());
                return;
            }
            await propertiesService.SetJwtTokenAsync(responseModel.Token);
            await propertiesService.SetUserNameAsync(UserName);
            await Shell.Current.GoToAsync($"//{nameof(ChatsPage)}");
        }

        private async Task<bool> FieldsNullOrEmptyAsync()
        {
            var fields = new string[] { UserName, Email, Password, ConfirmPassword };
            if (fields.Any(it => string.IsNullOrWhiteSpace(it)))
            {
                await dialogService.ShowAlert("Fill all fields.");
                return true;
            }
            return false;
        }
    }
}
