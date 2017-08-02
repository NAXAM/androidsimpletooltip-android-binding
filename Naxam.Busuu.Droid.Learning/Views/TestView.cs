using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V4.App;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Learning.Model;
using Naxam.Busuu.Droid.Learning.Control.Memo;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Label = "Premium", Theme = "@style/AppTheme.Premium", MainLauncher = true)]
    public class TestView : MvxAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.test_layout);

            OrderWordFragment frag = new OrderWordFragment(new UnitModel {
                Input = new List<string>
                {
                    "See you later."
                }
            });
            Android.Support.V4.App.FragmentManager manager = SupportFragmentManager;
            Android.Support.V4.App.FragmentTransaction trans = manager.BeginTransaction();
            trans.Replace(Resource.Id.test_layout_fragment, frag);
            trans.Commit();
        }
    }
}