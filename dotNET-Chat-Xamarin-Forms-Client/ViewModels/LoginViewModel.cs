using dotNET_Chat_Xamarin_Forms_Client.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string userName;
        private string password;

        public Command LoginCommand { get; }

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
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
