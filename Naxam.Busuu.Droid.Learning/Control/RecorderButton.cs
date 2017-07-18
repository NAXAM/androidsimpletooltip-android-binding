using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Github.Lzyzsd.Circleprogress;
using Android.Animation;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class RecorderButton : RelativeLayout
    {
        private Button btSpeak;
        private DonutProgress prgRecord;
        private Button btDelete;
        private Button btPlay;

        private int minTimeRecord = 0;
        private int maxTimeRecord = 30;
        private long startTimeRecord = 0;

        private bool isRecordComplete = false;
        private bool isPlay = false;
        bool isUpdate = false;

        public RecorderButton(Context context) : base(context)
        {
        }

        public RecorderButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public RecorderButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public RecorderButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected RecorderButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public void Init(Context context)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.recorder_button, null, true);
            AddView(view, new LayoutParams(-1, -1));

            btSpeak = view.FindViewById<Button>(Resource.Id.bt_speak);
            btDelete = view.FindViewById<Button>(Resource.Id.bt_delete);
            prgRecord = view.FindViewById<DonutProgress>(Resource.Id.prg_record);
            btPlay = view.FindViewById<Button>(Resource.Id.bt_play);

            //btn Delete layout param
            RelativeLayout.LayoutParams btnDeleteParam = (LayoutParams)btDelete.LayoutParameters;

            //set value for progress when record
            ValueAnimator progressAnimator = ValueAnimator.OfInt(0, 30);
            progressAnimator.SetDuration(30000);
            //    progressAnimator.AddUpdateListener(new ValueAnimator.IAnimatorUpdateListener() {


            //    public void onAnimationUpdate(ValueAnimator animation)
            //    {
            //        prgRecord.setProgress((int)animation.getAnimatedValue());
            //    }
            //});

            //set animation for btn delete left to right
            ValueAnimator btnDeleteAnimatorL2R = ValueAnimator.OfInt(0, 64);
            btnDeleteAnimatorL2R.SetDuration(500);
            //    btnDeleteAnimatorL2R.AddUpdateListener(new ValueAnimator.IAnimatorUpdateListener() {


            //        public void onAnimationUpdate(ValueAnimator animation)
            //    {
            //        btnDeleteParam.setMarginStart((int)animation.getAnimatedValue());
            //        btDelete.setLayoutParams(btnDeleteParam);
            //    }
            //});

            //btn delete alpha 100 to 0
            ValueAnimator btnDeleteAnimatorAlpha = ValueAnimator.OfInt(100, 0);
            //        btnDeleteAnimatorAlpha.SetDuration(500);
            //        btnDeleteAnimatorAlpha.AddUpdateListener(new ValueAnimator.IAnimatorUpdateListener() {


            //            public void onAnimationUpdate(ValueAnimator animation)
            //    {
            //        btDelete.setAlpha((int)animation.getAnimatedValue());
            //    }
            //});

            //set animation for btn delete right to left
            ValueAnimator btnDeleteAnimatorR2L = ValueAnimator.OfInt(64, -80);
            btnDeleteAnimatorR2L.SetDuration(500);
            //        btnDeleteAnimatorR2L.AddUpdateListener(new ValueAnimator.IAnimatorUpdateListener() {


            //            public void onAnimationUpdate(ValueAnimator animation)
            //{
            //    btnDeleteParam.setMarginStart((int)animation.getAnimatedValue());
            //    btDelete.setLayoutParams(btnDeleteParam);
            //}
            //        });

            //set animation for btn play rotate +
            ValueAnimator btnPLayAnimatorForward = ValueAnimator.OfInt(0, 360);
            btnPLayAnimatorForward.SetDuration(500);
            //        btnPLayAnimatorForward.AddUpdateListener(new ValueAnimator.IAnimatorUpdateListener() {

            //            public void onAnimationUpdate(ValueAnimator animation)
            //{
            //    btPlay.setRotation((int)animation.getAnimatedValue());
            //}
            //        });

            //set animation for btn play rotate -
            ValueAnimator btnPLayAnimatorBackward = ValueAnimator.OfInt(0, 360);
            btnPLayAnimatorBackward.SetDuration(500);
            //        btnPLayAnimatorBackward.AddUpdateListener(new ValueAnimator.AnimatorUpdateListener() {

            //            public void onAnimationUpdate(ValueAnimator animation)
            //{
            //    btPlay.setRotation(-((int)animation.getAnimatedValue()));
            //}
            //        });

        }
    }
}