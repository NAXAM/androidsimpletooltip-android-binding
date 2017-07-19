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
using MvvmCross.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Label ="Premium",Theme ="@style/AppTheme.Premium",ParentActivity =typeof(PremiumView))]
    public class BuyPremiumView : MvxAppCompatActivity
    { 

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.buy_premium_page);
            TextView tv_old_value = FindViewById<TextView>(Resource.Id.tv_old_value);
            tv_old_value.PaintFlags = Android.Graphics.PaintFlags.StrikeThruText;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }
    }
}