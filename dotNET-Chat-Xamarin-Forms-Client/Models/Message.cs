using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET_Chat_Xamarin_Forms_Client.Models
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid AuthorId { get; set; }
    }
}
