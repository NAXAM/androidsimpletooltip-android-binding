using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Graphics;
using Android.Views.Animations;
using Naxam.Busuu.Droid.Profile.Utils;
using Android.Util;
using Android.Support.V4.View;
using Android.Graphics.Drawables;
using Java.Lang;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Naxam.Busuu.Droid.Profile.Views
{

    [Activity(Label = "GeneralProfileView",Theme = "@style/AppTheme.NoActionBar")]
    public class GeneralProfileView : MvxAppCompatActivity, AppBarLayout.IOnOffsetChangedListener
    {
        private LinearLayout ListFriend;
        private GradientDrawable gradientDrawable;
        private TabLayout tabLayout;
        private ViewPager viewPager;

        //
        private static float PERCENTAGE_TO_SHOW_TITLE_AT_TOOLBAR = 0.9f;
        private static float PERCENTAGE_TO_HIDE_TITLE_DETAILS = 0.3f;
        private static int ALPHA_ANIMATIONS_DURATION = 200;

        private bool mIsTheTitleVisible = false;
        private bool mIsTheTitleContainerVisible = true;

        private LinearLayout mTitleContainer;
        private TextView mTitle, mPremium;
        private ImageView mFlag;

        private ImageView mAvatar;
        private AppBarLayout mAppBarLayout;
        private Android.Support.V7.Widget.Toolbar mToolbar;
        private PopupMenu popup;

        //
        public override View OnCreateView(string name, Context context, IAttributeSet attrs)
        {
            return base.OnCreateView(name, context, attrs);
            var avatarParam = (CoordinatorLayout.LayoutParams)mAvatar.LayoutParameters;
            avatarParam.Behavior = new AvatarImageBehavior(this, attrs);
            mAvatar.LayoutParameters = avatarParam;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GeneralProfileActivity);

            bindActivity();
            creatFriends();
            mAppBarLayout.AddOnOffsetChangedListener(this);

            mToolbar.SetBackgroundColor(Color.White);
            startAlphaAnimation(mTitle, 0, ViewStates.Invisible);
            mAvatar.Click += (s, e) =>
            {
                popup.Show();

            };
        }

        private void bindActivity()
        {
            ListFriend = (LinearLayout)FindViewById(Resource.Id.linearFriends);
            //
            gradientDrawable = new GradientDrawable();
            tabLayout = (TabLayout)FindViewById(Resource.Id.tab_layout);
            viewPager = (ViewPager)FindViewById(Resource.Id.view_pager);
            //
            Android.Support.V4.App.FragmentManager manager = SupportFragmentManager;
            MyPagerAdapter adapter = new MyPagerAdapter(manager);
            viewPager.Adapter = adapter;
            tabLayout.SetupWithViewPager(viewPager);
            viewPager.AddOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener(tabLayout));
            tabLayout.SetTabsFromPagerAdapter(adapter);
            //
            mToolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.main_toolbar);
            mPremium = (TextView)FindViewById(Resource.Id.txtPremium);
            mTitle = (TextView)FindViewById(Resource.Id.main_textview_title);
            mTitleContainer = (LinearLayout)FindViewById(Resource.Id.main_linearlayout_title);
            mAppBarLayout = (AppBarLayout)FindViewById(Resource.Id.main_appbar);
            mAvatar = (ImageView)FindViewById(Resource.Id.avatar);
            mFlag = (ImageView)FindViewById(Resource.Id.flag_country);
            popup = new PopupMenu(this, mAvatar);
            gradientDrawable.SetCornerRadius(1000);
            gradientDrawable.SetColor(Color.ParseColor("#FEAB35"));
            mPremium.Background = gradientDrawable;
            //Inflating the Popup using xml file
            popup.MenuInflater
                    .Inflate(Resource.Menu.popup_menu, popup.Menu);
            mFlag.Tag="flag";
            mAvatar.Tag="avatar";

        }

     




        private void creatFriends()
        {
            int scale = (int)ListFriend.Context.Resources.DisplayMetrics.Density;
            LinearLayout.LayoutParams par= new LinearLayout.LayoutParams(scale * 32, scale * 32);
            par.Gravity = GravityFlags.Center;
            ImageView imageView = new ImageView(this);
            imageView.SetImageResource(Resource.Drawable.usa_flag);
            imageView.LayoutParameters = par;
            ListFriend.AddView(imageView);


            for (int i = 0; i < 5; i++)
            {

                LinearLayout.LayoutParams params01 = new LinearLayout.LayoutParams(scale * 32, scale * 32);
                params01.Gravity = GravityFlags.Center;
                params01.LeftMargin = scale * 8;
                ImageView imageView01 = new ImageView(this);
                imageView01.SetImageResource(Resource.Drawable.usa_flag);
                imageView01.LayoutParameters=params01;
                ListFriend.AddView(imageView01);
            }


        }


        private void handleToolbarTitleVisibility(float percentage)
        {
            if (percentage >= PERCENTAGE_TO_SHOW_TITLE_AT_TOOLBAR)
            {

                if (!mIsTheTitleVisible)
                {
                    startAlphaAnimation(mTitle, ALPHA_ANIMATIONS_DURATION, ViewStates.Visible);
                    mToolbar.SetBackgroundColor(Color.ParseColor("#37A8F4"));
                    mIsTheTitleVisible = true;
                }

            }
            else
            {

                if (mIsTheTitleVisible)
                {
                    startAlphaAnimation(mTitle, ALPHA_ANIMATIONS_DURATION, ViewStates.Invisible);
                    mToolbar.SetBackgroundColor(Color.White);
                    mIsTheTitleVisible = false;
                }
            }
        }

        private void handleAlphaOnTitle(float percentage)
        {
            if (percentage >= PERCENTAGE_TO_HIDE_TITLE_DETAILS)
            {
                if (mIsTheTitleContainerVisible)
                {
                    startAlphaAnimation(mTitleContainer, ALPHA_ANIMATIONS_DURATION, ViewStates.Invisible);
                    mIsTheTitleContainerVisible = false;
                }

            }
            else
            {

                if (!mIsTheTitleContainerVisible)
                {
                    startAlphaAnimation(mTitleContainer, ALPHA_ANIMATIONS_DURATION, ViewStates.Visible);
                    mIsTheTitleContainerVisible = true;
                }
            }
        }

        public static void startAlphaAnimation(View v, long duration, ViewStates states)
        {
            AlphaAnimation alphaAnimation = (states == ViewStates.Visible)
                ? new AlphaAnimation(0f, 1.0f)
                : new AlphaAnimation(1.0f, 0f);

            alphaAnimation.Duration = duration;
            alphaAnimation.FillAfter = true;
            v.StartAnimation(alphaAnimation);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //GetMenuInflater().inflate(Resource.menu.menu_main, menu);
            return true;
        }

        public void OnOffsetChanged(AppBarLayout appBarLayout, int offset)
        {
            int maxScroll = appBarLayout.TotalScrollRange;
            float percentage = Java.Lang.Math.Abs(offset/ maxScroll);
            System.Diagnostics.Debug.WriteLine("-->"+ offset);
            handleAlphaOnTitle(percentage);
            handleToolbarTitleVisibility(percentage);
            mAvatar.SetAlpha((int)percentage*100);
        }
    }
}