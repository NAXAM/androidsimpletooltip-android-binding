using System;
using CoreGraphics;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Review.ViewModels;
using UIKit;
using ObjCRuntime;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using Naxam.Busuu.Review.Models;
using System.ComponentModel;
using MvvmCross.Core.ViewModels;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Naxam.Ausuu.IOS.Review.Floaty;
using Naxam.Busuu.IOS.Review.Floaty;

namespace Naxam.Busuu.iOS.Review.Views
{
    [MvxFromStoryboard(StoryboardName = "Review")]
    public partial class ReviewAllView : MvxViewController<ReviewAllViewModel>, IUITableViewDataSource
    {
        public ReviewAllView(IntPtr handle) : base(handle)
        {
        }

        CGPoint oriPoint;
        bool isAll = true;
        List<ReviewModel> AllReviews, FavoriteReviews;
        ActionButton actionButton;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var btnContinueLearning = new ActionButtonItem("CONTINUE LEARNING", UIImage.FromBundle("fab_menu_row_learning"), UIColor.White);
            btnContinueLearning.ActionPerform = HandleAction;

            var btnAll = new ActionButtonItem("TEST ALL", UIImage.FromBundle("fab_menu_row_all"), UIColor.Gray);
            btnAll.ActionPerform = HandleAction;

			var btnStrength = new ActionButtonItem("STRENGTHEN VOCABULARY", UIImage.FromBundle("fab_menu_row_weak"), UIColor.Gray);
			btnStrength.ActionPerform = HandleAction;

			var btnTestFavorite = new ActionButtonItem("TEST FAVORITES", UIImage.FromBundle("fab_menu_row_fav"), UIColor.Gray);
			btnTestFavorite.ActionPerform = HandleAction;

            actionButton = new ActionButton(this.View, new[] { btnContinueLearning, btnAll, btnStrength, btnTestFavorite });
            actionButton.Action = delegate {
                actionButton.ToggleMenu();
            };
            actionButton.SetTitle("+", UIControlState.Normal);

            actionButton.BackgroundColor = UIColor.Orange;

            // Perform any additional setup after loading the view, typically from a nib.
         
            this.NavigationItem.TitleView = uiViewButton;
            ReviewTableView.RowHeight = 60;

            AllReviews = this.ViewModel.Reviews;
            FavoriteReviews = this.ViewModel.FavoriteReviews;
            ReviewTableView.WeakDataSource = this;
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            uiViewButton.Layer.CornerRadius = uiViewButton.Bounds.Height / 2;
            uiViewSlide.Layer.CornerRadius = uiViewSlide.Bounds.Height / 2;
            btnAll.SetTitleColor(UIColor.White, UIControlState.Normal);
            oriPoint = uiViewSlide.Center;
        }

        void HandleAction(Ausuu.IOS.Review.Floaty.ActionButtonItem obj)
        {
			UIAlertView alert = new UIAlertView()
			{
				Title = "alert title",
				Message = "this is a simple alert"
			};
			alert.AddButton("OK");
			alert.Show();
        }

        partial void btnAll_TouchUpInside(NSObject sender)
        {
            if (isAll) return;
            isAll = true;
            ReviewTableView.ReloadData();

            UIView.BeginAnimations("slideAnimation");
            UIView.SetAnimationDuration(0.2);
            UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
            UIView.SetAnimationDelegate(this);
            UIView.SetAnimationDidStopSelector(new Selector("animationDidStop:finished:context:"));
            uiViewSlide.Center = new CGPoint(oriPoint.X, oriPoint.Y);
            UIView.CommitAnimations();
            lbButtonClicked.Text = "";
        }

        partial void btnFavorite_TouchUpInside(NSObject sender)
        {
            if (!isAll) return;
            isAll = false;
            ReviewTableView.ReloadData();

            UIView.BeginAnimations("slideAnimation");
            UIView.SetAnimationDuration(0.2);
            UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
            UIView.SetAnimationDelegate(this);
            UIView.SetAnimationDidStopSelector(new Selector("animationDidStop:finished:context:"));
            uiViewSlide.Center = new CGPoint(oriPoint.X + uiViewSlide.Bounds.Width, oriPoint.Y);
            UIView.CommitAnimations();
            lbButtonClicked.Text = "";
        }

        [Export("animationDidStop:finished:context:")]
        void SlideStopped(NSString animationID, NSNumber finished, NSObject context)
        {
            if (!isAll)
            {
                uiViewSlide.Center = new CGPoint(oriPoint.X + uiViewSlide.Bounds.Width, oriPoint.Y);
                lbButtonClicked.Text = "Favorites";
            }
            else
            {
                uiViewSlide.Center = new CGPoint(oriPoint.X, oriPoint.Y);
                lbButtonClicked.Text = "All";
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return isAll ? AllReviews.Count : FavoriteReviews.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (ReviewTableViewCell)tableView.DequeueReusableCell("reviewCell", indexPath);
            if (isAll)
            {
                cell.Item = AllReviews[indexPath.Row];
                cell.SetupCell();
                AllReviews[indexPath.Row] = cell.Item;
            }else
            {
                cell.Item = FavoriteReviews[indexPath.Row];
                cell.SetupCell();
                FavoriteReviews[indexPath.Row] = cell.Item;
            }
            return cell;
        }
    }
}

