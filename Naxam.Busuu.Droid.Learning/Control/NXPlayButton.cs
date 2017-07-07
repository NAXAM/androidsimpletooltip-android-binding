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
using Android.Views.Animations;
using Android.Util;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class NXPlayButton : FrameLayout
    {
        private ImageView imIcon;
        private bool isPlay;
        public bool IsPlay { get { return isPlay; } }
        private int widthControl = 0;
        private int heightControl = 0;
        public event EventHandler<bool> PlayPause;


        public NXPlayButton(Context context) : base(context)
        {

        }

        public NXPlayButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public NXPlayButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public NXPlayButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }


        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);
            widthControl = right - left;
            heightControl = bottom - top;
            Init();
        }

        protected NXPlayButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        private void Init()
        {

            if (ChildCount == 1)
                return;
            imIcon = new ImageView(Context);
            int pxfromdp = (int)Util.Util.PxFromDp(Context, 8);
            FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(-2, -2);
            param.BottomMargin = pxfromdp / 2;
            param.TopMargin = pxfromdp / 2;
            param.LeftMargin = pxfromdp / 2;
            param.RightMargin = pxfromdp / 2;
            param.Gravity = GravityFlags.Center;
            imIcon.LayoutParameters = param;
            
            imIcon.SetPadding(pxfromdp, pxfromdp, pxfromdp, pxfromdp);
            imIcon.SetImageResource(Resource.Drawable.ic_play_arrow);
            imIcon.SetBackgroundResource(Resource.Drawable.corner_button);
            if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            {
                imIcon.Elevation = Util.Util.PxFromDp(Context, 4);
            }
            AddView(imIcon);
            imIcon.Click += (s, e) =>
            {
                PlayPause?.Invoke(imIcon, !isPlay);
                OnClick();
            };
        }


        public void OnClick()
        {


            RotateAnimation rotate = new RotateAnimation(0, isPlay ? 180 : -180, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
            rotate.Duration = 250;
            rotate.FillAfter = true;
            StartAnimation(rotate);

            rotate.SetAnimationListener(new AnimationListener
            {
                AnimationStart = (start) =>
                {

                    if (isPlay)
                    {
                        imIcon.SetImageResource(Resource.Drawable.ic_pause);
                    }
                    else
                    {
                        imIcon.SetImageResource(Resource.Drawable.ic_play_arrow);
                    }
                },

                AnimationEnd = (a) =>
                {
                    imIcon.SetImageResource(isPlay ? Resource.Drawable.ic_play_arrow : Resource.Drawable.ic_pause);
                    ScaleAnimation scale = new ScaleAnimation(1f, 1.1f, 1f, 1.1f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
                    scale.Duration = 250;
                    StartAnimation(scale);
                    isPlay = !isPlay;
                }
            });

        }

    }
}