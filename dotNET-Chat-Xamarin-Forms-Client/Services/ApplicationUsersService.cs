﻿using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Services.Base;
using dotNET_Chat_Xamarin_Forms_Client.ValueModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    class ApplicationUsersService : BaseApiService, IApplicationUsersService
    {
        public async Task<List<Chat>> GetChatsAsync()
        {
            return await GetRequest<List<Chat>>(ApiRoutesModel.ApplicationUsers.Chats);
        }
    }
}
