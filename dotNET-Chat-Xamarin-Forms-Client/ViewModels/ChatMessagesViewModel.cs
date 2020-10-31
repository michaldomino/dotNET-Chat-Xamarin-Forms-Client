using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.ViewModels
{
    [QueryProperty(nameof(ChatId), nameof(ChatId))]
    class ChatMessagesViewModel : BaseViewModel
    {
        private readonly IChatsService chatService;
        private readonly IDialogService dialogService;
        private string text;
        private Guid chatId;

        public ObservableCollection<Message> Messages { get; }
        public Command LoadMessagesCommand { get; }
        public Command SendMessageCommand { get; }

        public string ChatId
        {
            get => chatId.ToString();
            set => chatId = Guid.Parse(value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public ChatMessagesViewModel()
        {
            chatService = DependencyService.Get<IChatsService>();
            dialogService = DependencyService.Get<IDialogService>();

            Messages = new ObservableCollection<Message>();
            LoadMessagesCommand = new Command(async () => await ExecuteLoadMessagesCommand());
            SendMessageCommand = new Command(OnSendMessage);
        }

        async Task ExecuteLoadMessagesCommand()
        {
            IsBusy = true;
            try
            {
                Messages.Clear();
                var messages = await chatService.GetMessagesAsync(chatId);
                foreach (var message in messages)
                {
                    Messages.Add(message);
                }
            }
            catch (Exception e)
            {
                await dialogService.ShowAlert(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnSendMessage(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
