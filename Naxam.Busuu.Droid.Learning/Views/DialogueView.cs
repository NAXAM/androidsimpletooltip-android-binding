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

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Theme = "@style/AppTheme.NoActionBar", ParentActivity = typeof(MainView))]
    public class DialogueView : MvxAppCompatActivity
    {
        ExerciseModel Item;
        int Corrrect;
        int step;
        int CountInput;
        Android.Support.V7.App.ActionBar actionBar;
        Android.Support.V4.App.FragmentTransaction transaction;
        Android.Support.V4.App.FragmentManager manager;
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.dialogue_activity);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            {
                toolbar.Elevation = Util.Util.PxFromDp(this, 4);
            }
            toolbar.Title = "";
            SetSupportActionBar(toolbar);
            actionBar = SupportActionBar;
            MemoriseBodyView memoriseBody = FindViewById<MemoriseBodyView>(Resource.Id.memoriseBody);
            memoriseBody.ItemChanged += (s, e) =>
            {
                Item = e;
            };
            Item = Item ?? (ViewModel as DialogueViewModel).Exercise;
            CountInput = Item.Units.Select(d => d.Answers.Where(an => an.Value).ToList().Count).Sum();
            manager = SupportFragmentManager;
            transaction = manager.BeginTransaction();
            InitFragment();
        }


        private void AddFragment(BaseFragment fragment)
        {
            transaction = manager.BeginTransaction();
            fragment.NextClicked += (s, e) =>
            {
                step++;
                Corrrect += e;
                transaction.Remove(fragment);
                InitFragment();
            };
            transaction.Replace(Resource.Id.layout, fragment, "");
            transaction.Commit();
        }

        private void InitFragment()
        {
            switch (step)
            {
                case 0:
                    AddFragment(new DialogueNormalListSentence(Item.Units));
                    break;
                case 1:
                    AddFragment(new DialogueFillListSentence(Item.Units));
                    break;
                case 2:
                    actionBar.Hide();
                    Summary summary = new Summary(Corrrect, CountInput);
                    transaction = manager.BeginTransaction();
                    transaction.Replace(Resource.Id.layout, summary, CountInput + "");
                    transaction.Commit();
                    break;
            }


        }
    }
}