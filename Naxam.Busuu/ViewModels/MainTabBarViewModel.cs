using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.ViewModel;
using Naxam.Busuu.Notification.ViewModels;
using Naxam.Busuu.Profile.ViewModels;
using Naxam.Busuu.Review.ViewModels;
using Naxam.Busuu.Social.ViewModels;

namespace Naxam.Busuu.ViewModels
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
			ShowViewModel<MainViewModel>();
            ShowViewModel<ReviewViewModel>();
			ShowViewModel<SocialViewModel>();
            ShowViewModel<NotificationViewModel>();
            //ShowViewModel<ProfileViewModel>();
		}
    }
}
