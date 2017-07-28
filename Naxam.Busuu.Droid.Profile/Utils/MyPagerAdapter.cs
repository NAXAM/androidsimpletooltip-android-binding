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
using Java.Lang;

namespace Naxam.Busuu.Droid.Profile.Utils
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
                    frag = new FragmentExercise();
                    break;
                case 1:
                    frag = new FragmentCorrection();
                    break;
            }
            return frag;
        }
        public override ICharSequence GetPageTitleFormatted(int position)
        {
            ICharSequence title = new Java.Lang.String("");
            switch (position)
            {
                case 0:
                    title = new Java.Lang.String("MY EXERCISES");
                    break;
                case 1:
                    title = new Java.Lang.String("MY CORRECTIONS");
                    break;
            }

            return title;
        }
    }

    internal class FragmentExercise : Android.Support.V4.App.Fragment
    {

    }

    internal class FragmentCorrection : Android.Support.V4.App.Fragment
    {
    }
}