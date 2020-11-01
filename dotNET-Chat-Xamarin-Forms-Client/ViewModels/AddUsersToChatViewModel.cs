using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.ViewModels
{
    [QueryProperty(nameof(ChatId), nameof(ChatId))]
    class AddUsersToChatViewModel : BaseViewModel
    {
        private readonly IApplicationUsersService applicationUsersService;
        private readonly IChatsService chatsService;
        private readonly IDialogService dialogService;
        private Guid chatId;

        public ObservableCollection<SelectableUser> UsersToSelect { get; }
        
        public Command LoadUsersCommand { get; }
        public Command AddUsersCommand { get; }

        public string ChatId
        {
            get => chatId.ToString();
            set => chatId = Guid.Parse(value);
        }
        
        
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
            applicationUsersService = DependencyService.Get<IApplicationUsersService>();
            chatsService = DependencyService.Get<IChatsService>();
            dialogService = DependencyService.Get<IDialogService>();

            Title = "Add users";
            UsersToSelect = new ObservableCollection<SelectableUser>();
            AddUsersCommand = new Command(OnAddUsers);
            LoadUsersCommand = new Command(async () => await ExecuteLoadUsersCommand());

            //TappedChat = new Command<Chat>(OnChatSelected);

            //AddChatCommand = new Command(OnAddChat);
        }

        private async Task ExecuteLoadUsersCommand()
        {
            IsBusy = true;
            try
            {
                UsersToSelect.Clear();
                var users = await applicationUsersService.GetApplicationUsersAsync();
                List<ApplicationUser> chatMembers = await chatsService.GetChatMembers(chatId);
                var usersToSelect = users.Except(chatMembers, new ApplicationUserComparer()).Select(it => new SelectableUser
                {
                    Id = it.Id,
                    UserName = it.UserName,
                    IsSelected = false
                });
                foreach (var userToSelect in usersToSelect)
                {
                    UsersToSelect.Add(userToSelect);
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
