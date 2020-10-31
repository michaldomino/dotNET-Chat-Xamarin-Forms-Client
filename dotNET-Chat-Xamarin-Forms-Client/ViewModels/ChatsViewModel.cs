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
    class ChatsViewModel : BaseViewModel
    {
        private Chat selectedChat;
        private readonly IChatsService chatService;

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
            Title = "Browse Chats";
            PropertiesService propertiesService = new PropertiesService();
            var token = propertiesService.GetJwtToken();
            chatService = DependencyService.Get<IChatsService>();
            Chats = new ObservableCollection<Chat>();
            LoadChatsCommand = new Command(async () => await ExecuteLoadChatsCommand());

            TappedChat = new Command<Chat>(OnChatSelected);

            AddChatCommand = new Command(OnAddChat);
        }

        async Task ExecuteLoadChatsCommand()
        {
            IsBusy = true;
            Chats.Clear();
            var chats = await chatService.GetChatsAsync();
            foreach (var chat in chats)
            {
                Chats.Add(chat);
            }
            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedChat = null;
        }



        private async void OnAddChat(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnChatSelected(Chat chat)
        {
            if (chat == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={chat.Id}");
        }
    }
}
