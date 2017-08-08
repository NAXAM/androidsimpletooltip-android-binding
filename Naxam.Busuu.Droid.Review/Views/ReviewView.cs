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
using Android.Views.InputMethods;
using Android.Graphics;
using Naxam.Busuu.Droid.Core.Utils;

namespace Naxam.Busuu.Droid.Review.Views
{
    [Activity(Label = "Review", Theme = "@style/AppTheme.NoActionBar" )]
    public class ReviewView : MvxAppCompatActivity
    {
        ViewPager viewPager;
        List<string> titles;
        List<Android.Support.V4.App.Fragment> listFragment;
        Android.Support.Design.Widget.TabLayout tabLayout;
        FloatingActionsMenu btnFloating;
        View viewBackground;
        EditText txtSearch;
        ImageView imgSearch, imgClose;
        Android.Support.V7.Widget.Toolbar toolbar;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

        }
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.review_activity);
            viewPager = FindViewById<ViewPager>(Resource.Id.view_pager);
            tabLayout = FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.tab_layout);
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            imgSearch = FindViewById<ImageView>(Resource.Id.imgSearch);
            txtSearch = FindViewById<EditText>(Resource.Id.txtSearch);
            imgClose = FindViewById<ImageView>(Resource.Id.imgClose);
            imgClose.Click += ImgClose_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtSearch.FocusChange += TxtSearch_FocusChange;
            imgSearch.Click += ImgSearch_Click;
            txtSearch.Background = BackgroundUtil.BackgroundRound(this,0,Color.Transparent);
            viewBackground = FindViewById(Resource.Id.layoutBackground);
            titles = new List<string> {
                "All","Favorites"
            };
            btnFloating = FindViewById<FloatingActionsMenu>(Resource.Id.btnFloating);
            btnFloating.MenuCollapsed += (s, e) =>
            {
                viewBackground.Visibility = ViewStates.Gone;
            };
            btnFloating.MenuExpanded += (s, e) =>
            {
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

        private void TxtSearch_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                btnFloating.Visibility = ViewStates.Gone;
            }
        }

        private void ImgClose_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            imgClose.Visibility = ViewStates.Gone;
        }

        private void TxtSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text.ToString()))
            {
                imgClose.Visibility = ViewStates.Visible;
            }
        }

        private void ImgSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Visibility = ViewStates.Visible;
            imgSearch.Visibility = ViewStates.Gone;
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnSupportNavigateUp()
        {
            txtSearch.Visibility = ViewStates.Gone;
            imgSearch.Visibility = ViewStates.Visible;
            imgClose.Visibility = ViewStates.Invisible;
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            View view = this.CurrentFocus;
            if (view != null)
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }
            btnFloating.Visibility = ViewStates.Visible;
            return base.OnSupportNavigateUp();
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {

            }
            return base.OnKeyDown(keyCode, e);
        }
    }
}