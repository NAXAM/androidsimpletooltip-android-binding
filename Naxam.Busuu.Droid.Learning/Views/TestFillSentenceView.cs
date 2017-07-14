﻿using System;
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
    [Activity(Label = "Premium", Theme = "@style/AppTheme.Premium", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class TestFillSentenceView : MvxAppCompatActivity
    {
        MemoFillSentence alterView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.test_fill_sentence_layout);
           // alterView = FindViewById<MemoFillSentence>(Resource.Id.alterView);
            alterView.OrientationScreen = 1;
            alterView.Item = Data;
           // alterView.Init();
        }
        private UnitModel Data
        {
            get
            {
                return new Busuu.Learning.Model.UnitModel
                {
                    Title = "Ai là người chơi mèo nổ giỏi nhất Naxam",

                    Input = new List<string>
                {
                    "Tôi Là %%  người chơi mèo nổ %% nhất Naxam"
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
                        Text = "nghĩa"


                    },
                      new AnswerModel
                    {
                        Text = "hà"
                    },
                       new AnswerModel
                    {
                        Text = "tuyến",
                         Position = 1,
                         Value  = true,
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
            }
        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                alterView.Dispose();
              //  alterView = FindViewById<MemoFillSentence>(Resource.Id.alterView);
                alterView.OrientationScreen = 2;
                alterView.Item = Data;
               // alterView.Init();
            }
            if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                alterView.Dispose();
               // alterView = FindViewById<MemoFillSentence>(Resource.Id.alterView);
                alterView.OrientationScreen = 1;
                alterView.Item = Data;
              //  alterView.Init();
            }
        }
    }
}