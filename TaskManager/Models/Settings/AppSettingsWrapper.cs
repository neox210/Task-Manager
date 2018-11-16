using System.Collections.Specialized;
using System.Configuration;

namespace TaskManager.Models.Settings
{
    public class AppSettingsWrapper : IAppSettings
    {
        private readonly NameValueCollection _appSettings;

        public AppSettingsWrapper()
        {
            this._appSettings = ConfigurationManager.AppSettings;
        }
        public string this[string key] => this._appSettings[key];
    }
}
