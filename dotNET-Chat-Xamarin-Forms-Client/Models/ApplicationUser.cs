using System;
using System.Collections.Generic;

namespace dotNET_Chat_Xamarin_Forms_Client.Models
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
    }

    public class ApplicationUserComparer : IEqualityComparer<ApplicationUser>
    {
        public bool Equals(ApplicationUser x, ApplicationUser y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(ApplicationUser obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
