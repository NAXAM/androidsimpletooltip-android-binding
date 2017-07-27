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
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Learning.Model;
using Naxam.Busuu.Learning.ViewModel;
using Naxam.Busuu.Droid.Learning.Control.Memo;
using Naxam.Busuu.Droid.Learning.Control.Vocabulary;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Theme = "@style/AppTheme.NoActionBar", ParentActivity = typeof(MainView))]
    public class MemoriseView : MvxAppCompatActivity
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
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.memorise_activity);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            {
                toolbar.Elevation = Util.Util.PxFromDp(this, 4);
            }
            toolbar.Title = "";
            SetSupportActionBar(toolbar);
            actionBar = SupportActionBar;

            txtStep = FindViewById<TextView>(Resource.Id.txtStep);
            prgStep = FindViewById<ProgressBar>(Resource.Id.prgStep);
            layoutStep = FindViewById<LinearLayout>(Resource.Id.layoutStep);
            layout = FindViewById<LinearLayout>(Resource.Id.layout);
            MemoriseBodyView memoriseBody = FindViewById<MemoriseBodyView>(Resource.Id.memoriseBody);
            memoriseBody.ItemChanged += (s, e) =>
            {
                Item = e;
            };
            Item = Item ?? (ViewModel as MemoriseViewModel).Exercise;
            prgStep.Max = Item.Units.Count;
            manager = SupportFragmentManager;

            InitFragment();
        }

        private void AddFragment(BaseFragment fragment)
        {
            transaction = manager.BeginTransaction();
            fragment.NextClicked += (s, e) =>
            {
                Corrrect += e;
                if (prgStep.Max > PositionStep)
                {
                    PositionStep++;
                }
                transaction.Remove(fragment);
                InitFragment();
            };
            transaction.Replace(Resource.Id.layout, fragment, PositionStep + "");
            transaction.Commit();
        }

        private void InitFragment()
        {
            if (PositionStep == prgStep.Max)
            {
                actionBar.Hide();
                layoutStep.Visibility = ViewStates.Gone;

                ((LinearLayout.LayoutParams)layout.LayoutParameters).BottomMargin = 0;
                Summary summary = new Summary(Corrrect, PositionStep);
                summary.NextClicked += (s, e) => {
                    Util.Util.ClearBackStack(manager);
                    OnBackPressed();
                };
                transaction = manager.BeginTransaction();
                transaction.Replace(Resource.Id.layout, summary, PositionStep + "");
                transaction.Commit();
                return;
            }

            prgStep.Progress = PositionStep + 1;
            txtStep.Text = prgStep.Progress + "/" + prgStep.Max;

            var temp = Item.Units[PositionStep];

            switch (temp.Type)
            {
                case UnitModel.UnitType.ChooseWord:
                    AddFragment(new ChooseWordFragment(temp));
                    break;
                case UnitModel.UnitType.FillSentence:
                    AddFragment(new FillSentenceFragment(temp));
                    break;
                case UnitModel.UnitType.SelectWord:
                    AddFragment(new SelectWordFragment(temp));
                    break;
                case UnitModel.UnitType.MatchingSentence:
                    AddFragment(new MatchingSentenceFragment(temp));
                    break;
                case UnitModel.UnitType.SelectWordImage:
                    AddFragment(new SelectWordImageFragment(temp));
                    break;
                case UnitModel.UnitType.CompleteSentence:
                    AddFragment(new CompleteSentenceFragment(temp));
                    break;
                case UnitModel.UnitType.HearAndRepeat:
                    AddFragment(new HearAndRepeatFragment(temp));
                    break;
            }
        }
    }
}