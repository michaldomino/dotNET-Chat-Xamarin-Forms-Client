using dotNET_Chat_Xamarin_Forms_Client.Models.Response;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using dotNET_Chat_Xamarin_Forms_Client.Views;
using System;
using System.Collections.Generic;
using System.Text;
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
                SetProperty(ref password, value);
                OnPropertyChanged();
            }
        }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Passwords do not match", "OK");
            }
            IAuthenticationService authenticationService = new AuthenticationService();
            AuthenticationResponseModel responseModel = await authenticationService.Register(UserName, Email, Password);
            if (!responseModel.Success)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Errors: \n");
                responseModel.Errors.ForEach(it => stringBuilder.Append(it).Append("\n"));
                await Application.Current.MainPage.DisplayAlert("Alert", stringBuilder.ToString(), "OK");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }
    }
}
