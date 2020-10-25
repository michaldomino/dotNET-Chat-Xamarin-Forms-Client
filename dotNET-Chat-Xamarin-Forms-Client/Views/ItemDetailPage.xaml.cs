using System.ComponentModel;
using Xamarin.Forms;
using dotNET_Chat_Xamarin_Forms_Client.ViewModels;

namespace dotNET_Chat_Xamarin_Forms_Client.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}