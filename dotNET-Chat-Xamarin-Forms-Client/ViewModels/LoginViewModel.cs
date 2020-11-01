﻿using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Models.Request;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using dotNET_Chat_Xamarin_Forms_Client.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace dotNET_Chat_Xamarin_Forms_Client.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string userName;
        private string password;
        private readonly IDialogService dialogService;
        private readonly IAuthenticationService authenticationService;
        private readonly IPropertiesService propertiesService;

        public Command LoginCommand { get; }
        public Command NewAccountCommand { get; }

        public string UserName
        {
            get => userName;
            set
            {
                SetProperty(ref userName, value);
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

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked, ValidateFields);
            NewAccountCommand = new Command(OnNewAccountClicked);
            dialogService = DependencyService.Get<IDialogService>();
            authenticationService = DependencyService.Get<IAuthenticationService>();
            propertiesService = DependencyService.Get<IPropertiesService>();
            PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
        }

        private bool ValidateFields(object arg)
        {
            return !FieldsNullOrWhiteSpace();
        }

        private async void OnLoginClicked(object obj)
        {
            var responseModel = await authenticationService.LoginAsync(UserName, Password);
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

        private async void OnNewAccountClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        private bool FieldsNullOrWhiteSpace()
        {
            var fields = new string[] { UserName, Password };
            return fields.Any(it => string.IsNullOrWhiteSpace(it));
        }
    }
}
