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
using Naxam.Busuu.Learning.Model;
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Learning.ViewModel;
using Naxam.Busuu.Droid.Learning.Control.Memo;
using Naxam.Busuu.Droid.Learning.Control.Vocabulary;
using Naxam.Busuu.Droid.Learning.Adapters;
using Naxam.Busuu.Droid.Learning.Transformers;
using static Android.Support.V4.View.ViewPager;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Theme = "@style/AppTheme.NoActionBar", ParentActivity = typeof(MainView))]
    public class VocabularyView : MvxAppCompatActivity
    {
        int PositionStep;
        ExerciseModel Item;
        TextView txtStep;
        ProgressBar prgStep;
        LinearLayout layoutStep, layout;
        int Corrrect;
        Android.Support.V7.App.ActionBar actionBar;
        Android.Support.V4.App.FragmentTransaction transaction;
        Android.Support.V4.App.FragmentManager manager;
        VocabularyViewPager viewPager;
        VocabularyPagerAdapter adapter;
        ImageView menuTip;
        IList<Android.Support.V4.App.Fragment> listFragment;
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.vocabulary_activity);
            manager = SupportFragmentManager;
            listFragment = new List<Android.Support.V4.App.Fragment>();
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            {
                toolbar.Elevation = Util.Util.PxFromDp(this, 4);
            }
            toolbar.Title = "";
            SetSupportActionBar(toolbar);
            actionBar = SupportActionBar;
            viewPager = FindViewById<VocabularyViewPager>(Resource.Id.viewPager);
            viewPager.SetAllowedSwipeDirection(VocabularyViewPager.SwipeDirection.Right);
            viewPager.SetPageTransformer(true, new ForegroundToBackgroundTransformer());

            menuTip = FindViewById<ImageView>(Resource.Id.menuTip);
            menuTip.Click += (s, e) => {
                TipDialog dialog = new TipDialog(this, Item.Units[PositionStep].Tip);
                dialog.Show();
            };
            txtStep = FindViewById<TextView>(Resource.Id.txtStep);
            prgStep = FindViewById<ProgressBar>(Resource.Id.prgStep);
            layoutStep = FindViewById<LinearLayout>(Resource.Id.layoutStep);
            layout = FindViewById<LinearLayout>(Resource.Id.layout);
            MemoriseBodyView memoriseBody = FindViewById<MemoriseBodyView>(Resource.Id.memoriseBody);
            memoriseBody.ItemChanged += (s, e) =>
            {
                Item = e;
            };
            Item = Item ?? (ViewModel as VocabularyViewModel).Exercise;
            prgStep.Max = Item.Units.Count;
            
            adapter = new VocabularyPagerAdapter(manager, listFragment);
            viewPager.Adapter = adapter;
            viewPager.AddOnPageChangeListener(new OnPageChangeListener((position)=> {
                if (position == 1)
                {
                    viewPager.SetAllowedSwipeDirection(VocabularyViewPager.SwipeDirection.None);
                }
            }));
            InitFragment();
        }
        public class OnPageChangeListener : Java.Lang.Object, IOnPageChangeListener
        {
            Action<int> PageSelected;
            public OnPageChangeListener(Action<int> PageSelected)
            {
                this.PageSelected = PageSelected;
            }
            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            { 
            }

            public void OnPageScrollStateChanged(int state)
            { 
            }

            public void OnPageSelected(int position)
            {
                PageSelected?.Invoke(position);
            }
        }

        private void InitFragment()
        {
            if (PositionStep == prgStep.Max)
            {
                actionBar.Hide();
                layoutStep.Visibility = ViewStates.Gone;
                Summary summary = new Summary(Corrrect, PositionStep);
                listFragment.Clear();
                listFragment.Add(summary);
                adapter.NotifyDataSetChanged();
                viewPager.Adapter = adapter;
                return;
            }

            prgStep.Progress = PositionStep + 1;
            txtStep.Text = prgStep.Progress + "/" + prgStep.Max;

            var temp = Item.Units[PositionStep];

            AddFragment(temp);
            adapter.NotifyDataSetChanged();
            viewPager.Adapter = adapter;
        }

        private void AddFragment(UnitModel item)
        {
            listFragment.Clear();
            viewPager.SetAllowedSwipeDirection(VocabularyViewPager.SwipeDirection.Right);
            listFragment.Add(new PreparePronounceFragment(item));
            if (item.Tip!=null)
            {
                TipFragment tip2 = new TipFragment(item);
                listFragment.Add(tip2);
                tip2.NextClicked += (s, e) =>
                {
                    viewPager.SetCurrentItem(2, false);
                };
            }
            
            FillSentenceFragment fill2 = new FillSentenceFragment(item);
            fill2.NextClicked += (s, e) =>
            {
                PositionStep++;
                if (e)
                {
                    Corrrect++;
                }
                InitFragment( ); 
            };
            listFragment.Add(fill2);
 
        }
    }
}