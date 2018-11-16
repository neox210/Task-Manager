#region

using System.Windows;
using CommonServiceLocator;
using TaskManager.Models.Settings;
using Unity.ServiceLocation;

#endregion

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = IocConfiguration.GetConteiner();
            var locator = new UnityServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
