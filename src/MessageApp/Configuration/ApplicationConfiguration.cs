using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace MessageApp.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationConfiguration : IConfiguration
    {
        NameValueCollection AppSettings;

        public ApplicationConfiguration()
        {
            AppSettings = ConfigurationManager.AppSettings;
        }

        public string GetValue(string key)
        {
            if (AppSettings.AllKeys.Contains(key))
            {
                return AppSettings[key];
            }

            return string.Empty;
        }
    }
}
