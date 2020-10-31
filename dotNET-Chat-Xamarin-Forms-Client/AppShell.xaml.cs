using System;
using System.Collections.Generic;
using dotNET_Chat_Xamarin_Forms_Client.ViewModels;
using dotNET_Chat_Xamarin_Forms_Client.Views;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(NewChatPage), typeof(NewChatPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
