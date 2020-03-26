using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using DAL.Services.RequestProvider;
using DAL.WeatherApi;
using DBL;
using DBL.Service;
using Model.DbItems;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using WeatherMobileApp.Core.Services.Settings;
using WeatherMobileApp.Core.Services.SqLite;
using WeatherMobileApp.Core.Services.Weather;
using WeatherMobileApp.Core.ViewModels.Home;

namespace WeatherMobileApp.Core
{
    public class App : MvxApplication
    {
        public override async void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            var _ioC = Mvx.IoCProvider;

            InitializeServices(_ioC);
            await InitializeDbAsync(_ioC);

            RegisterAppStart<HomeViewModel>();
        }        

        private void InitializeServices(IMvxIoCProvider provider)
        {
            provider.ConstructAndRegisterSingleton<IRequestProvider, RequestProvider>();
            provider.ConstructAndRegisterSingleton<IOpenWeatherApi, OpenWeatherApi>();
            provider.ConstructAndRegisterSingleton<IWeatherViewService, WeatherViewService>();            
            provider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
        }

        private async Task InitializeDbAsync(IMvxIoCProvider provider)
        {
            ISQLite sqlite = provider.Resolve<ISQLite>();
            var dbContext = new DbAsyncContext(sqlite.GetDatabasePath(Consts.DatabaseName));
            await dbContext.ConnectToDatabase();

            provider.RegisterSingleton<IDbAsyncContext>(dbContext);

            provider.ConstructAndRegisterSingleton<
                IDatabaseService<WeatherDbItem>, DatabaseService<WeatherDbItem, int>>();
            provider.ConstructAndRegisterSingleton<
                IDatabaseService<CountryDbItem>, DatabaseService<CountryDbItem, int>>();
        }
    }
}
