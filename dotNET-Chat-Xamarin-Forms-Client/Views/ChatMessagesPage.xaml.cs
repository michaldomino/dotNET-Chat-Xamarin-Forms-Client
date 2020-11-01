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
    public partial class ChatMessagesPage : ContentPage
    {
        private readonly ChatMessagesViewModel viewModel;

        public ChatMessagesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ChatMessagesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}
