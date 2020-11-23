using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dotNET_Chat_Xamarin_Forms_Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewChatPage : ContentPage
    {
        public Chat Chat { get; set; }

        public NewChatPage()
        {
            InitializeComponent();
            BindingContext = new NewChatViewModel();
        }
    }
}