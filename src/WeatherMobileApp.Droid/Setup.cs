using Acr.UserDialogs;
using Android.App;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android;
using WeatherMobileApp.Core.Services.SqLite;
using WeatherMobileApp.Droid.Services.SQLite;

#if DEBUG
[assembly: Application(Debuggable = true)]
#else
[assembly: Application(Debuggable = false)]
#endif

namespace WeatherMobileApp.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, UI.App>
    {
        protected override void InitializePlatformServices()
        {
            base.InitializePlatformServices();

            IMvxIoCProvider ioC = Mvx.IoCProvider;

            ioC.RegisterSingleton<ISQLite>(new SQLiteService());
            UserDialogs.Init(() => ioC
                .Resolve<IMvxAndroidCurrentTopActivity>().Activity);
        }
    }
}
