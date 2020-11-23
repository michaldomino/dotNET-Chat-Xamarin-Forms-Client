using dotNET_Chat_Xamarin_Forms_Client.Models.Request;
using dotNET_Chat_Xamarin_Forms_Client.Models.Response;
using dotNET_Chat_Xamarin_Forms_Client.ValueModels;
using dotNET_Chat_Xamarin_Forms_Client.Views;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly IPropertiesService propertiesService;
        private readonly IDialogService dialogService;
        private readonly IApplicationUsersService applicationUsersService;

        public AuthenticationService()
        {
            propertiesService = DependencyService.Get<IPropertiesService>();
            dialogService = DependencyService.Get<IDialogService>();
            applicationUsersService = DependencyService.Get<IApplicationUsersService>();
        }

        public async Task<AuthenticationResponseModel> LoginAsync(string userName, string password)
        {
            LoginRequestModel loginRequest = new LoginRequestModel
            {
                UserName = userName,
                Password = password
            };
            return await GetResponse(loginRequest, ApiRoutesModel.Authentication.Login);
        }

        public async Task<AuthenticationResponseModel> RegisterAsync(string userName, string email, string password)
        {
            RegisterRequestModel loginRequest = new RegisterRequestModel
            {
                UserName = userName,
                Email = email,
                Password = password
            };
            return await GetResponse(loginRequest, ApiRoutesModel.Authentication.Register);
        }

        public async Task LogoutAsync()
        {
            await propertiesService.ResetUserAsync();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private static async Task<AuthenticationResponseModel> GetResponse(object requestModel, string route)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(route),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")
            };

            HttpClient httpClient = new HttpClient();
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

        public async Task LogInIfValidTokenAsync()
        {
            bool isTokenValid = await IsTokenValid();
            if (isTokenValid)
            {
                await Shell.Current.GoToAsync($"//{nameof(ChatsPage)}");
            }
        }

        private async Task<bool> IsTokenValid()
        {
            string token = propertiesService.GetJwtToken();
            string userName = propertiesService.GetUserName();
            string[] userProperties = { token, userName };
            if (userProperties.Any(it => string.IsNullOrEmpty(it)))
            {
                return false;
            }
            try
            {
                await applicationUsersService.GetApplicationUsersAsync();
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            return true;
        }
    }
}
