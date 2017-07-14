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
using System.Linq;

namespace Naxam.Busuu.iOS.Review.Views
{
    [MvxFromStoryboard(StoryboardName = "Review")]
    public partial class ReviewAllView : MvxViewController<ReviewAllViewModel>, IUITableViewDataSource, IUISearchResultsUpdating
    {
        public ReviewAllView(IntPtr handle) : base(handle)
        {
        }

        UISearchController searchController;
        CGPoint oriPoint;
        bool isAll = true;
        List<ReviewModel> AllReviews, FavoriteReviews, FilteredReviews;
        ActionButton actionButton;
        public static bool isPlayingAudio;

        IGrouping<char, ReviewModel>[] grouping;
        string[] indices;

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
            UpdateKeyFromList(AllReviews);

            ReviewTableView.WeakDataSource = this;
            searchController = new UISearchController((UIViewController)null);
            searchController.SearchResultsUpdater = this;
            searchController.DimsBackgroundDuringPresentation = false;
            searchController.SearchBar.WeakDelegate = this;

			ReviewTableView.TableHeaderView = searchController.SearchBar;
			DefinesPresentationContext = true;
			searchController.SearchBar.SizeToFit();
        }

        void UpdateKeyFromList(List<ReviewModel> list)
        {
            
			grouping = (from w in list
						orderby w.Title[0].ToString().ToUpper() ascending
						group w by w.Title[0] into g
						select g).ToArray();

			indices = (from s in list
					   orderby s.Title[0].ToString().ToUpper() ascending
					   group s by s.Title[0] into g
					   select g.Key.ToString().ToUpper()).ToArray();
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
            UpdateKeyFromList(AllReviews);
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
            UpdateKeyFromList(FavoriteReviews);
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
           
            if (FilteredReviews == null)
            {
                return grouping[section].Count();
            }else
            {
                return FilteredReviews.Count;
            }
        }

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            return grouping.Length;
        }

        [Export("tableView:titleForHeaderInSection:")]
        public string TitleForHeader(UITableView tableView, nint section)
        {
            return grouping[section].Key.ToString().ToUpper();
        }

        [Export("sectionIndexTitlesForTableView:")]
        public string[] SectionIndexTitles(UITableView tableView)
        {
            return indices;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (ReviewTableViewCell)tableView.DequeueReusableCell("reviewCell", indexPath);
            if (FilteredReviews == null)
            {
                if (isAll)
                {
                    cell.Item = grouping[indexPath.Section].ElementAt(indexPath.Row);
                    cell.SetupCell();
                }
                else
                {
                    cell.Item = FavoriteReviews[indexPath.Row];
                    cell.SetupCell();
                }
            }else
            {
				cell.Item = FilteredReviews[indexPath.Row];
				cell.SetupCell();   
            }
            isPlayingAudio = cell.isPlaying;
            return cell;
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            if (searchController.Active)
            {
                FilteredReviews = new List<ReviewModel>();
            }
            else
            {
                FilteredReviews = null;
            }
			FilterContentForSearchText(searchController.SearchBar.Text);
		}

        private void FilterContentForSearchText(string text)
        {
			if (FilteredReviews != null)
			{
				FilteredReviews.Clear();
				FilteredReviews.AddRange(
					AllReviews.Where(e =>
									string.IsNullOrWhiteSpace(text)
									|| e.Title.ToUpper().Contains(text.ToUpper())));
			}
			ReviewTableView.ReloadData();
        }
    }
}

