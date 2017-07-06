using System;
using CoreGraphics;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Review.ViewModels;
using UIKit;
using ObjCRuntime;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;

namespace Naxam.Busuu.iOS.Review.Views
{
    [MvxFromStoryboard(StoryboardName = "Review")]
    public partial class ReviewAllView : MvxViewController<ReviewAllViewModel>
    {
        public ReviewAllView(IntPtr handle): base(handle)
        {
        }

        CGPoint oriPoint;
        bool IsDiscovery = true;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            NavigationItem.TitleView = uiViewButton;
            ReviewTableView.RowHeight = 60;
            var source = new MvxStandardTableViewSource(ReviewTableView,(NSString)"reviewCell");
            ReviewTableView.Source = source;

            var set = this.CreateBindingSet<ReviewAllView, ReviewAllViewModel>();
            set.Bind(source).To(vm=>vm.Reviews);
            set.Apply();
            ReviewTableView.ReloadData();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
			uiViewButton.Layer.CornerRadius = uiViewButton.Bounds.Height / 2;
			uiViewSlide.Layer.CornerRadius = uiViewSlide.Bounds.Height / 2;
			btnDiscovery.SetTitleColor(UIColor.White, UIControlState.Normal);
			oriPoint = uiViewSlide.Center;
        }

        partial void btnDiscovery_TouchUpInside(NSObject sender)
        {
			if (IsDiscovery) return;
			IsDiscovery = true;
			UIView.BeginAnimations("slideAnimation");
			UIView.SetAnimationDuration(0.2);
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("animationDidStop:finished:context:"));
			uiViewSlide.Center = new CGPoint(oriPoint.X, oriPoint.Y);
			UIView.CommitAnimations();
			lbButtonClicked.Text = "";
        }

        partial void btnFriends_TouchUpInside(NSObject sender)
        {
			if (!IsDiscovery) return;
			IsDiscovery = false;
			UIView.BeginAnimations("slideAnimation");
			UIView.SetAnimationDuration(0.2);
			UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("animationDidStop:finished:context:"));
			uiViewSlide.Center = new CGPoint(oriPoint.X + uiViewSlide.Bounds.Width , oriPoint.Y);
			UIView.CommitAnimations();
			lbButtonClicked.Text = "";
        }

		[Export("animationDidStop:finished:context:")]
		void SlideStopped(NSString animationID, NSNumber finished, NSObject context)
		{
            if (!IsDiscovery) { 
                uiViewSlide.Center = new CGPoint(oriPoint.X + uiViewSlide.Bounds.Width, oriPoint.Y); 
                lbButtonClicked.Text = "Friends";
            }
            else
            {
                uiViewSlide.Center = new CGPoint(oriPoint.X, oriPoint.Y);
                lbButtonClicked.Text = "Discovery";
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

