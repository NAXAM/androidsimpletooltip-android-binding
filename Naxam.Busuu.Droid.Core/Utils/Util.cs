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
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using Android.Media;
using Java.IO;
 
namespace Naxam.Busuu.Droid.Core.Utils
{
    public class Util
    {
 
        public static float DpFromPx(Context context, float px)
        {
            return px / context.Resources.DisplayMetrics.Density;
        }

        public static float PxFromDp(Context context, float dp)
        {
            return dp * context.Resources.DisplayMetrics.Density;
        }
    }

    public class BackgroundUtil
    {
        public static Drawable BackgroundRound(Context context, int radius, Color color)
        {
            PaintDrawable background = new PaintDrawable(color);
            background.Shape = new RectShape();
            int radiusPX = (int)Util.PxFromDp(context, radius);
            background.SetCornerRadius(radiusPX);

            return background;
        }
    }
}