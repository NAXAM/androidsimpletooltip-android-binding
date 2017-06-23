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
using MvvmCross.Droid.Support.V7.AppCompat;
using Com.Ittianyu.Bottomnavigationviewex;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Theme ="@style/AppTheme.NoActionBar")]
    public class MainView : MvxAppCompatActivity
    {
        BottomNavigationViewEx menu;
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.MainActivity);
            menu = FindViewById<BottomNavigationViewEx>(Resource.Id.menu_bottom);
            menu.EnableShiftingMode(false);

            menu.NavigationItemSelected += Menu_NavigationItemSelected;
        }

        private void Menu_NavigationItemSelected(object sender, Android.Support.Design.Widget.BottomNavigationView.NavigationItemSelectedEventArgs e)
        {

        }
    }
}