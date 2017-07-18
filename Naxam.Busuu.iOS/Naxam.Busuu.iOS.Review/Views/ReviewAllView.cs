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
    public partial class ReviewAllView : MvxViewController<ReviewAllViewModel>, IUITableViewDataSource, IUISearchBarDelegate
    {
        public ReviewAllView(IntPtr handle) : base(handle)
        {
        }

        CGPoint oriPoint;
        UISearchBar SearchBar;
        UIBarButtonItem SearchBarButtonItem, TitleBarButtonItem;
        UILabel TitleLabel;
        bool isAll = true;
        List<ReviewModel> AllReviews, FavoriteReviews = null, FilteredReviews = null;
        ActionButton actionButton;
        public static bool isPlayingAudio;

        IGrouping<char, ReviewModel>[] grouping;
        string[] indices;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var btnContinueLearning = new ActionButtonItem("CONTINUE LEARNING", UIImage.FromBundle("fab_menu_row_learning"), UIColor.White);
            btnContinueLearning.ActionPerform = HandleAction;

            var btnAll = new ActionButtonItem("TEST ALL", UIImage.FromBundle("fab_menu_row_all"), UIColor.Blue);
            btnAll.ActionPerform = HandleAction;

			var btnStrength = new ActionButtonItem("STRENGTHEN VOCABULARY", UIImage.FromBundle("fab_menu_row_weak"), UIColor.Red);
			btnStrength.ActionPerform = HandleAction;

			var btnTestFavorite = new ActionButtonItem("TEST FAVORITES", UIImage.FromBundle("fab_menu_row_fav"), UIColor.Gray);
			btnTestFavorite.ActionPerform = HandleAction;

            actionButton = new ActionButton(this.View, new[] { btnContinueLearning, btnAll, btnStrength, btnTestFavorite });
            actionButton.Action = delegate {
                actionButton.ToggleMenu();
            };
            actionButton.SetTitle("+", UIControlState.Normal);

            actionButton.BackgroundColor = UIColor.Blue;

            // Perform any additional setup after loading the view, typically from a nib.

            ReviewTableView.RowHeight = 60;

            AllReviews = this.ViewModel.Reviews;
            FavoriteReviews = this.ViewModel.FavoriteReviews;
            UpdateKeyFromList(AllReviews);

            ReviewTableView.WeakDataSource = this;
            SearchBar = new UISearchBar();
            SearchBar.Delegate = this;
            SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
			
            TitleLabel = new UILabel(new CGRect(0, 0, 150, 30));
            TitleLabel.BackgroundColor = UIColor.Clear;
            TitleLabel.Font = UIFont.FromName("HelveticaNeue-Medium",18);
            TitleLabel.TextAlignment = UITextAlignment.Left;
            TitleLabel.TextColor = UIColor.White;
            TitleLabel.Text = "Review";

            SearchBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Search, SearchHandler);
            TitleBarButtonItem = new UIBarButtonItem(TitleLabel);
            NavigationItem.LeftBarButtonItem = TitleBarButtonItem;
            NavigationItem.RightBarButtonItem = SearchBarButtonItem;
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

        void SearchHandler(object sender, EventArgs e)
        {
            ShowSearchBar();
        }

        private void ShowSearchBar()
        {
            SearchBar.Alpha = 0;
            UIView.Animate(0.5f, ()=>
            {
                NavigationItem.TitleView = SearchBar;
                
                NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIImage.FromBundle("back_arrow"), UIBarButtonItemStyle.Plain, (sender, e) => {
                    HideSearchBar();
                }), true);
				NavigationItem.SetRightBarButtonItem(null, true);
                this.SearchBar.Alpha = 1;
            }, ()=>{
                SearchBar.BecomeFirstResponder();
            });
        }

        void HideSearchBar()
        {
            UIView.Animate(0.2f,() => {
                SearchBar.Alpha = 0;
                NavigationItem.SetRightBarButtonItem(SearchBarButtonItem, true);
                NavigationItem.SetLeftBarButtonItem(TitleBarButtonItem, true);
                SearchBar.ResignFirstResponder();
            },() => {
                
            });
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
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
            UIView.Animate(0.2, () =>
            {
                uiViewSlide.Center = new CGPoint(btnAll.Frame.GetMidX(), btnAll.Frame.GetMaxY());
                uiViewSlide.Transform = CGAffineTransform.MakeTranslation(0.5f, 0.5f);
                btnAll.SetTitleColor(UIColor.FromRGB(86, 156, 201), UIControlState.Normal);
                btnFavorite.SetTitleColor(UIColor.LightGray, UIControlState.Normal);
            });
            FavoriteReviews = null;
        }

        partial void btnFavorite_TouchUpInside(NSObject sender)
        {
            if (!isAll) return;
            isAll = false;
            FavoriteReviews = new List<ReviewModel>();
            FilterFavorite();
            UpdateKeyFromList(FavoriteReviews);
            ReviewTableView.ReloadData();

			UIView.Animate(0.2, () =>
			{
                uiViewSlide.Center = new CGPoint(btnFavorite.Frame.GetMidX(), btnFavorite.Frame.GetMaxY());
				uiViewSlide.Transform = CGAffineTransform.MakeTranslation(0.5f, 0.5f);
                btnFavorite.SetTitleColor(UIColor.FromRGB(86,156,201), UIControlState.Normal);
                btnAll.SetTitleColor(UIColor.LightGray, UIControlState.Normal);
			});
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
            cell.Layer.MasksToBounds = true;
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

        [Export("searchBarSearchButtonClicked:")]
        public void SearchButtonClicked(UISearchBar searchBar)
        {
            FilteredReviews = new List<ReviewModel>();
            FilterContentForSearchText(SearchBar.Text);
        }

        [Export("searchBar:textDidChange:")]
        public void TextChanged(UISearchBar searchBar, string searchText)
        {
			FilteredReviews = new List<ReviewModel>();
			FilterContentForSearchText(SearchBar.Text);
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

        private void FilterFavorite()
        {
            if (FavoriteReviews!=null)
            {
				FavoriteReviews.AddRange(
					AllReviews.Where(e => e.IsFavorite));
			}
			ReviewTableView.ReloadData();
        }
    }
}

