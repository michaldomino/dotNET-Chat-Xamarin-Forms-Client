﻿using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Models.Request;
using dotNET_Chat_Xamarin_Forms_Client.Models.Response;
using dotNET_Chat_Xamarin_Forms_Client.Services.Base;
using dotNET_Chat_Xamarin_Forms_Client.ValueModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    class ChatsService : BaseApiService, IChatsService
    {
        public async Task<AddUsersToChatResponseModel> AddUsersToChatAsync(Guid chatId, AddUsersToChatRequestModel requestModel)
        {
            return await PostRequest<AddUsersToChatResponseModel>(
                ApiRoutesModel.Chats.AddUsers(chatId),
                requestModel,
                "Unable to add users",
                HttpStatusCode.OK);
        }

        public async Task<Chat> CreateChatAsync(Chat chat)
        {
            return await PostRequest<Chat>(ApiRoutesModel.Chats.Value, chat, "Chat not created", HttpStatusCode.Created);
        }

        public async Task<List<ApplicationUser>> GetChatMembers(Guid chatId)
        {
            return await GetRequest<List<ApplicationUser>>(ApiRoutesModel.Chats.GetMembers(chatId));
        }

        public async Task<List<Message>> GetMessagesAsync(Guid chatId)
        {
            List<Message> messages = await GetRequest<List<Message>>(ApiRoutesModel.Chats.GetMessages(chatId));
            messages.ForEach(it => it.CreationTime = TimeZoneInfo.ConvertTimeFromUtc(it.CreationTime, TimeZoneInfo.Local));
            return messages;
        }

        public async Task<Message> SendMessageAsync(Guid chatId, NewMessageRequestModel requestModel)
        {
            return await PostRequest<Message>(
                ApiRoutesModel.Chats.SendMessage(chatId),
                requestModel,
                "Message not sent",
                HttpStatusCode.Created);
        }
    }
}
