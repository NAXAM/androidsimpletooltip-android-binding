using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Naxam.Busuu.ViewModels;
using Naxam.Busuu.Notification.Services;
using Naxam.Busuu.Social.Services;
using Naxam.Busuu.Review.Services;

namespace Naxam.Busuu.iOS
{
	public class App : MvxApplication
	{
		public App()
        {     
            Mvx.RegisterType<IDataSocial, DataSocial>();
			Mvx.RegisterType<IDataNotification, DataNotification>();
			Mvx.RegisterType<IReviewService, ReviewService>();
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainTabBarViewModel>());
		}
	}
}
