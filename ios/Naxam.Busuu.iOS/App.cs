using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Naxam.Busuu.ViewModels;
using Naxam.Busuu.Notification.Services;
using Naxam.Busuu.Social.Services;
using Naxam.Busuu.Review.Services;
using Naxam.Busuu.Start.ViewModel;
using Naxam.Busuu.Learning.Services;

namespace Naxam.Busuu.iOS
{
	public class App : MvxApplication
	{
		public App()
        {     
            Mvx.RegisterType<ILearningService, LearningService>();
            Mvx.RegisterType<IReviewService, ReviewService>();
            Mvx.RegisterType<IDataSocial, DataSocial>();
			Mvx.RegisterType<IDataNotification, DataNotification>();		
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<StartPageViewModel>());
		}
	}
}
