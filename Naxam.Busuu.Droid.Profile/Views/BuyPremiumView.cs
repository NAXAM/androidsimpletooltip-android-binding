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

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity]
    public class BuyPremiumView : MvxActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.buy_premium_page);
            TextView tv_old_value = FindViewById<TextView>(Resource.Id.tv_old_value);
            tv_old_value.PaintFlags = Android.Graphics.PaintFlags.StrikeThruText;
        }
    }
}