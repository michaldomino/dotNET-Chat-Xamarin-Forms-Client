using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.ValueModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    class ChatsService : IChatsService
    {
        private IPropertiesService propertiesService;

        public ChatsService()
        {
            propertiesService = DependencyService.Get<IPropertiesService>();
        }

        public async Task<List<Chat>> GetChatsAsync()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", propertiesService.GetJwtToken());

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(ApiRoutesModel.ApplicationUsers.Chats),
                Method = HttpMethod.Get,
            };

            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            List<Chat> responseModel = JsonConvert.DeserializeObject<List<Chat>>(responseString);

            if (responseModel != null)
            {
                return responseModel;
            }

            return new List<Chat>();
        }
    }
}
