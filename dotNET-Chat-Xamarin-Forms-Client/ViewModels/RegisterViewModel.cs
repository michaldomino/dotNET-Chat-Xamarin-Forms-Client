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
            }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                SetProperty(ref confirmPassword, value);
            }
        }

        public RegisterViewModel()
        {
            dialogService = DependencyService.Get<IDialogService>();
            authenticationService = DependencyService.Get<IAuthenticationService>();
            propertiesService = DependencyService.Get<IPropertiesService>();
            RegisterCommand = new Command(OnRegisterClicked, ValidateFields);
            PropertyChanged +=
                (_, __) => RegisterCommand.ChangeCanExecute();
        }

        private bool ValidateFields(object arg)
        {
            return !FieldsNullOrWhiteSpace();
        }

        private async void OnRegisterClicked(object obj)
        {
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

        private bool FieldsNullOrWhiteSpace()
        {
            var fields = new string[] { UserName, Email, Password, ConfirmPassword };
            return fields.Any(it => string.IsNullOrWhiteSpace(it));
        }
    }
}
