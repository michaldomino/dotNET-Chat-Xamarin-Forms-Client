﻿using dotNET_Chat_Xamarin_Forms_Client.Models.Response;
using System.Threading.Tasks;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    interface IAuthenticationService
    {
        Task<AuthenticationResponseModel> LoginAsync(string userName, string password);
        Task<AuthenticationResponseModel> RegisterAsync(string userName, string email, string password);
        Task LogoutAsync();
        Task LogInIfValidTokenAsync();
    }
}
