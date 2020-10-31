using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET_Chat_Xamarin_Forms_Client.Models
{
    class Chat
    {
        public Chat()
        {
            Messages = new HashSet<Message>();
            ApplicationUsers = new HashSet<ApplicationUser>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
