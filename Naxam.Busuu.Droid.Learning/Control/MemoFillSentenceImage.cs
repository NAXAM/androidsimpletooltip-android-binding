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
using Com.Google.Android.Flexbox;
using Naxam.Busuu.Learning.Model;
using Com.Bumptech.Glide;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Text;
using Android.Text.Style;
using static Android.Widget.TextView;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class MemoFillSentenceImage : LinearLayout
    {
        private event EventHandler<AnswerModel> AnswerClick;
        public int OrientationScreen;
        public UnitModel Item;
        int CountCorrect;
        public MemoFillSentenceImage(Context context) : base(context)
        {
        }

        public MemoFillSentenceImage(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public MemoFillSentenceImage(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public MemoFillSentenceImage(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected MemoFillSentenceImage(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public object FlexBoxLayout { get; private set; }
        private Drawable GetBackground(Color color)
        {
            PaintDrawable background = new PaintDrawable(color);
            background.SetCornerRadius((int)Util.Util.PxFromDp(Context, 4));
            background.Shape = new RectShape();
            return background;
        }
        public void Init()
        {
            CountCorrect = Item.Answers.Where(d => d.Value).ToList().Count;
            View view = LayoutInflater.FromContext(Context).Inflate(OrientationScreen == 0 ? Resource.Layout.landscape_fill_sentence_image_layout : Resource.Layout.portrait_fill_sentence_image_layout, null);
            ImageView imgImage = view.FindViewById<ImageView>(Resource.Id.imgImage);
            TextView txtQuestion = view.FindViewById<TextView>(Resource.Id.txtQuestion);
            TextView txtInput = view.FindViewById<TextView>(Resource.Id.txtInput);
            FlexboxLayout flexAnswer = view.FindViewById<FlexboxLayout>(Resource.Id.flexAnswer);
            Button btnNext = view.FindViewById<Button>(Resource.Id.btnNext);

            Glide.With(Context).Load(Item.Images[0]).Into(imgImage);
            txtQuestion.Text = Item.Title;

            SpannableStringBuilder ssb = new SpannableStringBuilder(Item.Input[0]);
            int start = Item.Input[0].IndexOf("       ");
            ssb.SetSpan(new UnderlineSpan(), start+1, start + 6, SpanTypes.InclusiveInclusive);
            txtInput.SetText(ssb, BufferType.Normal);

            btnNext.Visibility = ViewStates.Gone;
            int margin = (int)Util.Util.PxFromDp(Context, 8);
            for (int i = 0; i < Item.Answers.Count; i++)
            {
                TextView btn = new TextView(Context);
                btn.SetTextColor(Color.Black);
               // Typeface face = Typeface.CreateFromAsset(Context.Assets, "fonts/museo_sans300.otf");
                //btn.SetTypeface(face, TypefaceStyle.Normal);
                btn.SetPadding(margin * 2, margin, margin * 2, margin);
                if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    btn.Elevation = margin / 4;
                }
                btn.SetTextSize(ComplexUnitType.Sp, 20);
                btn.TransformationMethod = null;

                var answer = Item.Answers.ElementAt(i);
                btn.Text = answer.Text;
                flexAnswer.AddView(btn, new ViewGroup.LayoutParams(-2, -2));
                FlexboxLayout.LayoutParams layoutparam = (FlexboxLayout.LayoutParams)btn.LayoutParameters;

                layoutparam.SetMargins(margin, margin, margin, margin);

                btn.Background = GetBackground(Color.White);
                btn.Click += (s, e) =>
                {
                    btn.Background = GetBackground(Color.ParseColor("#CFEAFC"));
                    AnswerClick?.Invoke(this, answer);
                };
                
            }
            AnswerClick += (s, e) =>
            {
                if (CountCorrect == 1)
                {
                    AnswerModel ex = Item.Answers.Where(d => d.Value).FirstOrDefault();

                    for (int i = 0; i < flexAnswer.ChildCount; i++)
                    {
                        View v = flexAnswer.GetChildAt(i);
                        v.Enabled = false;
                    }
                    if (e.Value)
                    {
                        SpannableStringBuilder ssbt = new SpannableStringBuilder(txtInput.Text.Replace("       ", " " + e.Text + " "));
                        ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), start + 1, start + 1 + e.Text.Length, SpanTypes.InclusiveInclusive);
                        ssbt.SetSpan(new ForegroundColorSpan(Color.ParseColor("#74B826")), start + 1, start + 1 + e.Text.Length, SpanTypes.InclusiveInclusive);
                        txtInput.SetText(ssbt, BufferType.Normal);
                    }
                    else
                    {
                        SpannableStringBuilder ssbt = new SpannableStringBuilder(txtInput.Text.Replace("       ", " " + e.Text + " " + ex.Text + " "));
                        ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), start + 1, start + e.Text.Length + 2 + ex.Text.Length, SpanTypes.InclusiveInclusive);
                        ssbt.SetSpan(new ForegroundColorSpan(Color.ParseColor("#E54532")), start + 1, start + e.Text.Length + 1, SpanTypes.InclusiveInclusive);
                        ssbt.SetSpan(new StrikethroughSpan(), start + 1, start + e.Text.Length + 1, SpanTypes.InclusiveInclusive);
                        ssbt.SetSpan(new ForegroundColorSpan(Color.ParseColor("#74B826")), start + 2 + e.Text.Length, start + 2 + e.Text.Length + ex.Text.Length, SpanTypes.InclusiveInclusive);
                        txtInput.SetText(ssbt, BufferType.Normal);
                    }
                    btnNext.Visibility = ViewStates.Visible;
                } 
                
            };
            AddView(view, new LinearLayout.LayoutParams(-1, -1));
        }
    }
}