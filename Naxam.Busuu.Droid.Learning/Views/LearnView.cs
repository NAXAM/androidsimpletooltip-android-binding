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
    [Activity]
    public class LearnView : MvxAppCompatActivity
    { 
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet(); 
        }

        private void Menu_NavigationItemSelected(object sender, Android.Support.Design.Widget.BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            
        }
    }
}