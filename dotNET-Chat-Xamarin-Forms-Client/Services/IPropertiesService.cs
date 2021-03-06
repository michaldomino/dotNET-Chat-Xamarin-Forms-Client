﻿using System.Threading.Tasks;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    interface IPropertiesService
    {
        string? GetJwtToken();
        Task SetJwtTokenAsync(string value);
        
        string? GetUserName();
        Task SetUserNameAsync(string value);
        Task ResetUserAsync();
    }
}
