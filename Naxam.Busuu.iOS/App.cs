using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Naxam.Busuu.Review.ViewModels;

namespace Naxam.Busuu.iOS
{
	public class App : MvxApplication
	{
		public App()
		{
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<ReviewAllViewModel>());
		}
	}
}
