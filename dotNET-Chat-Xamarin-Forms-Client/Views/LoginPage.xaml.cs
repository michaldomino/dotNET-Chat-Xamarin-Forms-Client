using dotNET_Chat_Xamarin_Forms_Client.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dotNET_Chat_Xamarin_Forms_Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            this.BindingContext = new LoginViewModel();
            InitializeComponent();
        }
    }
}
