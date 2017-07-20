using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Notification.ViewModels;
using Naxam.Busuu.Profile.ViewModel;
using Naxam.Busuu.Social.ViewModels;


namespace Naxam.Busuu.Core.ViewModels
{
    public class MainTabBarViewModel : MvxViewModel
    {
		IMvxCommand _showInitialViewModelsCommand;
		public IMvxCommand ShowInitialViewModelsCommand
		{
			get
			{
				return _showInitialViewModelsCommand ?? (_showInitialViewModelsCommand = new MvxCommand(ShowInitialViewModels));
			}
		}

		void ShowInitialViewModels()
		{
            ShowViewModel<Naxam.Busuu.Review.ViewModels.ReviewAllViewModel>();
			ShowViewModel<SocialViewModel>();
            ShowViewModel<NotificationViewModel>();
            ShowViewModel<RegisterViewModel>();
		}
    }
}
