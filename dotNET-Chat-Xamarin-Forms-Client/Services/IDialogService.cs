using System.Threading.Tasks;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    interface IDialogService
    {
        Task ShowAlert(string message, string title="Alert",  string cancel = "OK");
    }
}
