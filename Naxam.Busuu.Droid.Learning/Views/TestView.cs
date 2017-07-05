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

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Label = "Premium", Theme = "@style/AppTheme.Premium")]
    public class TestView : MvxAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.test_layout);
            AlternativeWayView alterView = FindViewById<AlternativeWayView>(Resource.Id.alterView);
            alterView.Item = new Busuu.Learning.Model.UnitModel {
                Title = "Ai là đàn bà",
                Answers = new List<AnswerModel>
                {
                    new AnswerModel
                    {
                        Text = "thảo",
                        Value = true
                    },
                     new AnswerModel
                    {
                        Text = "nghĩa"
                    },
                      new AnswerModel
                    {
                        Text = "hà"
                    },
                       new AnswerModel
                    {
                        Text = "tuyến"
                    }
                }
            };
            alterView.Init();
        }
    }
}