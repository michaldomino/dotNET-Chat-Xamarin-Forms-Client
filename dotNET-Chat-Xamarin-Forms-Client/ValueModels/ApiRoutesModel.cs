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
            private const string AUTHENTICATION = API + "/Authentication";
            public const string Login = AUTHENTICATION + "/Login";
            public const string Register = AUTHENTICATION + "/Register";
        }

        public static class ApplicationUsers
        {
            private const string APPLICTAION_USERS = API + "/ApplicationUsers";
            public const string Chats = APPLICTAION_USERS + "/Chats";
        }

        public static class Chats
        {
            private const string CHATS = API + "/Chats";
            public const string Value = CHATS;
            public static string GetMessages(Guid chatId) => $"{CHATS}/GetMessages/{chatId}";
        }
    }
}
