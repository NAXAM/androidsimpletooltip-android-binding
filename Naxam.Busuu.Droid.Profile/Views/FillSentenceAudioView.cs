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
using Android.Text;
using Android.Graphics;
using Android.Text.Style;
using Java.Lang;
using System.Collections;
using Android.Text.Method;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "FillSentenceAudioView")]
    public class FillSentenceAudioView : Activity
    {
        bool? isCorrect;
        string strCorrection;
        string strDisplay;
        string strResult;
        TextView txtDisplay;
        TextView txtResult;
        ImageView imgPlayBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FillSentenceAudio);
            init();
        }

        private void init()
        {
            // Note:
            // three "_" is a missing word
            //
            imgPlayBtn = (ImageView)FindViewById(Resource.Id.imgPlayBtn);
            imgPlayBtn.Clickable = true;
            txtResult = (TextView)FindViewById(Resource.Id.txtResult);
            txtDisplay = (TextView)FindViewById(Resource.Id.txtDisplay);
            strDisplay = "How's it ___";
            strResult = "going fine";
            strCorrection = "going";
            txtDisplay.Text = strDisplay;
            txtResult.Clickable = true;
            //
            Animation RotateAnim = AnimationUtils.LoadAnimation(this,
               Resource.Animation.roteanim);
            Animation RotateAnimBack = AnimationUtils.LoadAnimation(this,
              Resource.Animation.roteanimback);
            Animation zoomIn = AnimationUtils.LoadAnimation(this,
                   Resource.Animation.zoom_in);
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

            // populating with spannable 
            SpannableStringBuilder stringBuilder = new SpannableStringBuilder();
            string between = "";
            stringBuilder.Append("");
            foreach (string tag in strResult.Split(' '))
            {
                stringBuilder.Append(between);
                if (between.Length == 0) between = "  ";
                string thisTag = "  " + tag + "  ";
                stringBuilder.Append(thisTag);
                stringBuilder.SetSpan(new TouchableSpan(this), stringBuilder.Length() - thisTag.Length, stringBuilder.Length(), SpanTypes.ExclusiveExclusive);
            }
            txtResult.SetText(stringBuilder, TextView.BufferType.Normal);
            txtResult.MovementMethod = LinkMovementMethod.Instance;
        }
    }

    class TouchableSpan : ClickableSpan
    {
        private Context context;
        private bool mIsPressed;

        public TouchableSpan(Context context)
        {
            this.context = context;
        }

        public override void UpdateDrawState(TextPaint ds)
        {
            base.UpdateDrawState(ds);
            ds.Color = Color.Black;
            ds.BgColor = mIsPressed ? Color.ParseColor("#CEEAFD") : Color.White;
            ds.UnderlineText = false;

        }

        public override void OnClick(View view)
        {
            mIsPressed = !mIsPressed;
            TextView tv = (TextView)view;
            SpannableString s = new SpannableString(tv.TextFormatted);
            int start = s.GetSpanStart(this);
            int end = s.GetSpanEnd(this);
            string clickedTxt = s.SubSequence(start, end).ToString();
            Toast.MakeText(context, "clicked: " + clickedTxt, ToastLength.Long).Show();
        }
    }
}

   

  