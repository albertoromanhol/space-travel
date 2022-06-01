using Caliburn.Micro;
using SpaceTravel.WPF.ViewModels;
using System.Windows;

namespace SpaceTravel.WPF
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<SpaceTravelViewModel>();
        }
    }
}
