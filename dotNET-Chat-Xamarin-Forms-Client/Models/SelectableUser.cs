using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET_Chat_Xamarin_Forms_Client.Models
{
    public class SelectableUser
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public bool IsSelected { get; set; }
    }
}
