using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.ValueModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    class ChatsService : IChatsService
    {
        private readonly IPropertiesService propertiesService;

        public ChatsService()
        {
            propertiesService = DependencyService.Get<IPropertiesService>();
        }

        public async Task<Chat> CreateChatAsync(Chat chat)
        {
            HttpClient httpClient = GetHttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(ApiRoutesModel.Chats.Value),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(chat), Encoding.UTF8, "application/json")
            };
            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Not authorized");
            }    
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new HttpRequestException("Chat not created");
            }
            var responseString = await response.Content.ReadAsStringAsync();
            Chat responseModel = JsonConvert.DeserializeObject<Chat>(responseString);
            if (responseModel != null)
            {
                return responseModel;
            }
            throw new Exception("Something went wrong");
        }

        public async Task<List<Chat>> GetChatsAsync()
        {
            return await GetRequest<List<Chat>>(ApiRoutesModel.ApplicationUsers.Chats);
        }

        public async Task<List<Message>> GetMessagesAsync(Guid chatId)
        {
            return await GetRequest<List<Message>>(ApiRoutesModel.Chats.GetMessages(chatId));
        }

        private async Task<T> GetRequest<T>(string route)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(route),
                Method = HttpMethod.Get,
            };
            HttpClient httpClient = GetHttpClient();
            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Not authorized");
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException("Wrong request");
            }
            var responseString = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<T>(responseString);
            if (responseModel != null)
            {
                return responseModel;
            }
            throw new Exception("Something went wrong");
        }

        private HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", propertiesService.GetJwtToken());
            return httpClient;
        }
    }
}
