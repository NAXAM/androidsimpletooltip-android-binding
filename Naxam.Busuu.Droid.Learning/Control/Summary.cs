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

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class Summary : Android.Support.V4.App.Fragment
    {
        public int Correct;
        public int Total;
        private bool IsCompleted;
        public Summary(int Correct, int Total)
        {
            this.Correct = Correct;
            this.Total = Total;
            if (Correct == Total - 1)
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

            ValueAnimator animator = ValueAnimator.OfInt(0, Correct);
            ValueAnimator animatorBackground = ValueAnimator.OfInt(0, 3);
            AnimatorSet setAnim = new AnimatorSet();
            setAnim.Play(animator).After(animatorBackground);
            
            animatorBackground.SetDuration(500);
            animator.SetDuration(500 * Correct);
            setAnim.Start();
            animatorBackground.AddUpdateListener(new AnimatorUpdateListener((anim) =>
            {
                txtMark.Text = anim.AnimatedValue + "";
                if ((int)anim.AnimatedValue % 2 == 0)
                {

                    layoutMark.LayoutParameters.Width += 20;
                    layoutMark.LayoutParameters.Height += 20;
                }
                else
                {
                    layoutMark.LayoutParameters.Width -= 20;
                    layoutMark.LayoutParameters.Height -= 20;
                }
            }));
            animator.AddUpdateListener(new AnimatorUpdateListener((anim) =>
            {
                txtMark.Text = anim.AnimatedValue + "";
                if ((int)anim.AnimatedValue == Total - 1)
                { 
                }
            }));
        }
    }
}