using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Services.Base;
using dotNET_Chat_Xamarin_Forms_Client.ValueModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    class ApplicationUsersService : BaseApiService, IApplicationUsersService
    {
        public async Task<List<ApplicationUser>> GetApplicationUsersAsync()
        {
            return await GetRequest<List<ApplicationUser>>(ApiRoutesModel.ApplicationUsers.Search);
        }

        public async Task<List<Chat>> GetChatsAsync()
        {
            return await GetRequest<List<Chat>>(ApiRoutesModel.ApplicationUsers.Chats);
        }
    }
}
