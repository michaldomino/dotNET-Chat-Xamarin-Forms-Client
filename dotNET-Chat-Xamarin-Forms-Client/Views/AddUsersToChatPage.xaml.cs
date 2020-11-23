using dotNET_Chat_Xamarin_Forms_Client.ViewModels;

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

        private void CheckBox_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            viewModel.OnCheckBoxChanged();
        }
    }
}