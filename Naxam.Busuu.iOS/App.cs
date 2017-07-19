using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Naxam.Busuu.Core.ViewModels;
using Naxam.Busuu.Notification.Serveices;
using Naxam.Busuu.Social.Serveices;
using Naxam.Busuu.Review.Services;
using Naxam.Busuu.Review.ViewModels;

namespace Naxam.Busuu.iOS
{
	public class App : MvxApplication
	{
		public App()
		{

            Mvx.RegisterType<IDataSocial, DataSocial>();
			Mvx.RegisterType<IDataNotification, DataNotification>();
			Mvx.RegisterType<IReviewService, ReviewService>();

            //Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainTabBarViewModel>());
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<ReviewAllViewModel>());
		}
	}
}
