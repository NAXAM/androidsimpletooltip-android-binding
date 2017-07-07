// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Linq;

using Foundation;
using MvvmCross.iOS.Views;
using UIKit;
using ObjCRuntime;
using CoreGraphics;
using Naxam.Busuu.Social.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace Naxam.Busuu.iOS.Social.Views
{
    [MvxFromStoryboard(StoryboardName = "Social")]
    public partial class SocialView : MvxViewController<SocialViewModel>
	{
        private bool IsAnimationViewBar = true;
        private MvxViewController dvView = (MvxViewController)UIStoryboard.FromName("Social", NSBundle.MainBundle).InstantiateViewController("DiscoverView");
        private MvxViewController friView = (MvxViewController)UIStoryboard.FromName("Social", NSBundle.MainBundle).InstantiateViewController("FriendsView");
        //private SocialPageView socialPageView;

		public SocialView (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewBarItem.Layer.ShadowRadius = 2;
			ViewBarItem.Layer.ShadowOffset = new CGSize(2, 2);
			ViewBarItem.Layer.ShadowOpacity = 0.3f;

   //         socialPageView = this.Storyboard.InstantiateViewController("SocialPageView") as SocialPageView;
			//socialPageView.DataSource = new PageViewControllerDataSource();

			//MvxViewController dv = (MvxViewController)UIStoryboard.FromName("Social", NSBundle.MainBundle).InstantiateViewController("DiscoverView");
            //         socialPageView.SetViewControllers(new UIViewController[] { dv }, UIPageViewControllerNavigationDirection.Forward, true, null);

            //         socialPageView.View.Frame = new CGRect(0, 0, ViewContainer.Frame.Size.Width, ViewContainer.Frame.Size.Height);
            dvView.View.Frame = ViewContainer.Bounds;
            friView.View.Frame = ViewContainer.Bounds;
			//AddChildViewController(dvView);
            ViewContainer.AddSubview(dvView.View);
			//dvView.DidMoveToParentViewController(this);

            var setBinding = this.CreateBindingSet<SocialView, SocialViewModel>();
            setBinding.Bind(BarFilterButtonItem).To(vm => vm.PopModalCommand);
			setBinding.Apply();
		}

		partial void ButtonDiscover_TouchUpInside(NSObject sender)
		{
			if (IsAnimationViewBar) return;

			ButtonDiscover.SetTitleColor(UIColor.FromRGB(57, 169, 246), UIControlState.Normal);
			ButtonFriends.SetTitleColor(UIColor.FromRGB(167, 176, 182), UIControlState.Normal);
			ButtonDiscover.Enabled = false;
			ButtonFriends.Enabled = true;

			//MvxViewController dv = (MvxViewController)UIStoryboard.FromName("Social", NSBundle.MainBundle).InstantiateViewController("DiscoverView");
			//socialPageView.SetViewControllers(new UIViewController[] { dv }, UIPageViewControllerNavigationDirection.Forward, false, null);
			//WillMoveToParentViewController(friView);
			//friView.RemoveFromParentViewController();
            ViewContainer.WillRemoveSubview(friView.View);

			//dvView.View.Frame = ViewContainer.Bounds;
			//AddChildViewController(dvView);
			ViewContainer.AddSubview(dvView.View);
			//dvView.DidMoveToParentViewController(this);

			IsAnimationViewBar = true;
			UIView.BeginAnimations("slideAnimation");
			UIView.SetAnimationDuration(0.3);
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("animationDidStop:finished:context:"));
			ViewSelectForButton.Center = new CGPoint(ViewSelectForButton.Bounds.Width / 2, 43);
			UIView.CommitAnimations();
		}

		partial void ButtonFriends_TouchUpInside(NSObject sender)
		{
			if (!IsAnimationViewBar) return;

			ButtonFriends.SetTitleColor(UIColor.FromRGB(57, 169, 246), UIControlState.Normal);
			ButtonDiscover.SetTitleColor(UIColor.FromRGB(167, 176, 182), UIControlState.Normal);
			ButtonDiscover.Enabled = true;
			ButtonFriends.Enabled = false;

            //MvxViewController dv = (MvxViewController)UIStoryboard.FromName("Social", NSBundle.MainBundle).InstantiateViewController("FriendsView");
            //socialPageView.SetViewControllers(new UIViewController[] { dv }, UIPageViewControllerNavigationDirection.Forward, false, null);
            //WillMoveToParentViewController(dvView);
            //dvView.RemoveFromParentViewController();
            ViewContainer.WillRemoveSubview(dvView.View);

            //friView.View.Frame = ViewContainer.Bounds;
			//AddChildViewController(friView);
			ViewContainer.AddSubview(friView.View);
            //friView.DidMoveToParentViewController(this);

			IsAnimationViewBar = false;
			UIView.BeginAnimations("slideAnimation");
			UIView.SetAnimationDuration(0.3);
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("animationDidStop:finished:context:"));
			ViewSelectForButton.Center = new CGPoint(ViewSelectForButton.Bounds.Width + ViewSelectForButton.Bounds.Width / 2, 43);
			UIView.CommitAnimations();
		}

		[Export("animationDidStop:finished:context:")]
		void SlideStopped(NSString animationID, NSNumber finished, NSObject context)
		{
			if (!IsAnimationViewBar)
			{
				ViewSelectForButton.Center = new CGPoint(ViewSelectForButton.Bounds.Width + ViewSelectForButton.Bounds.Width / 2, 43);
			}
			else
			{
				ViewSelectForButton.Center = new CGPoint(ViewSelectForButton.Bounds.Width / 2, 43);
			}
		}
	}

    public class PageViewControllerDataSource : UIPageViewControllerDataSource
	{       
		public PageViewControllerDataSource()
		{
			
		}

		public override UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			if (referenceViewController is DiscoverView) return null;
            return (MvxViewController)UIStoryboard.FromName("Social", NSBundle.MainBundle).InstantiateViewController("DiscoverView");
		}

		public override UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			if (referenceViewController is FriendsView) return null;
			return (MvxViewController)UIStoryboard.FromName("Social", NSBundle.MainBundle).InstantiateViewController("FriendsView");
		}

		public override nint GetPresentationCount(UIPageViewController pageViewController)
		{
			return 2;
		}

		public override nint GetPresentationIndex(UIPageViewController pageViewController)
		{
			return 0;
		}
	}
}
