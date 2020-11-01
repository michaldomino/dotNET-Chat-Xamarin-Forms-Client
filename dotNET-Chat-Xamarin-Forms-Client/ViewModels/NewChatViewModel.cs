using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.ViewModels
{
    class NewChatViewModel : BaseViewModel
    {
        private string name;
        private readonly IChatsService chatService;
        private readonly IDialogService dialogService;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewChatViewModel()
        {
            chatService = DependencyService.Get<IChatsService>();
            dialogService = DependencyService.Get<IDialogService>();

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async void OnSave()
        {
            Chat chat = new Chat()
            {
                Name = Name
            };
            try
            {
                var createdChat = await chatService.CreateChatAsync(chat);
            }
            catch (Exception e)
            {
                await dialogService.ShowAlert(e.Message);
                return;
            }
            await GoToPreviousPageAsync();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        private async void OnCancel()
        {
            await GoToPreviousPageAsync();
        }

        private async Task GoToPreviousPageAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
