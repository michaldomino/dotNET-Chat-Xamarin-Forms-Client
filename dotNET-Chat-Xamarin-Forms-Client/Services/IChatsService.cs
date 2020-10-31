using dotNET_Chat_Xamarin_Forms_Client.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    interface IChatsService
    {
        Task<List<Chat>> GetChatsAsync();
    }
}
