using System;
using System.Collections.Generic;
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
        MvxObservableCollection<CountryModel> AllCountry;
        IGrouping<char, CountryModel>[] grouping;

		public ChooseCountryTableViewSource(UITableView tableView) : base(tableView)
        {
            AllCountry = AllDataTable.Countries;
            grouping = (from w in AllCountry
                        orderby w.Country[0].ToString().ToUpper() ascending
                        group w by w.Country[0] into g
                        select g).ToArray();
		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (CountryCell)tableView.DequeueReusableCell((NSString)"CountryCell");
        }

		[Export("tableView:titleForHeaderInSection:")]
        public override string TitleForHeader(UITableView tableView, nint section)
		{
			return grouping[section].Key.ToString().ToUpper();
		}

        public override nint RowsInSection(UITableView tableview, nint section)
		{
			return grouping[section].Count();
		}

		[Export("numberOfSectionsInTableView:")]
        public override nint NumberOfSections(UITableView tableView)
		{
			return grouping.Length;
		}
    }
}
