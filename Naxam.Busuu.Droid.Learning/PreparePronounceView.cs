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
using static Android.Resource;
using Android.Views.Animations;

namespace Naxam.Busuu.Droid.Learning
{
    [Activity(Label = "PreparePronounceView")]
    public class PreparePronounceView : Activity
    {
        private bool isClickStar;
        ImageView imgPlayBtn, imgStarBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PreparePronounce);
            init();
        }
        private void init()
        {

            imgStarBtn = FindViewById<ImageView>(Resource.Id.imgStar);
            imgPlayBtn = FindViewById<ImageView>(Resource.Id.imgPlayBtn);
            //
            imgStarBtn.SetBackgroundResource(Resource.Drawable.star_white);
            //
            Android.Views.Animations.Animation RotateAnim = AnimationUtils.LoadAnimation(this,
               Resource.Animation.roteanim);
            Android.Views.Animations.Animation RotateAnimBack = AnimationUtils.LoadAnimation(this,
              Resource.Animation.roteanimback);
            Android.Views.Animations.Animation zoomIn = AnimationUtils.LoadAnimation(this,
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
            imgStarBtn.Click += (s, e) =>
            {
                if (isClickStar == false) {
                    imgStarBtn.SetBackgroundResource(Resource.Drawable.ic_yellow_star);
                }
                else
                {
                    imgStarBtn.SetBackgroundResource(Resource.Drawable.star_white);
                }
                isClickStar = !isClickStar;

            };

        }
    }
}