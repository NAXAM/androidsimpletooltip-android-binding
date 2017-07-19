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
using Android.Animation;
using static Android.Resource;
using Java.Lang;
using Android.Views.Animations;
using Naxam.Busuu.Droid.Learning.Control.Memo;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class Summary : MemoriseFragmentBase
    {
        public override event EventHandler<bool> NextClicked;
        public event EventHandler<bool> TryAgainClicked;
        public int Correct;
        public int Total;
        private bool IsCompleted;
        public Summary(int Correct, int Total)
        {
            this.Correct = Correct;
            this.Total = Total;
            if (Correct >= Total - 1)
            {
                IsCompleted = true;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.summary_layout, container, false);
            InitComponent(view);
            return view;
        }
        bool busy;
        TextView txtStatus, txtMark, txtTotal, txtTip, txtResult;
        Button btnNext, btnTryAgain;
        RelativeLayout layoutMark;

        private void InitComponent(View view)
        {
            txtStatus = view.FindViewById<TextView>(Resource.Id.txtStatus);
            txtMark = view.FindViewById<TextView>(Resource.Id.txtMark);
            txtTotal = view.FindViewById<TextView>(Resource.Id.txtTotal);
            txtTip = view.FindViewById<TextView>(Resource.Id.txtStatus);
            txtResult = view.FindViewById<TextView>(Resource.Id.txtResult);
            btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            btnTryAgain = view.FindViewById<Button>(Resource.Id.btnTryAgain);
            layoutMark = view.FindViewById<RelativeLayout>(Resource.Id.layoutMark);

            txtStatus.Text = IsCompleted ? "Rất Tốt" : "Ôi Không";
            txtTotal.Text = "trên " + Total;
            txtResult.Text = IsCompleted ? "Hãy tiếp tục!" : "bạn cần ít nhất " + (Total - 1) + " Điểm để vượt qua";
            btnTryAgain.Visibility = IsCompleted ? ViewStates.Gone : ViewStates.Visible;
            btnTryAgain.Click += (s, e) =>
            {
                TryAgainClicked?.Invoke(btnTryAgain, true);
            };
            btnNext.Click += (s, e) =>
            {
                NextClicked?.Invoke(btnNext, IsCompleted);
            };
            float distance = Util.Util.PxFromDp(Context, 2);
            ValueAnimator animator = ValueAnimator.OfInt(0, Correct);
            AnimatorSet mAnimatorSet = new AnimatorSet();
            var animx = ObjectAnimator.OfFloat(layoutMark, "TranslationX", distance, -distance, 0);
            animx.RepeatCount = 4;
            animx.RepeatMode = ValueAnimatorRepeatMode.Reverse;
            mAnimatorSet.Play(animx);


            mAnimatorSet.SetDuration(50);
           
             
            animator.SetDuration(500 * (Correct + 1));


            animator.AddUpdateListener(new AnimatorUpdateListener((anim) =>
            {
                if (txtMark.Text != anim.AnimatedValue + "")
                {
                    txtMark.Text = anim.AnimatedValue + "";
                    busy = false;
                }

                if (!busy&& (int)anim.AnimatedValue>0)
                {
                    busy = true;
                    mAnimatorSet.Start();

                }
            }));

            animator.AddListener(new AnimatorListener
            {
                AnimationEndHandle = (anim) =>
                {
                    layoutMark.ClearAnimation(); 
                }
            });

            animator.Start();
        }
    }
}