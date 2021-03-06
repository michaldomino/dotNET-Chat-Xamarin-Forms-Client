﻿using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Models.Request;
using dotNET_Chat_Xamarin_Forms_Client.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    interface IChatsService
    {
        Task<Chat> CreateChatAsync(Chat chat);
        Task<List<Message>> GetMessagesAsync(Guid chatId);
        Task<Message> SendMessageAsync(Guid chatId, NewMessageRequestModel requestModel);
        Task<List<ApplicationUser>> GetChatMembers(Guid chatId);
        Task<AddUsersToChatResponseModel> AddUsersToChatAsync(Guid chatId, AddUsersToChatRequestModel requestModel);
    }
}
