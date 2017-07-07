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

namespace Naxam.Busuu.iOS.Review.Views
{
    [MvxFromStoryboard(StoryboardName = "Review")]
    public partial class ReviewAllView : MvxViewController<ReviewAllViewModel>, IUISearchResultsUpdating
    {
        public ReviewAllView(IntPtr handle): base(handle)
        {
        }

        CGPoint oriPoint;
        bool isAll = true;
        MvxStandardTableViewSource source;
        MvxFluentBindingDescriptionSet<ReviewAllView, ReviewAllViewModel> setBinding;
        UISearchController searchController;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            this.NavigationItem.TitleView = uiViewButton;
            ReviewTableView.RowHeight = 60;

            source = new MvxStandardTableViewSource(ReviewTableView,(NSString)"reviewCell");
            ReviewTableView.Source = source;

            setBinding = this.CreateBindingSet<ReviewAllView, ReviewAllViewModel>();
            setBinding.Bind(source).To(vm=>vm.Reviews);
            setBinding.Bind(searchBar).To(vm => vm.SearchTerm);
            setBinding.Apply();
            ReviewTableView.ReloadData();

			searchController = new UISearchController((UIViewController)null);
			searchController.SearchResultsUpdater = this;
            searchBar = searchController.SearchBar;
			DefinesPresentationContext = true;
			searchController.SearchBar.SizeToFit();
			searchController.WeakDelegate = this;

        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
			uiViewButton.Layer.CornerRadius = uiViewButton.Bounds.Height / 2;
			uiViewSlide.Layer.CornerRadius = uiViewSlide.Bounds.Height / 2;
			btnAll.SetTitleColor(UIColor.White, UIControlState.Normal);
			oriPoint = uiViewSlide.Center;
        }

        partial void btnAll_TouchUpInside(NSObject sender)
        {
			if (isAll) return;
			isAll = true;

			this.ClearAllBindings();
			
			//var set = this.CreateBindingSet<ReviewAllView, ReviewAllViewModel>();
			setBinding.Bind(source).To(vm => vm.Reviews);
			setBinding.Apply();
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

			this.ClearAllBindings();
			//var set = this.CreateBindingSet<ReviewAllView, ReviewAllViewModel>();
			setBinding.Bind(source).To(vm => vm.FavoriteReviews);
			setBinding.Apply();
            ReviewTableView.ReloadData();

			ReviewTableView.ReloadData();
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
            if (!isAll) { 
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

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            this.ClearAllBindings();
			if (searchController.Active)
            {
                setBinding.Bind(source).To(m=>m.Filterediews);
            }else
            {
                if(isAll)
                {
                    setBinding.Bind(source).To(m => m.Reviews);
                }else
                {
                    setBinding.Bind(source).To(m => m.FavoriteReviews);
                }
            }
			setBinding.Apply();
			ReviewTableView.ReloadData();
        }
    }
}

