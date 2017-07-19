using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Naxam.Busuu.Core.ViewModels;
using Naxam.Busuu.Notification.Services;
using Naxam.Busuu.Social.Services;
using Naxam.Busuu.Social.ViewModels;

namespace Naxam.Busuu.iOS
{
	public class App : MvxApplication
	{
		public App()
		{          
            Mvx.RegisterType<IDataSocial, DataSocial>();
			Mvx.RegisterType<IDataNotification, DataNotification>();
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainTabBarViewModel>());
		}
	}
}
