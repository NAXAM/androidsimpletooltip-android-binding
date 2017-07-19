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
    public class PreparePronounceView : Android.Support.V4.App.Fragment
    {
        private bool isClickStar;
        ImageView imgPlayBtn, imgStarBtn;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.PreparePronounce, container, false);
            view.Tag = "1";
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            init(view);
        }
       
        private void init(View view)
        {
            Context context = view.Context;

            imgStarBtn = view.FindViewById<ImageView>(Resource.Id.imgStar);
            imgPlayBtn = view.FindViewById<ImageView>(Resource.Id.imgPlayBtn);
            //
            imgStarBtn.SetBackgroundResource(Resource.Drawable.star_white);
            //
            Android.Views.Animations.Animation RotateAnim = AnimationUtils.LoadAnimation(context,
               Resource.Animation.roteanim);
            Android.Views.Animations.Animation RotateAnimBack = AnimationUtils.LoadAnimation(context,
              Resource.Animation.roteanimback);
            Android.Views.Animations.Animation zoomIn = AnimationUtils.LoadAnimation(context,
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