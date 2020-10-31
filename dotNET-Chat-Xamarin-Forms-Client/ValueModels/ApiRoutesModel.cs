using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET_Chat_Xamarin_Forms_Client.ValueModels
{
    public static class ApiRoutesModel
    {
        private const string API = "http://192.168.1.87:49375/api";
        public static class Authentication
        {
            private const string AUTHENTICATION = API + "/authentication";
            public const string Login = AUTHENTICATION + "/login";
            public const string Register = AUTHENTICATION + "/register";
        }
    }
}
