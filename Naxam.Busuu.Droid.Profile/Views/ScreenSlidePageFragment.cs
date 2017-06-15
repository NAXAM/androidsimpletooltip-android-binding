using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace Naxam.Busuu.Droid.Profile.Views
{
    public class ScreenSlidePageFragment : Fragment
    {
        public ScreenSlidePageFragment()
        {

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewGroup rootView = (ViewGroup)inflater.Inflate(Resource.Layout.fragment_screen_slide_page, container, false);
            return rootView;
        }
    }
}