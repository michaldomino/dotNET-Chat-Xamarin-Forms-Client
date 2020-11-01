using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET_Chat_Xamarin_Forms_Client.Models.Response
{
    class AddUsersToChatResponseModel
    {
        public ICollection<ApplicationUser> CurrentChatMembers { get; set; }
    }
}
