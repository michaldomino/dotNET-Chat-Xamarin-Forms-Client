using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.ViewModels
{
    [QueryProperty(nameof(ChatId), nameof(ChatId))]
    class AddUsersToChatViewModel : BaseViewModel
    {
        private readonly IChatsService chatService;
        private readonly IDialogService dialogService;
        private Guid chatId;

        public ObservableCollection<SelectableUser> Users { get; }
        public Command AddUsersCommand { get; }

        public string ChatId
        {
            get => chatId.ToString();
            set => chatId = Guid.Parse(value);
        }
        //public Command LoadChatsCommand { get; }
        //public Command AddChatCommand { get; }
        //public Command<Chat> TappedChat { get; }

        //public Chat SelectedUsers
        //{
        //    get => selectedChat;
        //    set
        //    {
        //        SetProperty(ref selectedChat, value);
        //        OnChatSelected(value);
        //    }
        //}

        public AddUsersToChatViewModel()
        {
            chatService = DependencyService.Get<IChatsService>();
            dialogService = DependencyService.Get<IDialogService>();

            Title = "Add users";
            Users = new ObservableCollection<SelectableUser>();
            AddUsersCommand = new Command(OnAddUsers);
            //LoadChatsCommand = new Command(async () => await ExecuteLoadChatsCommand());

            //TappedChat = new Command<Chat>(OnChatSelected);

            //AddChatCommand = new Command(OnAddChat);
        }

        //private async Task ExecuteLoadChatsCommand()
        //{
        //    IsBusy = true;
        //    try
        //    {
        //        Chats.Clear();
        //        var chats = await chatService.GetChatsAsync();
        //        foreach (var chat in chats)
        //        {
        //            Chats.Add(chat);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        await dialogService.ShowAlert(e.Message);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        public void OnAppearing()
        {
            IsBusy = true;
            //SelectedUsers = null;
        }

        private async void OnAddUsers()
        {
            throw new NotImplementedException();
        }

        //private async void OnAddChat(object obj)
        //{
        //    await Shell.Current.GoToAsync(nameof(NewChatPage));
        //}

        //async void OnChatSelected(Chat chat)
        //{
        //    if (chat == null)
        //        return;

        //    // This will push the ItemDetailPage onto the navigation stack
        //    await Shell.Current.GoToAsync($"{nameof(ChatMessagesPage)}?{nameof(ChatMessagesViewModel.ChatId)}={chat.Id}");
        //}
    }
}
