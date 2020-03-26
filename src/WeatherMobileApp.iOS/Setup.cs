using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;
using WeatherMobileApp.Core.Services.SqLite;
using WeatherMobileApp.iOS.Services.SQLite;

namespace WeatherMobileApp.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, UI.App>
    {
        protected override void InitializePlatformServices()
        {
            base.InitializePlatformServices();

            IMvxIoCProvider ioC = Mvx.IoCProvider;

            ioC.RegisterSingleton<ISQLite>(new SQLiteService());
        }
    }
}
