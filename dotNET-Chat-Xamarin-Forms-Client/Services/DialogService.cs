using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    class DialogService : IDialogService
    {
        public async Task ShowAlert(string message, string title = "Alert", string cancel = "OK")
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
