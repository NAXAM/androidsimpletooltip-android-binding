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
    // Checking a result with strCorrection
    // If true or false create a new corresponding spannable text
    // Assigning to txtDisplay

    [Activity(Label = "FillSentenceAudioView")]
    public class FillSentenceAudioView : Activity
    {
        string strCorrection;
        string strDisplay;
        string strResult;
        TextView txtDisplay;
        TextView txtResult;
        ImageView imgPlayBtn;
        Button btnContinue;

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
            btnContinue= (Button)FindViewById(Resource.Id.btnContinue);
            imgPlayBtn = (ImageView)FindViewById(Resource.Id.imgPlayBtn);
            imgPlayBtn.Clickable = true;
            txtResult = (TextView)FindViewById(Resource.Id.txtResult);
            txtDisplay = (TextView)FindViewById(Resource.Id.txtDisplay);
            btnContinue.Visibility = ViewStates.Invisible;
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
                stringBuilder.SetSpan(new TouchableSpan(this, ref txtResult, ref txtDisplay, strCorrection, strDisplay,ref btnContinue), stringBuilder.Length() - thisTag.Length, stringBuilder.Length(), SpanTypes.ExclusiveExclusive);
            }
            txtResult.SetText(stringBuilder, TextView.BufferType.Normal);
            txtResult.MovementMethod = LinkMovementMethod.Instance;
        }
    }

    class TouchableSpan : ClickableSpan
    {
        Button btnContinue;
        int start;
        string strDisplay;
        TextView txtDisplay;
        TextView txtResult;
        string strCorrection;
        private Context context;
        private bool mIsPressed;

        public TouchableSpan(Context context, ref TextView txtResult, ref TextView txtDisplay, string strCorrection, string strDisplay, ref Button btnContinue)
        {
            this.btnContinue = btnContinue;
            this.strCorrection = strCorrection;
            this.strDisplay = strDisplay;
            this.context = context;
            this.txtResult = txtResult;
            this.txtDisplay = txtDisplay;
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
            int sa = s.GetSpanStart(this);
            int en = s.GetSpanEnd(this);
            string clickedTxt = s.SubSequence(sa, en).ToString().Replace(" ", "");

            if (strCorrection == clickedTxt)
            {
                start = strDisplay.IndexOf("___");
                strDisplay = strDisplay.Replace("___", clickedTxt);
                SpannableStringBuilder ssb = new SpannableStringBuilder(strDisplay);
                ssb.SetSpan(new ForegroundColorSpan(Color.ParseColor("#93BB59")), start, start + clickedTxt.Length, SpanTypes.InclusiveInclusive);
                ssb.SetSpan(new StyleSpan(TypefaceStyle.Bold), start, start + clickedTxt.Length, SpanTypes.InclusiveInclusive);
                txtDisplay.SetText(ssb, TextView.BufferType.Normal);
                btnContinue.Visibility = ViewStates.Visible;

            }
            else
            {
                start = strDisplay.IndexOf("___");
                strDisplay = strDisplay.Replace("___", clickedTxt + " " + strCorrection);
                SpannableStringBuilder ssb = new SpannableStringBuilder(strDisplay);
                ssb.SetSpan(new StrikethroughSpan(), start, start + clickedTxt.Length, SpanTypes.InclusiveInclusive);
                ssb.SetSpan(new ForegroundColorSpan(Color.ParseColor("#EB4230")), start, start + clickedTxt.Length, SpanTypes.InclusiveInclusive);
                ssb.SetSpan(new StyleSpan(TypefaceStyle.Bold), start, start + clickedTxt.Length, SpanTypes.InclusiveInclusive);
                //
                ssb.SetSpan(new ForegroundColorSpan(Color.ParseColor("#93BB59")), start + clickedTxt.Length + 1, start + clickedTxt.Length + 1 + strCorrection.Length, SpanTypes.InclusiveInclusive);
                ssb.SetSpan(new StyleSpan(TypefaceStyle.Bold), start + clickedTxt.Length + 1, start + clickedTxt.Length + 1 + strCorrection.Length, SpanTypes.InclusiveInclusive);
                txtDisplay.SetText(ssb, TextView.BufferType.Normal);
                btnContinue.Visibility = ViewStates.Visible;
            }
            txtResult.Enabled = false;

        }
    }
    //
    //public class RoundedBackgroundSpan : ReplacementSpan
    //{

    //    private static int CORNER_RADIUS = 8;
    //    private int backgroundColor = 0;
    //    private int textColor = 0;

    //    public RoundedBackgroundSpan(Context context) : base()
    //    {

    //        backgroundColor = context.Resources.GetColor(Resource.Color.material_grey_300);
    //        textColor = context.Resources.GetColor(Resource.Color.colorWhite);
    //    }


    //    private float measureText(Paint paint, string text, int start, int end)
    //    {
    //        return paint.MeasureText(text, start, end);
    //    }


    //    public override void Draw(Canvas canvas, ICharSequence text, int start, int end, float x, int top, int y, int bottom, Paint paint)
    //    {
    //        RectF rect = new RectF(x, top, x + measureText(paint, text.ToString(), start, end), bottom);
    //        paint.Color = Color.Red;
    //        canvas.DrawRoundRect(rect, CORNER_RADIUS, CORNER_RADIUS, paint);
    //        paint.Color = Color.Yellow;
    //        canvas.DrawText(text, start, end, x, y, paint);
    //    }

    //    public override int GetSize(Paint paint, ICharSequence text, int start, int end, Paint.FontMetricsInt fm)
    //    {
    //        return Java.Lang.Math.Round(paint.MeasureText(text, start, end));
    //    }
    //}
}



