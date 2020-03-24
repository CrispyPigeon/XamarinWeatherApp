using MvvmCross.IoC;
using MvvmCross.ViewModels;
using GismeteoMobileApp.Core.ViewModels.Home;

namespace GismeteoMobileApp.Core
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
