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

namespace Naxam.Busuu.iOS.Review.Views
{
    [MvxFromStoryboard(StoryboardName = "Review")]
    public partial class ReviewAllView : MvxViewController<ReviewAllViewModel>
    {
        public ReviewAllView(IntPtr handle): base(handle)
        {
        }

        CGPoint oriPoint;
        bool isAll = true;
        MvxStandardTableViewSource source;
        MvxFluentBindingDescriptionSet<ReviewAllView, ReviewAllViewModel> setBinding;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            this.NavigationItem.TitleView = uiViewButton;
            ReviewTableView.RowHeight = 60;

            source = new ReviewTableViewSource(ReviewTableView,(NSString)"reviewCell");
            ReviewTableView.Source = source;

            setBinding = this.CreateBindingSet<ReviewAllView, ReviewAllViewModel>();
            setBinding.Bind(source).To(vm => vm.Reviews);
            setBinding.Bind(source).For(nameof(ReviewTableViewSource.FavoriteCommand)).To(vm=>vm.FavoriteCommand);
            setBinding.Bind(searchBar).To(vm => vm.SearchTerm).TwoWay();
            setBinding.Apply();
            ReviewTableView.ReloadData();

            searchBar.SearchButtonClicked += SearchBar_SearchButtonClicked;
            searchBar.CancelButtonClicked += SearchBar_CancelButtonClicked;
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

        void SearchBar_SearchButtonClicked(object sender, EventArgs e)
        {
            this.ClearAllBindings();
            setBinding.Bind(source).To(m => m.Filterediews);
            setBinding.Apply();
            ReviewTableView.ReloadData();
        }

        void SearchBar_CancelButtonClicked(object sender, EventArgs e)
        {
            this.ClearAllBindings();
            if(isAll)
            {
                setBinding.Bind(source).To(m => m.Reviews);
            }else
            {
                setBinding.Bind(source).To(m => m.FavoriteReviews);
            }
            setBinding.Apply();
            ReviewTableView.ReloadData();
        }
    }

    public class ReviewTableViewSource : MvxStandardTableViewSource, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        IMvxCommand _favoriteCommand;
		public IMvxCommand FavoriteCommand
		{
			get
			{
				return _favoriteCommand;
			}

			set
			{
				SetProperty(ref _favoriteCommand, value);
			}
		}

        public ReviewTableViewSource(UITableView tableview, NSString cellId): base(tableview,cellId)
        {
            
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (ReviewTableViewCell)base.GetCell(tableView, indexPath);

            cell.FavoriteHandler -= HandleEventHandler;
            cell.FavoriteHandler += HandleEventHandler;

			return cell;
        }

        void HandleEventHandler(object sender, ReviewModel e)
        {
			if (FavoriteCommand?.CanExecute(e) != true) return;

			FavoriteCommand.Execute(e);

            var cell = sender as ReviewTableViewCell;
            if (!cell.IsFavorite)
			{
				cell.BtnStar.SetImage(UIImage.FromBundle("rounded_golden_star"), UIControlState.Normal);
			}
			else
			{
				cell.BtnStar.SetImage(UIImage.FromBundle("rounded_grey_star"), UIControlState.Normal);
			}
			cell.IsFavorite = !cell.IsFavorite;
        }

        void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(backingField, value)) return;

			backingField = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
    }
}

