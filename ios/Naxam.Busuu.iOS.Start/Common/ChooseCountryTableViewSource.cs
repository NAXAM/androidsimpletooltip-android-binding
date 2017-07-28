using System;
using System.Linq;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.iOS.Start.Cells;
using Naxam.Busuu.Start.Model;
using Naxam.Busuu.Start.ViewModel;
using UIKit;

namespace Naxam.Busuu.iOS.Start.Common
{
    public class ChooseCountryTableViewSource : MvxTableViewSource
    {
        ChooseCountryViewModel AllDataTable = new ChooseCountryViewModel();

		public ChooseCountryTableViewSource(UITableView tableView) : base(tableView)
        {
		    
		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (CountryCell)tableView.DequeueReusableCell((NSString)"CountryCell");
        }

		[Export("tableView:titleForHeaderInSection:")]
        public override string TitleForHeader(UITableView tableView, nint section)
		{
            return AllDataTable.Countries[(int)section].Country.Substring(0, 1);
		}
    }
}
