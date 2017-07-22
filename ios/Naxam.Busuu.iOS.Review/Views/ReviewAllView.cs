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
using System.Linq;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Naxam.Busuu.iOS.Core;
using Naxam.Busuu.iOS.Core.Views;
using Naxam.Busuu.IOS.Core.Floaty;

namespace Naxam.Busuu.iOS.Review.Views
{
    [MvxFromStoryboard(StoryboardName = "Review")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "review_tab_icon", TabName = "Review", TabSelectedIconName = "review_tab_icon_selected")]
    public partial class ReviewAllView : MvxViewController<ReviewViewModel>, IUITableViewDataSource
    {
        public ReviewAllView(IntPtr handle) : base(handle)
        {
        }

        CGPoint oriPoint;
        UITextField SearchTextField;
        UIBarButtonItem SearchBarButtonItem, TitleBarButtonItem;
        UILabel TitleLabel;
        bool isAll = true;
        List<ReviewModel> AllReviews, FavoriteReviews = null, FilteredReviews = null;
        ActionButton actionButton;
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
            actionButton.Action = delegate
            {
                actionButton.ToggleMenu();
            };
            actionButton.SetTitle("+", UIControlState.Normal);

            actionButton.BackgroundColor = UIColor.Blue;

            // Perform any additional setup after loading the view, typically from a nib.

            ReviewTableView.RowHeight = 60;

            AllReviews = this.ViewModel.Reviews;
            UpdateKeyFromList(AllReviews);

            ReviewTableView.WeakDataSource = this;

            var buyPremiumCell = BuyPremiumCell.Create();
            buyPremiumCell.Frame = new CGRect(uiViewSlide.Frame.GetMinX(), uiViewSlide.Frame.GetMaxY(), View.Bounds.Size.Width, 50);
            buyPremiumCell.Layer.MasksToBounds = true;
            View.AddSubview(buyPremiumCell);

            var setBinding = this.CreateBindingSet<ReviewAllView, ReviewViewModel>();
            setBinding.Bind(buyPremiumCell.BtnGo).To(vm => vm.GoPremiumCommand);
            setBinding.Apply();

            SearchTextField = new UITextField(new CGRect(0, 0, 300, 30));
            SearchTextField.BackgroundColor = UIColor.Clear;
            SearchTextField.TextAlignment = UITextAlignment.Left;
            SearchTextField.TextColor = UIColor.White;
            SearchTextField.Placeholder = "Search";
            SearchTextField.BorderStyle = UITextBorderStyle.None;
            SearchTextField.EditingChanged += SearchTextField_EdittingChanged;

            TitleLabel = new UILabel(new CGRect(0, 0, 150, 30));
            TitleLabel.BackgroundColor = UIColor.Clear;
            TitleLabel.Font = UIFont.FromName("HelveticaNeue-Medium", 18);
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
            SearchTextField.Alpha = 0;
            UIView.Animate(0.5f, () =>
            {
                SearchTextField.Text = "";
                NavigationItem.TitleView = SearchTextField;
                NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIImage.FromBundle("back_arrow"), UIBarButtonItemStyle.Plain, (sender, e) =>
                {
                    {
                        HideSearchBar();
                        if (isAll)
                        {
                            UpdateKeyFromList(AllReviews);
                        }
                        else
                        {
                            UpdateKeyFromList(FavoriteReviews);
                        }
                        ReviewTableView.ReloadData();
                    }
                }), true);
                NavigationItem.SetRightBarButtonItem(null, true);
                this.SearchTextField.Alpha = 1;
            }, () =>
            {
                SearchTextField.BecomeFirstResponder();
            });
        }

        void HideSearchBar()
        {
            UIView.Animate(0.2f, () =>
            {
                SearchTextField.Alpha = 0;
                NavigationItem.SetRightBarButtonItem(SearchBarButtonItem, true);
                NavigationItem.SetLeftBarButtonItem(TitleBarButtonItem, true);
            }, () =>
            {
                SearchTextField.ResignFirstResponder();
            });
        }
        void SearchTextField_EdittingChanged(object sender, EventArgs e)
        {
            FilteredReviews = new List<ReviewModel>();
            FilterContentForSearchText(SearchTextField.Text);
            UpdateKeyFromList(FilteredReviews);
            ReviewTableView.ReloadData();
        }


        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            oriPoint = uiViewSlide.Center;
        }

        void HandleAction(ActionButtonItem obj)
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
                uiViewSlide.Center = new CGPoint(btnAll.Frame.GetMidX(), btnAll.Frame.GetMaxY() - uiViewSlide.Bounds.Size.Height / 2);
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
     uiViewSlide.Center = new CGPoint(btnFavorite.Frame.GetMidX(), btnFavorite.Frame.GetMaxY() - uiViewSlide.Bounds.Size.Height / 2);
     uiViewSlide.Transform = CGAffineTransform.MakeTranslation(0.5f, 0.5f);
     btnFavorite.SetTitleColor(UIColor.FromRGB(86, 156, 201), UIControlState.Normal);

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
            }
            else
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
            cell.Item = grouping[indexPath.Section].ElementAt(indexPath.Row);
            cell.SetupCell();
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
        }

        private void FilterFavorite()
        {
            if (FavoriteReviews != null)
            {
                FavoriteReviews.AddRange(
                    AllReviews.Where(e => e.IsFavorite));
            }
            ReviewTableView.ReloadData();
        }
    }
}

