using System;
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
			Mvx.RegisterType<IDataDiscover, DataDiscover>();
            Mvx.RegisterType<IDataFriends, DataFriends>();
            Mvx.RegisterType<IDataSocialDetail, DataSocialDetail>();
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<SocialDetailViewModel>());
		}
	}
}
