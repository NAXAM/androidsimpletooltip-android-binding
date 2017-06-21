﻿using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Review.ViewModels;
using UIKit;
using ObjCRuntime;
using Foundation;

namespace Naxam.Busuu.iOS.Review.Views
{
    public partial class ReviewAllView : MvxViewController<ReviewAllViewModel>, IUITableViewDataSource
    {
        CGPoint oriPoint;
        bool IsDiscovery = true;

        public ReviewAllView() : base("ReviewAllView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            NavigationItem.TitleView = uiViewButton;

            ReviewTableView.RegisterNibForCellReuse(ReviewTableViewCell.Nib, "reviewCell");
            ReviewTableView.WeakDataSource = this;
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

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return 3;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = ReviewTableView.DequeueReusableCell("reviewCell", indexPath);
            cell.BackgroundColor = UIColor.Black;
            return cell;
        }
    }
}

