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
using Android.Support.V4.View;

namespace Naxam.Busuu.Droid.Learning.Transformers
{
    public abstract class BaseTransformer : Java.Lang.Object,ViewPager.IPageTransformer
    {
        protected abstract void onTransform(View view, float position);
        

        public void TransformPage(View view, float position)
        {
            onPreTransform(view, position);
            onTransform(view, position);
            onPostTransform(view, position);
        }

        protected bool hideOffscreenPages()
        {
            return true;
        }

        protected bool isPagingEnabled()
        {
            return false;
        }

       
        protected void onPreTransform(View view, float position)
        {
             float width = view.Width;

            view.RotationX=0;
            view.RotationY=0;
            view.Rotation=0;
            view.ScaleX=1;
            view.ScaleY=1;
            view.PivotX=0;
            view.PivotY=0;
            view.TranslationY=0;
            view.TranslationX=isPagingEnabled() ? 0f : -width * position;

            if (hideOffscreenPages())
            {
                view.Alpha=(position <= -1f || position >= 1f ? 0f : 1f);
            }
            else
            {
                view.Alpha=(1f);
            }
        }

        protected void onPostTransform(View view, float position)
        {
        }
    }
}