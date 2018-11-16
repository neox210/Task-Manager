#region

using TaskManager.Persisistence;
using TaskManager.Repository;
using TaskManager.ViewModels.Dialogs;
using Unity;

#endregion

namespace TaskManager.Models.Settings
{
    public static class IocConfiguration
    {
        public static IUnityContainer GetConteiner()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IDialogHelper, DialogHelper>();
            container.RegisterType<IAppSettings, AppSettingsWrapper>();

            return container;
        }
    }
}
