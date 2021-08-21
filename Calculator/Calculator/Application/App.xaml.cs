using Autofac;
using Calculator.Common.Navigation;
using Calculator.Modules.Calculator;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //class used for registration
            var builder = new ContainerBuilder();
            //scan and register all classes in the assembly
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                   .AsImplementedInterfaces()
                   .AsSelf();

            NavigationPage navigationPage = null;

            Func<INavigation> navigationFunc = () => {
                return navigationPage.Navigation;
            };

            builder.RegisterType<NavigationService>().As<INavigationService>()
                .WithParameter("navigation", navigationFunc);

            //get container
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var service = container.Resolve<CalculatorView>();
                navigationPage = new NavigationPage(service);
                MainPage = navigationPage;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
