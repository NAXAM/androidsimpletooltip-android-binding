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
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Naxam.Busuu.Droid.Learning.Adapters;
using Naxam.Busuu.Droid.Learning.Transformers;

namespace Naxam.Busuu.Droid.Learning
{
    [Activity(Label = "ListPracticeView")]
    public class ListPracticeView : AppCompatActivity
    {
        ViewPager pager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListPractice);
            pager = (ViewPager)FindViewById(Resource.Id.myViewPager);
            Android.Support.V4.App.FragmentManager manager = SupportFragmentManager;
            MyPagerAdapter adapter = new MyPagerAdapter(manager);
            pager.Adapter=adapter;
            pager.SetPageTransformer(true, new ForegroundToBackgroundTransformer());

        }
    }
}