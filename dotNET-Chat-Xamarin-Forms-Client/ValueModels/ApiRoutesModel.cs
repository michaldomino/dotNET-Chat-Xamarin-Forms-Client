using System;

namespace dotNET_Chat_Xamarin_Forms_Client.ValueModels
{
    public static class ApiRoutesModel
    {
        private const string API = "http://192.168.1.4:49375/api";
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
            public const string Search = APPLICTAION_USERS + "/Search";
        }

        public static class Chats
        {
            private const string CHATS = API + "/Chats";
            public const string Value = CHATS;
            public static string AddUsers(Guid chatId) => $"{CHATS}/AddUsers/{chatId}";
            public static string GetMembers(Guid chatId) => $"{CHATS}/GetMembers/{chatId}";
            public static string GetMessages(Guid chatId) => $"{CHATS}/GetMessages/{chatId}";
            public static string SendMessage(Guid chatId) => $"{CHATS}/SendMessage/{chatId}";
        }
    }
}
