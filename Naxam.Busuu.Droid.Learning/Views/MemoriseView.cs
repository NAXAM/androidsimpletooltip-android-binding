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

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Label = "Premium", Theme = "@style/AppTheme.Premium", ParentActivity = typeof(MainView))]
    public class MemoriseView : MvxAppCompatActivity
    {
        int PositionStep;
        ExerciseModel Item;
        TextView txtStep;
        ProgressBar prgStep;

        private void InitFragment()
        {
            prgStep.Progress = PositionStep + 1;
            txtStep.Text = prgStep.Progress + "/" + prgStep.Max;
            var manager = SupportFragmentManager;
            var transaction = manager.BeginTransaction();
            
            var temp = Item.Units[PositionStep];
            if (temp.Type == UnitModel.UnitType.FillSentence)
            {
                MemoFillSentence fillSentence = new MemoFillSentence
                {
                    Item = temp
                };
                fillSentence.NextClicked += (s, e) => {
                    if (e)
                    {
                       
                    }
                    PositionStep++;
                    transaction.Remove(fillSentence);
                    InitFragment();
                };
                transaction.Replace(Resource.Id.layout, fillSentence, PositionStep + "");
                transaction.Commit();
            }
            if (temp.Type == UnitModel.UnitType.SelectWord)
            {
                MemoSelectWord selectWord = new MemoSelectWord
                {
                    Item = temp
                };
                selectWord.NextClicked += (s, e) => {
                    if (e)
                    {

                    }
                    PositionStep++;
                    transaction.Remove(selectWord);
                    InitFragment();
                };
                transaction.Replace(Resource.Id.layout, selectWord, PositionStep + "");
                transaction.Commit();
            }
        }

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.memorise_activity);
            txtStep = FindViewById<TextView>(Resource.Id.txtStep);
            prgStep = FindViewById<ProgressBar>(Resource.Id.prgStep);
            MemoriseBodyView memoriseBody = FindViewById<MemoriseBodyView>(Resource.Id.memoriseBody);
            memoriseBody.ItemChanged += (s, e) =>
            {
                Item = e;
            };
            Item = Item ?? (ViewModel as MemoriseViewModel).Exercise;
            prgStep.Max = Item.Units.Count;
            
            InitFragment();
        }
    }
}