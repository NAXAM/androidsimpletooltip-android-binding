using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Learning.Model;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Label = "Premium", Theme = "@style/AppTheme.Premium" )]
    public class TestView : MvxAppCompatActivity
    {
        MemoSelectWord alterView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.test_layout);
            //alterView = FindViewById<MemoSelectWord>(Resource.Id.alterView);
            alterView.OrientationScreen = 1;
            alterView.Item = Data;
          //  alterView.Init();
        }
        private UnitModel Data
        {
            get
            {
                return new Busuu.Learning.Model.UnitModel
                {
                    Title = "Chọn từ đúng",

                    Input = new List<string>
                {
                    "Tôi là %% người %%  trai nhất naxam"
                },
                    Images = new List<string> {
                    "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg",
                },
                    Audios = new List<AudioModel> {
                    new AudioModel
                    {
                        Link = "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg"
                    }
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
                        Text = "xấu",
                        Value  = true,
                        Position = 1
                    },
                      new AnswerModel
                    {
                        Text = "đẹp"
                    },
                       new AnswerModel
                    {
                        Text = "nghĩa"
                    }
                       ,
                       new AnswerModel
                    {
                        Text = "sơn"
                    },
                       new AnswerModel
                    {
                        Text = "dị"
                    }
                }
                };
            }
        }
        
    }
}