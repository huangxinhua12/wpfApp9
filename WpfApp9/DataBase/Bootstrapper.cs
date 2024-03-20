using System.Windows;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;

namespace WpfApp9.DataBase
{
    public class MyBootstrapper : PrismApplication    
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow, YourViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
