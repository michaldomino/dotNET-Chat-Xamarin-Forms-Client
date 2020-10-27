using dotNET_Chat_Xamarin_Forms_Client.Models;
using dotNET_Chat_Xamarin_Forms_Client.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string userName;
        private string password;

        public Command LoginCommand { get; }

        public string UserName
        {
            get => userName;
            set
            {
                SetProperty(ref userName, value);
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password; 
            set
            {
                SetProperty(ref password, value);
                OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            HttpClient httpClient = new HttpClient();

            LoginRequestModel loginRequest = new LoginRequestModel
            {
                UserName = "test",
                Password = "Pswd123_"
            };
            var a = JsonConvert.SerializeObject(loginRequest);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://192.168.1.87:49375/api/authentication/login"),
                Method = HttpMethod.Post,
                Content = new StringContent(a, Encoding.UTF8, "application/json")
            };
            //WebRequest webRequest = WebRequest.Create("http://192.168.1.87:49375/api/applicationusers/search");
            //webRequest.Method = "GET";
            ////webRequest.Headers =
            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                //var a = 5;
            }
            //WebResponse a = webRequest.GetResponse();
            //a.Con
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
