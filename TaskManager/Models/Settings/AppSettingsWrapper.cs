using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
