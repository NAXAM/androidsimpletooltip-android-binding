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
using Android.Util;

namespace Naxam.Busuu.Droid.Learning.CustomControls
{
    public class CustomViewPager : ViewPager
    {
        public enum SwipeDirection
        {
            all, left, right, none
        }

        private float initialXValue;
        private SwipeDirection direction;
        //
        public CustomViewPager(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            this.direction = SwipeDirection.all;
        }
        //
        private bool IsSwipeAllowed(MotionEvent e)
        {
            if (this.direction == SwipeDirection.all) return true;

            if (direction == SwipeDirection.none)//disable any swipe
                return false;

            if (e.Action == MotionEventActions.Down)
            {
                initialXValue = e.GetX();
                return true;
            }

            if (e.Action == MotionEventActions.Move)
            {
                try
                {
                    float diffX = e.GetX() - initialXValue;
                    if (diffX > 0 && direction == SwipeDirection.right)
                    {
                        // swipe from left to right detected
                        return false;
                    }
                    else if (diffX < 0 && direction == SwipeDirection.left)
                    {
                        // swipe from right to left detected
                        return false;
                    }
                }
                catch (Exception exception)
                {
                    //exception.PrintStackTrace();
                }
            }

            return true;
        }
        //
        public override bool OnTouchEvent(MotionEvent e)
        {
            if (this.IsSwipeAllowed(e))
            {
                return base.OnTouchEvent(e);
            }

            return false;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (this.IsSwipeAllowed(ev))
            {
                return base.OnInterceptTouchEvent(ev);
            }
            return false;
        }
        public void setAllowedSwipeDirection(SwipeDirection direction)
        {
            this.direction = direction;
        }
    }
}