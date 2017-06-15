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
using Android.Support.V4.View;
using Android.Support.V4.App;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity]
    public class StartPageView : FragmentActivity
    {
        public static int NUM_PAGE = 3;
        private ViewPager viewPager;
        private PagerAdapter pagerAdapter;
        private ImageView imMainBackground;
        private ImageView imSecondBackground;
        private int oldPosition = 0;
        private float oldPositionOffset = 0f;
        private float oldState = 0;

        private bool isSwipeLeft = true;
        private int[] listSourceBackground = new int[]
        {
            Resource.Drawable.background1,
            Resource.Drawable.background2,
            Resource.Drawable.background3
        };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.StartPage);

            imMainBackground = FindViewById<ImageView>(Resource.Id.im_main_background);
            imSecondBackground = FindViewById<ImageView>(Resource.Id.im_second_background);

            imMainBackground.SetBackgroundResource(listSourceBackground[oldPosition]);
            imSecondBackground.SetBackgroundResource(listSourceBackground[oldPosition]);
            imSecondBackground.Alpha = 0;

            viewPager = FindViewById<ViewPager>(Resource.Id.pager);
            pagerAdapter = new ScreenSlidePagerAdapter(SupportFragmentManager);
            viewPager.Adapter = pagerAdapter;

            viewPager.SetOnPageChangeListener(new OnPageChangeListener(
                (position, positionOffset, positionOffsetPixels) =>
                {
                    if ( oldPositionOffset != 0 && positionOffset - oldPositionOffset < 0)
                    {
                        isSwipeLeft = false;
                        updateBackgroud(isSwipeLeft, positionOffset, position);
                    }
                    else if (  oldPositionOffset != 0 && positionOffset - oldPositionOffset > 0)
                    {
                        isSwipeLeft = true;
                        updateBackgroud(isSwipeLeft, positionOffset, position);
                    }
                    oldPositionOffset = positionOffset;
                },
                (state) =>
                {
                    oldState = state;
                    if (state == 0)
                    {
                        oldPositionOffset = 0f;
                        imMainBackground.SetBackgroundResource(listSourceBackground[oldPosition]);
                        imSecondBackground.SetBackgroundResource(listSourceBackground[oldPosition]);
                        imMainBackground.Tag = (listSourceBackground[oldPosition]);
                        imSecondBackground.Tag = (listSourceBackground[oldPosition]);
                        imMainBackground.Alpha = 1;
                        imSecondBackground.Alpha = 0;
                    }
                },
                (position) =>
                {
                    oldPosition = position;
                }));
        }


        public void updateBackgroud(bool isSwipeLeft, float alpha, int currentPosition)
        {
            if (oldState == 1)
            {
                if (isSwipeLeft)
                {
                    if (imMainBackground.Tag == (imSecondBackground.Tag))
                    {
                        if (currentPosition + 1 < listSourceBackground.Length)
                        {
                            imSecondBackground.SetBackgroundResource(listSourceBackground[currentPosition + 1]);
                            imSecondBackground.Tag = (listSourceBackground[currentPosition + 1]);
                        }
                    }
                }
                else
                {
                    if (imMainBackground.Tag == (imSecondBackground.Tag))
                    {
                        if (currentPosition - 1 >= 0)
                        {
                            imSecondBackground.SetBackgroundResource(listSourceBackground[currentPosition - 1]);
                            imSecondBackground.Tag = (listSourceBackground[currentPosition - 1]);
                        }
                    }
                }
            }
            imSecondBackground.Alpha = alpha;
            imMainBackground.Alpha = 1 - alpha;
        }

        public class OnPageChangeListener : Java.Lang.Object, ViewPager.IOnPageChangeListener
        {
            Action<int, float, int> pageScrolled;
            Action<int> pageScrollStateChanged;
            Action<int> pageSelected;
            public OnPageChangeListener(Action<int, float, int> pageScrolled, Action<int> pageScrollStateChanged, Action<int> pageSelected)
            {
                this.pageScrolled = pageScrolled;
                this.pageScrollStateChanged = pageScrollStateChanged;
                this.pageSelected = pageSelected;
            }

            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {
                pageScrolled?.Invoke(position, positionOffset, positionOffsetPixels);
            }

            public void OnPageScrollStateChanged(int state)
            {
                pageScrollStateChanged?.Invoke(state);
            }

            public void OnPageSelected(int position)
            {
                pageSelected?.Invoke(position);
            }
        }
    }

    public class ScreenSlidePagerAdapter : FragmentStatePagerAdapter
    {
        public ScreenSlidePagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
        }
        public override int Count
        {
            get
            {
                return StartPageView.NUM_PAGE;
            }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return new ScreenSlidePageFragment();
        }
    }
}