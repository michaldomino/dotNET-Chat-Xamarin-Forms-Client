using dotNET_Chat_Xamarin_Forms_Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dotNET_Chat_Xamarin_Forms_Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUsersToChatPage : ContentPage
    {
        private readonly AddUsersToChatViewModel viewModel;

        public AddUsersToChatPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AddUsersToChatViewModel();
        }

        protected override void OnAppearing()
        {
            viewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}