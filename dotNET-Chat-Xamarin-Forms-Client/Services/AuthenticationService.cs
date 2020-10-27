using dotNET_Chat_Xamarin_Forms_Client.Models.Request;
using dotNET_Chat_Xamarin_Forms_Client.Models.Response;
using dotNET_Chat_Xamarin_Forms_Client.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    class AuthenticationService : IAuthenticationService
    {
        public async Task<AuthenticationResponseModel> LoginAsync(string userName, string password)
        {
            HttpClient httpClient = new HttpClient();

            LoginRequestModel loginRequest = new LoginRequestModel
            {
                UserName = userName,
                Password = password
            };
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://192.168.1.87:49375/api/authentication/login"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json")
            };
            var response = await httpClient.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();
            AuthenticationResponseModel responseModel = JsonConvert.DeserializeObject<AuthenticationResponseModel>(responseString);
            if (responseModel != null)
            {
                return responseModel;
            }
            return new AuthenticationResponseModel
            {
                Success = false,
                Errors = new[] { "Request failed" }
            };
        }
    }
}
