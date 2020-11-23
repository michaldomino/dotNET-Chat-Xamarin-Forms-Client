using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Models.Request;
using dotNET_Chat_Xamarin_Forms_Client.Models.Response;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly IAuthenticationService authenticationService;
        private Guid chatId;

        public ObservableCollection<SelectableUser> UsersToSelect { get; }
        
        public Command LoadUsersCommand { get; }
        public Command AddUsersCommand { get; }
        public Command CancelCommand { get; }
        public Command CheckBoxChangedCommand { get; }

        public string ChatId
        {
            get => chatId.ToString();
            set => chatId = Guid.Parse(value);
        }

        public AddUsersToChatViewModel()
        {
            applicationUsersService = DependencyService.Get<IApplicationUsersService>();
            chatsService = DependencyService.Get<IChatsService>();
            dialogService = DependencyService.Get<IDialogService>();
            authenticationService = DependencyService.Get<IAuthenticationService>();

            Title = "Add users";
            UsersToSelect = new ObservableCollection<SelectableUser>();
            AddUsersCommand = new Command(OnAddUsers, ValidateSave);
            LoadUsersCommand = new Command(async () => await ExecuteLoadUsersCommand());
            CancelCommand = new Command(OnCancel);
            CheckBoxChangedCommand = new Command(() => OnPropertyChanged());
            PropertyChanged +=
                (_, __) => AddUsersCommand.ChangeCanExecute();
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
            catch (UnauthorizedAccessException e)
            {
                await dialogService.ShowAlert(e.Message);
                await authenticationService.LogoutAsync();
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

        public void OnCheckBoxChanged()
        {
            OnPropertyChanged();
        }

        private async void OnAddUsers()
        {
            var selectedUsersIds = UsersToSelect.Where(it => it.IsSelected).Select(it => it.Id);
            AddUsersToChatRequestModel requestModel = new AddUsersToChatRequestModel
            {
                UsersIds = selectedUsersIds.ToList()
            };
            AddUsersToChatResponseModel responseModel = await chatsService.AddUsersToChatAsync(chatId, requestModel);
            await GoToPreviousPageAsync();
        }

        private async void OnCancel()
        {
            await GoToPreviousPageAsync();
        }

        private bool ValidateSave()
        {
            return UsersToSelect.Any(it => it.IsSelected);
        }

        private async Task GoToPreviousPageAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
