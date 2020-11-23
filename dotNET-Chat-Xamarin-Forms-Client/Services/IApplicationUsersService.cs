using dotNET_Chat_Xamarin_Forms_Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    interface IApplicationUsersService
    {
        Task<List<Chat>> GetChatsAsync();
        Task<List<ApplicationUser>> GetApplicationUsersAsync();
    }
}
