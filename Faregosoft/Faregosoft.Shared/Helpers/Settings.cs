using Faregosoft.Models;
using Newtonsoft.Json;
using Windows.Storage;

namespace Faregosoft.Helpers
{
    public class Settings
    {
        private static readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        public static string GetApiUrl()
        {
            return (string)_localSettings.Values["ApiUrl"];
        }

        public static TokenResponse GetToken()
        {
            string tokenString = (string)_localSettings.Values["Token"];
            if (string.IsNullOrEmpty(tokenString))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<TokenResponse>(tokenString);
        }

        public static void SaveToken(TokenResponse token)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string tokenString = JsonConvert.SerializeObject(token);
            localSettings.Values["Token"] = tokenString;
        }
    }
}
