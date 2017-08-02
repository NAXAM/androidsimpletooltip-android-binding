using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using Naxam.Busuu.Droid.Core.Utils;
using Naxam.Busuu.Droid.Core.Controls;
using Naxam.Busuu.Droid.Review.Adapter;
using Naxam.Busuu.Review.Models;

namespace Naxam.Busuu.Droid.Review.Views
{
    public class AllFragment : MvxFragment
    {
        IList<ReviewModel> Items;
        public AllFragment(IList<ReviewModel> Items)
        {
            this.Items = Items;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HeaderListView listView = new HeaderListView(container.Context);
            ReviewListAdapter adapter = new ReviewListAdapter(container.Context, Items);
            listView.SetAdapter(adapter);
            return listView;
        }
    }
}