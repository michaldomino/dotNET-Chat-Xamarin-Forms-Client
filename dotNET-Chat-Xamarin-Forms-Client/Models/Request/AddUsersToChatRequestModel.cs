using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET_Chat_Xamarin_Forms_Client.Models.Request
{
    class AddUsersToChatRequestModel
    {
        public ICollection<Guid> UsersIds { get; set; }
    }
}
