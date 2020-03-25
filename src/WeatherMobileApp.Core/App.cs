using DAL.Services.RequestProvider;
using DAL.WeatherApi;
using MvvmCross;
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

            Mvx.IoCProvider.ConstructAndRegisterSingleton<IRequestProvider, RequestProvider>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IOpenWeatherApi, OpenWeatherApi>();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
