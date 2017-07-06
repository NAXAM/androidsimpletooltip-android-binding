using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.iOS.Social.Cells;
using UIKit;

namespace Naxam.Busuu.iOS.Social.Common
{
    public class FriendsTableViewSource : MvxTableViewSource
    {
       
		public FriendsTableViewSource(UITableView tableView) : base(tableView)
        {
            
		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (FriendsCell)tableView.DequeueReusableCell((NSString)"FriendsCell");
        }

    }
}
