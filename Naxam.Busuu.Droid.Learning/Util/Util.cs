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
using Android.Util;

namespace Naxam.Busuu.Droid.Learning.Util
{
    public class Util
    {
        public static float DpFromPx(Context context,float px)
        {
            return px / context.Resources.DisplayMetrics.Density;
        }

        public static float PxFromDp(Context context, float dp)
        {
            return dp * context.Resources.DisplayMetrics.Density;
        }
    }
}