using System;
using System.Collections.Generic;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using dotNET_Chat_Xamarin_Forms_Client.ViewModels;
using dotNET_Chat_Xamarin_Forms_Client.Views;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private readonly IAuthenticationService authenticationService;

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewChatPage), typeof(NewChatPage));
            Routing.RegisterRoute(nameof(ChatMessagesPage), typeof(ChatMessagesPage));
            Routing.RegisterRoute(nameof(AddUsersToChatPage), typeof(AddUsersToChatPage));

            authenticationService = DependencyService.Get<IAuthenticationService>();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await authenticationService.LogoutAsync();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
