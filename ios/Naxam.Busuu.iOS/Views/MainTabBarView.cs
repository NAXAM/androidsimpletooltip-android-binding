// This file has been autogenerated from a class added in the UI designer.

using System;

using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Naxam.Busuu.Core.ViewModels;
using UIKit;

namespace Naxam.Busuu.iOS.Views
{
    [MvxFromStoryboard(StoryboardName = "Main")]
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class MainTabBarView : MvxTabBarViewController<MainTabBarViewModel>
	{
        bool _isPresentedFirstTime = true;

		public MainTabBarView (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);


			if (ViewModel != null && _isPresentedFirstTime)
			{               
				_isPresentedFirstTime = false;
				ViewModel.ShowInitialViewModelsCommand.Execute(null);	

                //this.CreateBinding(TabBar.Items[1]).For(vm => vm.BadgeValue).To<NotificationViewModel>(vm => vm.NotificationCount).Apply();
                TabBar.Items[1].BadgeValue = "9";
			}
		}	

	}
}
