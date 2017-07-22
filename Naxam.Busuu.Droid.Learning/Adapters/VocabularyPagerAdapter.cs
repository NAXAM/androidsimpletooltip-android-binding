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
    public class VocabularyPagerAdapter : FragmentStatePagerAdapter
    {
        IList<Android.Support.V4.App.Fragment> listFragment;
        public VocabularyPagerAdapter(Android.Support.V4.App.FragmentManager fm, IList<Android.Support.V4.App.Fragment> listFragment) : base(fm)
        {
            this.listFragment = listFragment;
        }
        public override int Count => listFragment.Count;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return listFragment[position];
        } 
    }
}