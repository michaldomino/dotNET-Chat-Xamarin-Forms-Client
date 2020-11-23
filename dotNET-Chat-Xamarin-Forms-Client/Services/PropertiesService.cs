using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotNET_Chat_Xamarin_Forms_Client.Services
{
    class PropertiesService : IPropertiesService
    {
        private const string TOKEN_KEY = "token";
        private const string USER_NAME_KEY = "userName";
        
        private object? GetPropertyValue(string key)
        {
            if (Application.Current.Properties.ContainsKey(key))
            {
                return Application.Current.Properties[key] as string;
            }
            return null;
        }

        private async Task SetPropertyValueAsync(string key, object value)
        {
            Application.Current.Properties[key] = value;
            await Application.Current.SavePropertiesAsync();
        }

        public string? GetJwtToken()
        {
            return GetPropertyValue(TOKEN_KEY) as string;
        }

        public async Task SetJwtTokenAsync(string value)
        {
            await SetPropertyValueAsync(TOKEN_KEY, value);
        }

        public string? GetUserName()
        {
            return GetPropertyValue(USER_NAME_KEY) as string;
        }

        public async Task SetUserNameAsync(string value)
        {
            await SetPropertyValueAsync(USER_NAME_KEY, value);
        }

        public async Task ResetUserAsync()
        {
            await SetJwtTokenAsync(null);
            await SetUserNameAsync(null);
        }
    }
}
