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
using Android.Graphics.Drawables;
using static Android.Views.Animations.Animation;
using Android.Views.Animations;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Learning
{
    [Activity(Label = "HearAndRepeat")]
    public class HearAndRepeatView : Activity
    {
        ImageView imgMic, hiddenCircle, imgPlayBtn;
        GradientDrawable clikedShape;
        GradientDrawable UnclikedShape;
        Android.Views.Animations.Animation zoominBtn, zoomout;
        TextView txtGuide;
        bool isClick;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HearAndRepeat);
            init();

        }
        private void init()
        {
            //
            imgPlayBtn = FindViewById<ImageView>(Resource.Id.imgPlayBtn);
            Animation RotateAnim = AnimationUtils.LoadAnimation(this,
               Resource.Animation.roteanim);
            Animation RotateAnimBack = AnimationUtils.LoadAnimation(this,
              Resource.Animation.roteanimback);
            Animation zoomIn = AnimationUtils.LoadAnimation(this,
                   Resource.Animation.zoom_in);
            //
            imgPlayBtn.Click += (s, e) =>
            {
                imgPlayBtn.StartAnimation(zoomIn);
            };

            zoomIn.AnimationEnd += (s, e) =>
            {
                imgPlayBtn.StartAnimation(RotateAnim);

            };
            RotateAnim.AnimationStart += (s, e) =>
            {
                imgPlayBtn.SetImageResource(Resource.Drawable.ic_pause_btn);

            };
            RotateAnim.AnimationEnd += (s, e) =>
            {
                imgPlayBtn.StartAnimation(RotateAnimBack);
                System.Threading.Tasks.Task.Delay(500);
            };
            RotateAnimBack.AnimationStart += (s, e) =>
            {
                imgPlayBtn.SetImageResource(Resource.Drawable.ic_play_btn);
            };


            hiddenCircle = (ImageView)FindViewById(Resource.Id.hiddenCircle);
            imgMic = (ImageView)FindViewById(Resource.Id.imgMic);
            txtGuide = (TextView)FindViewById(Resource.Id.txtGuide);
            // setting an animation
            zoominBtn = AnimationUtils.LoadAnimation(this, Resource.Animation.zoom_in_btn);
            zoomout = AnimationUtils.LoadAnimation(this, Resource.Animation.zoom_out_btn);
            //
            zoomINListen();
            clikedShape = new GradientDrawable();
            clikedShape.SetShape(ShapeType.Rectangle);
            clikedShape.SetCornerRadius(1000);
            clikedShape.SetColor(Color.Red);
            //
            UnclikedShape = new GradientDrawable(); 
            UnclikedShape.SetShape(ShapeType.Rectangle);
            UnclikedShape.SetCornerRadius(10000);
            UnclikedShape.SetColor(Color.ParseColor("#38A9F6"));
            imgMic.Clickable = true;
            // setting drawable for views
            hiddenCircle.Background=UnclikedShape;
            
            imgMic.Click += (s, e) =>
            {
                if (isClick == false)
                {
                    zoomINListen();
                    imgPlayBtn.Enabled = false;
                    txtGuide.Visibility = ViewStates.Invisible;
                    imgMic.SetBackgroundDrawable(clikedShape);// checking this later
                    hiddenCircle.StartAnimation(zoominBtn);
                    isClick = !isClick;
                }
                else
                {

                    zoomout.SetAnimationListener(null);
                    imgPlayBtn.Enabled = true;
                    txtGuide.Visibility = ViewStates.Visible;
                    imgMic.SetBackgroundDrawable(UnclikedShape);
                    isClick = !isClick;
                };
              
            };
            
            }

            
        private void zoomINListen()
        {
            zoominBtn.SetAnimationListener(new AnimationZoomIn(hiddenCircle, zoomout));
            zoomout.SetAnimationListener(new AnimationZoomOut(hiddenCircle, zoominBtn));
        }

        private class AnimationZoomIn :Java.Lang.Object, IAnimationListener
        {
            private Android.Views.Animations.Animation zoomout;
            private ImageView hiddenCircle;
            
            public AnimationZoomIn(ImageView hiddenCircle, Android.Views.Animations.Animation zoomout)
            {
                this.hiddenCircle = hiddenCircle;
                this.zoomout = zoomout;
            }

            public void OnAnimationEnd(Animation animation)
            {
                hiddenCircle.StartAnimation(zoomout);
            }

            public void OnAnimationRepeat(Animation animation)
            {
            }

            public void OnAnimationStart(Animation animation)
            {
            }
        }
        //
        private class AnimationZoomOut : Java.Lang.Object, IAnimationListener
        {
            private Android.Views.Animations.Animation zoomin;
            private ImageView hiddenCircle;
            public AnimationZoomOut(ImageView hiddenCircle, Android.Views.Animations.Animation zoomin)
            {
                this.zoomin = zoomin;
                this.hiddenCircle = hiddenCircle;
            }
           

            public void OnAnimationEnd(Animation animation)
            {
                hiddenCircle.StartAnimation(zoomin);
            }

            public void OnAnimationRepeat(Animation animation)
            {
            }

            public void OnAnimationStart(Animation animation)
            {
            }
        }


    }
}