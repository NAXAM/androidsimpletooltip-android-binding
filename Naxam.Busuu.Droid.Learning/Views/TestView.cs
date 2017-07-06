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
            MemoSelectWord alterView = FindViewById<MemoSelectWord>(Resource.Id.alterView);
           // alterView.OrientationScreen = 1;
            alterView.Item = new Busuu.Learning.Model.UnitModel {
                Title = "Ai là đàn bà",
                Input = new List<string>
                {
                    "Tôi Là Ai       Em Là Ai"
                },
                Images = new List<string> {
                    "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg",
                },
                Answers = new List<AnswerModel>
                {
                    new AnswerModel
                    {
                        Text = "thảo",
                        Value = true
                    },
                     new AnswerModel
                    {
                        Text = "nghĩa",
                        Value = true
                    },
                      new AnswerModel
                    {
                        Text = "hà"
                    },
                       new AnswerModel
                    {
                        Text = "tuyến"
                    }
                       ,
                       new AnswerModel
                    {
                        Text = "bình"
                    },
                       new AnswerModel
                    {
                        Text = "sơn"
                    }
                }
            };
            alterView.Init();
        }
    }
}