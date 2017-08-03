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
using Android.Support.V4.View;
using Naxam.Busuu.Droid.Core.Adapter;
using Naxam.Busuu.Review.ViewModels;
using Com.Getbase.Floatingactionbutton;

namespace Naxam.Busuu.Droid.Review.Views
{
    [Activity(Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class ReviewView : MvxAppCompatActivity
    {
        ViewPager viewPager;
        List<string> titles;
        List<Android.Support.V4.App.Fragment> listFragment;
        Android.Support.Design.Widget.TabLayout tabLayout;
        FloatingActionsMenu btnFloating;
        View viewBackground;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

        }
        protected override void OnViewModelSet()

        {
            SetContentView(Resource.Layout.review_activity);
            viewPager = FindViewById<ViewPager>(Resource.Id.view_pager);
            tabLayout = FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.tab_layout);
            viewBackground = FindViewById(Resource.Id.layoutBackground);
            titles = new List<string> {
                "All","Favorites"
            };
            btnFloating = FindViewById<FloatingActionsMenu>(Resource.Id.btnFloating);
            btnFloating.MenuCollapsed += (s, e) => {
                viewBackground.Visibility = ViewStates.Gone;
            };
            btnFloating.MenuExpanded += (s, e) => {
                viewBackground.Visibility = ViewStates.Visible;
            };
             

            listFragment = new List<Android.Support.V4.App.Fragment>();
            AllFragment frag = new AllFragment((ViewModel as ReviewViewModel).Reviews);
            listFragment.Add(frag);
            AllFragment frag2 = new AllFragment((ViewModel as ReviewViewModel).Reviews);
            listFragment.Add(frag2);
            ViewPagerFragmentAdapter adapter = new ViewPagerFragmentAdapter(SupportFragmentManager, listFragment, titles);
            viewPager.Adapter = adapter;
            tabLayout.SetupWithViewPager(viewPager);
        }
    }
}