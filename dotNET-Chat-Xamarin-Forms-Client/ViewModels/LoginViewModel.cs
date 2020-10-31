using dotNET_Chat_Xamarin_Forms_Client.Models;
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

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            NewAccountCommand = new Command(OnNewAccountClicked);
            dialogService = DependencyService.Get<IDialogService>();
            authenticationService = DependencyService.Get<IAuthenticationService>();
            propertiesService = DependencyService.Get<IPropertiesService>();
        }

        private async void OnLoginClicked(object obj)
        {
            if(await FieldsNullOrEmptyAsync())
            {
                return;
            }    

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
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }

        private async void OnNewAccountClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        private async Task<bool> FieldsNullOrEmptyAsync()
        {
            var fields = new string[] { UserName, Password };
            if (fields.Any(it => it == null || it == string.Empty))
            {
                await dialogService.ShowAlert("Fill all fields.");
                return true;
            }
            return false;
        }
    }
}
