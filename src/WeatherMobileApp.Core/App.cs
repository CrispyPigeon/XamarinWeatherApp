using MvvmCross.IoC;
using MvvmCross.ViewModels;
using WeatherMobileApp.Core.ViewModels.Home;

namespace WeatherMobileApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
