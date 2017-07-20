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

namespace Naxam.Busuu.Droid.Learning.Adapters
{
    public class MyPagerAdapter : FragmentStatePagerAdapter
    {
        public MyPagerAdapter(Android.Support.V4.App.FragmentManager fm):base(fm)
        {

        }
        public override int Count => 2;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            Android.Support.V4.App.Fragment frag = null;
            switch (position)
            {
                case 0:
                    frag = new PreparePronounceView();
                    break;
                case 1:
                    frag = new HearAndRepeatView();
                    break;

            }
            return frag;
        }
    }
}