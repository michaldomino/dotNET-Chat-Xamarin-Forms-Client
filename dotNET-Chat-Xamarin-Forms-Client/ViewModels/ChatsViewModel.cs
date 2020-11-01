using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using dotNET_Chat_Xamarin_Forms_Client.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.ViewModels
{
    class ChatsViewModel : BaseViewModel
    {
        private Chat selectedChat;
        private readonly IApplicationUsersService applicationUsersService;
        private readonly IDialogService dialogService;

        public ObservableCollection<Chat> Chats { get; }
        public Command LoadChatsCommand { get; }
        public Command AddChatCommand { get; }
        public Command<Chat> TappedChat { get; }

        public Chat SelectedChat
        {
            get => selectedChat;
            set
            {
                SetProperty(ref selectedChat, value);
                OnChatSelected(value);
            }
        }

        public ChatsViewModel()
        {
            applicationUsersService = DependencyService.Get<IApplicationUsersService>();
            dialogService = DependencyService.Get<IDialogService>();

            Title = "Select chat";
            PropertiesService propertiesService = new PropertiesService();
            Chats = new ObservableCollection<Chat>();
            LoadChatsCommand = new Command(async () => await ExecuteLoadChatsCommand());

            TappedChat = new Command<Chat>(OnChatSelected);

            AddChatCommand = new Command(OnAddChat);
        }

        private async Task ExecuteLoadChatsCommand()
        {
            IsBusy = true;
            try
            {
                Chats.Clear();
                var chats = await applicationUsersService.GetChatsAsync();
                foreach (var chat in chats)
                {
                    Chats.Add(chat);
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
            SelectedChat = null;
        }


        private async void OnAddChat(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewChatPage));
        }

        async void OnChatSelected(Chat chat)
        {
            if (chat == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ChatMessagesPage)}?{nameof(ChatMessagesViewModel.ChatId)}={chat.Id}");
        }
    }
}
