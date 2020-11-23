using System.Collections.Generic;

namespace dotNET_Chat_Xamarin_Forms_Client.Models.Response
{
    public class AuthenticationResponseModel
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
