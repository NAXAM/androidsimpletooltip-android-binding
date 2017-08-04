// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Naxam.Busuu.iOS.Core;
using Naxam.Busuu.Learning.Model;
using Naxam.Busuu.Learning.ViewModel;
using UIKit;

namespace Naxam.Busuu.iOS.Learning.Views
{
	[MvxFromStoryboard(StoryboardName = "Learning")]
	[MvxTabPresentation(WrapInNavigationController = true, TabIconName = "learn_tab_icon", TabName = "Learn", TabSelectedIconName = "learn_tab_icon_selected")]
    public partial class MainView : MvxViewController<MainViewModel>
	{
		public MainView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var nib = UINib.FromName("LessonHeader", null);
            LessonTableView.RegisterNibForHeaderFooterViewReuse(nib, "LessonHeader");

			var buyPremiumCell = BuyPremiumCell.Create();
			buyPremiumCell.Frame = new CGRect(0, 0, View.Bounds.Size.Width, 50);
			View.AddSubview(buyPremiumCell);

			var source = new LessionTableViewSource(LessonTableView);
			LessonTableView.Source = source;

            var bindingSet = this.CreateBindingSet<MainView, MainViewModel>();
			bindingSet.Bind(buyPremiumCell.BtnGo).To(vm => vm.GoPremiumCommand);
            bindingSet.Bind(source).To(vm => vm.LessonAndSubLessions);
			bindingSet.Apply();

			NavigationController.NavigationBar.Layer.ShadowRadius = 2;
			NavigationController.NavigationBar.Layer.ShadowOffset = new CGSize(0, 2);
			NavigationController.NavigationBar.Layer.ShadowOpacity = 0.25f;

            NavigationItem.Title = "Learn";
        }
	}

    public class LessionTableViewSource : MvxTableViewSource, INotifyPropertyChanged, ILessonTableViewCellDelegate
    {
        public event PropertyChangedEventHandler PropertyChanged;
        List<int> lessonIndexs;
        List<int> openingLessons;
        Dictionary<int, List<NSIndexPath>> AllTopicWithLessonKey;

        IMvxCommand _downloadCommand;
		public IMvxCommand DownloadCommand
		{
			get
			{
				return _downloadCommand;
			}

			set
			{
				SetProperty(ref _downloadCommand, value);
			}
		}
		public LessionTableViewSource(UITableView tableview) : base(tableview)
		{
            lessonIndexs = new List<int>();
            openingLessons = new List<int>();
            AllTopicWithLessonKey = new Dictionary<int, List<NSIndexPath>>();
		}

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            var cell = LessonHeader.Create();
            cell.Title.Text = "Beginner A1";
            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            foreach (var item in lessonIndexs)
            {
                if(indexPath.Row == item)
                {
                    return 80f;
                }
            }
            foreach (var lesson in openingLessons)
            {
                foreach (var topic in AllTopicWithLessonKey[lesson])
                {
                    if(indexPath.Row == topic.Row)
                    {
                        return 140f;
                    }
                }
            }
            return 0;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 80;
        }

		void HandleEventHandler(object sender, LessonModel e)
		{
		}

		void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(backingField, value)) return;

			backingField = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
			NSString cellIdentifier;
            if (item is LessonModel)
			{
				cellIdentifier = (Foundation.NSString)"lessonCell";
			}
			else if (item is TopicModel)
			{
                cellIdentifier = (Foundation.NSString)"subCell";
			}
			else
			{
				throw new ArgumentException("Unknown animal of type " + item.GetType().Name);
			}

			var cell = (UITableViewCell)TableView.DequeueReusableCell(cellIdentifier, indexPath);

            if (cell is LessonTableViewCell lCell) {
				lessonIndexs.Add(indexPath.Row);
				var topics = new List<NSIndexPath>();
				for (int j = 1; j <= (item as LessonModel).Count; j++)
				{
					topics.Add(NSIndexPath.FromRowSection(indexPath.Row + j, 0));
				}
				if (!AllTopicWithLessonKey.ContainsKey(indexPath.Row)) AllTopicWithLessonKey.Add(indexPath.Row, topics);
                lCell.Delegate = this;
                System.Diagnostics.Debug.WriteLine($"Row {indexPath.Row} ({(item as LessonModel).LessonName}) is selected: {openingLessons.Contains(indexPath.Row)}");
                lCell.Update(openingLessons.Contains(indexPath.Row), item as LessonModel);
            }
            return cell;
        }

		public void DidTapOnLessonCell(LessonTableViewCell cell)
		{
            var indexPath = TableView.IndexPathForCell(cell);

            if(cell.IsOpen)
            {
                openingLessons.Add(indexPath.Row);
                TableView.ReloadRows(AllTopicWithLessonKey[indexPath.Row].ToArray(), UITableViewRowAnimation.Automatic);
                TableView.ScrollToRow(indexPath, UITableViewScrollPosition.Top, true);
            }
             else
            {
                openingLessons.Remove(indexPath.Row);
                TableView.ReloadRows(AllTopicWithLessonKey[indexPath.Row].ToArray(), UITableViewRowAnimation.Automatic);
            }
		}
    }
}
