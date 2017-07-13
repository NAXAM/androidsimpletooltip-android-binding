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

namespace Naxam.Busuu.Droid.Learning.Transformers
{
    public class ForegroundToBackgroundTransformer : BaseTransformer
    {
        protected override void onTransform(View view, float position)
        {
             float height = view.Height;
             float width = view.Width;
             float scale = min(position > 0 ? 1f : Math.Abs(1f + position), 0.5f);

            view.ScaleX=scale;
            view.ScaleY=scale;
            view.PivotX=width * 0.5f;
            view.PivotY=height * 1.0f;
            view.TranslationX=(position > 0 ? width * position : -width * position * 0.25f);
        }
        private static  float min(float val, float min)
        {
            return val < min ? min : val;
        }
    }
}