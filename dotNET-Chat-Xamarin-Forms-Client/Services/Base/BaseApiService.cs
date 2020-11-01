using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.Services.Base
{
    abstract class BaseApiService
    {
        private readonly IPropertiesService propertiesService;

        public BaseApiService()
        {
            propertiesService = DependencyService.Get<IPropertiesService>();
        }

        protected async Task<T> GetRequest<T>(string route)
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

        protected async Task<T> PostRequest<T>(string route, object requestModel, string notCreatedMessage)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(route),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")
            };
            HttpClient httpClient = GetHttpClient();
            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Not authorized");
            }
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new HttpRequestException(notCreatedMessage);
            }
            var responseString = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<T>(responseString);
            if (responseModel != null)
            {
                return responseModel;
            }
            throw new Exception("Something went wrong");
        }

        protected HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", propertiesService.GetJwtToken());
            return httpClient;
        }
    }
}
