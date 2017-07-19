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
    [Activity(Label = "Premium", Theme = "@style/AppTheme.Premium")]
    public class TestListView : MvxAppCompatActivity
    {
        ConversationNormalListSentence alterView;
        ConversationFillListSentence alterView2;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.test_fill_list_sentence_layout);
            alterView = FindViewById<ConversationNormalListSentence>(Resource.Id.alterView);
            alterView.OrientationScreen = 1;
            alterView.Items = Data;
            alterView.Init();
            alterView2 = FindViewById<ConversationFillListSentence>(Resource.Id.alterView2);
            alterView2.OrientationScreen = 1;
            alterView2.Items = Data;
            alterView2.Init();
        }
        private List<UnitModel> Data
        {
            get
            {
                Random random = new Random();
                List<UnitModel> listTest = new List<UnitModel>();
                for (int i = 0; i < 4; i++)
                {
                    listTest.Add(new Busuu.Learning.Model.UnitModel
                    {
                        Title = i % 2 == 0 ? "Chipu" : "So Ji Sub",

                        Input = new List<string>
                {
                    "Câu Số %% Hàng Số %%  Sai Hay Đúng"
                },
                        Images = new List<string> {
                    i%2==0?"http://eva-img.24hstatic.com/upload/2-2017/images/2017-04-20/1492655255-chi-pu.jpg":"http://newsen.vn/data/news/2015/3/11/17/So-Ji-Sub-bat-ngo-tai-xuat-man-anh-rong-Newsen-vn-0-1426043851.jpg",
                },
                        Audios = new List<AudioModel> {
                    new AudioModel
                    {
                        Link = "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg",
                        Start = i*10,
                        End = (i+1)*10-1
                    }
                },
                        Answers = new List<AnswerModel>
                {
                    new AnswerModel
                    {
                        Text = " Một 1 " + random.Next(1,1000)+" ",
                        Value = true
                    },
                     new AnswerModel
                    {
                        Text = " Hai 2 " + random.Next(1,1000)+" ",
                        Value  = true,
                        Position = 1
                    }

                }
                    });
                }
                return listTest;
            }
        }
        
    }
}