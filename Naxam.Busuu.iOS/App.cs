using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Naxam.Busuu.Review.Services;
using Naxam.Busuu.Review.ViewModels;

namespace Naxam.Busuu.iOS
{
	public class App : MvxApplication
	{
		public App()
		{
            Mvx.RegisterType<IReviewService, ReviewService>();
          //  Mvx.RegisterType<IDataSocial, DataSocial>();
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<ReviewAllViewModel>());
            //Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<SocialViewModel>());
		}
	}
}
