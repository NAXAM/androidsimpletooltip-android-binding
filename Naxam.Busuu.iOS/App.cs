using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Naxam.Busuu.Social.Serveices;
using Naxam.Busuu.Social.ViewModels;

namespace Naxam.Busuu.iOS
{
	public class App : MvxApplication
	{
		public App()
		{
			Mvx.RegisterType<IDataSocial, DataSocial>();
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<SocialViewModel>());
		}
	}
}
