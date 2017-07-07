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
using System.Text.RegularExpressions;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class MemoFillSentence : LinearLayout
    {
        private event EventHandler<AnswerModel> AnswerClick;
        public int OrientationScreen;
        public UnitModel Item;
        List<string> listString;
        List<int> listIndex;
        List<AnswerModel> listCorrect;
        Dictionary<TextView, AnswerModel> listChoice;
        int CountCorrect;
        public MemoFillSentence(Context context) : base(context)
        {
        }

        public MemoFillSentence(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public MemoFillSentence(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public MemoFillSentence(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected MemoFillSentence(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
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
        string input;
        public void Init()
        {
            RemoveAllViews();
            listString = new List<string>();
            listIndex = new List<int>();
            int imageCount = Item.Images.Count;
            listChoice = new Dictionary<TextView, AnswerModel>();
            listCorrect = Item.Answers.Where(d => d.Value).OrderBy(d => d.Position).ToList();
            CountCorrect = Item.Answers.Where(d => d.Value).ToList().Count;
            View view = LayoutInflater.FromContext(Context).Inflate(OrientationScreen == 2 && imageCount > 0 ? Resource.Layout.landscape_fill_sentence_image_layout : Resource.Layout.portrait_fill_sentence_image_layout, null);
            ImageView imgImage = view.FindViewById<ImageView>(Resource.Id.imgImage);
            TextView txtQuestion = view.FindViewById<TextView>(Resource.Id.txtQuestion);
            TextView txtInput = view.FindViewById<TextView>(Resource.Id.txtInput);
            FlexboxLayout flexAnswer = view.FindViewById<FlexboxLayout>(Resource.Id.flexAnswer);
            Button btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            NXPlayButton btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btnPlay);
            listString = Regex.Split(Item.Input[0], "%%").ToList();
            if (Item.Audios.Count > 0)
            {

            }
            else
            {
                btnPlay.Visibility = ViewStates.Gone;
            }
            int measuredWidth = 0;
            int measuredHeight = 0;
            IWindowManager windowManager = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.HoneycombMr2)
            {
                Point size = new Point();
                windowManager.DefaultDisplay.GetSize(size);
                measuredWidth = size.X;
                measuredHeight = size.Y;
            }
            else
            {
                Display d = windowManager.DefaultDisplay;
                measuredWidth = d.Width;
                measuredHeight = d.Height;
            }
            if (imageCount > 0)
            {
                if (OrientationScreen == 2)
                {
                    imgImage.LayoutParameters.Width = (int)measuredWidth / 2;
                    imgImage.LayoutParameters.Height = (int)measuredWidth * 9 / 32;
                }
                if (OrientationScreen == 1)
                {
                    imgImage.LayoutParameters.Width = (int)measuredWidth;
                    imgImage.LayoutParameters.Height = (int)measuredWidth * 9 / 16;
                }
                Glide.With(Context).Load(Item.Images[0]).Into(imgImage);
            }
            else
            {
                imgImage.Visibility = ViewStates.Gone;
            }

            txtQuestion.Text = Item.Title;
            input = "";
            for (int i = 0; i < listString.Count; i++)
            {
                if (i < listString.Count - 1)
                {
                    input = input + listString[i] + "     ";
                    listIndex.Add(input.Length - 5);
                }
                else
                {
                    input = input + listString[i];
                }
            }
            SpannableStringBuilder ssb = new SpannableStringBuilder(input);
            for (int i = 0; i < listIndex.Count; i++)
            {
                ssb.SetSpan(new UnderlineSpan(), listIndex[i], listIndex[i] + 5, SpanTypes.InclusiveInclusive);
            }
            txtInput.SetText(ssb, BufferType.Normal);

            btnNext.Visibility = ViewStates.Gone;
            int margin = (int)Util.Util.PxFromDp(Context, 8);
            for (int i = 0; i < Item.Answers.Count; i++)
            {
                TextView btn = new TextView(Context);
                btn.SetTextColor(Color.Black);
                btn.SetPadding(margin * 2, margin, margin * 2, margin);
                if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    btn.Elevation = margin / 4;
                }
                btn.SetTextSize(ComplexUnitType.Sp, 14);
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
                    AnswerClick?.Invoke(btn, answer);
                };

            }
            AnswerClick += (s, e) =>
            {
                if (listChoice.Keys.Contains((TextView)s))
                {
                    listChoice.Remove((TextView)s);
                    ((TextView)s).Background = GetBackground(Color.White);

                }
                else
                {
                    listChoice.Add((TextView)s, e);
                }


                if (CountCorrect == listChoice.Count)
                {
                    input = "";
                    listIndex = new List<int>();
                    for (int i = 0; i < listString.Count; i++)
                    {
                        if (i < listString.Count - 1)
                        {
                            if (listCorrect[i].Text == listChoice.Values.ElementAt(i).Text)
                            {
                                input += listString[i] + listCorrect[i].Text;
                                listIndex.Add(input.Length - listCorrect[i].Text.Length);

                            }
                            else
                            {
                                input += listString[i] + listChoice.Values.ElementAt(i).Text + " " + listCorrect[i].Text;
                                listIndex.Add(input.Length - 1 - listCorrect[i].Text.Length - listChoice.Values.ElementAt(i).Text.Length);
                            }
                        }
                        else
                        {
                            input += listString[i];
                        }
                    }
                    SpannableStringBuilder ssbt = new SpannableStringBuilder(input);
                    for (int i = 0; i < listIndex.Count; i++)
                    {
                        if (listCorrect[i].Text == listChoice.Values.ElementAt(i).Text)
                        {
                            ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), listIndex[i], listIndex[i] + listCorrect[i].Text.Length, SpanTypes.InclusiveInclusive);
                            ssbt.SetSpan(new ForegroundColorSpan(Color.ParseColor("#74B826")), listIndex[i], listIndex[i] + listCorrect[i].Text.Length, SpanTypes.InclusiveInclusive);
                        }
                        else
                        {
                            ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), listIndex[i], listIndex[i] + listCorrect[i].Text.Length + listChoice.Values.ElementAt(i).Text.Length + 1, SpanTypes.InclusiveInclusive);
                            ssbt.SetSpan(new ForegroundColorSpan(Color.ParseColor("#E54532")), listIndex[i], listIndex[i] + listChoice.Values.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                            ssbt.SetSpan(new StrikethroughSpan(), listIndex[i], listIndex[i] + listChoice.Values.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                            ssbt.SetSpan(new ForegroundColorSpan(Color.ParseColor("#74B826")), listIndex[i] + listChoice.Values.ElementAt(i).Text.Length + 1, listIndex[i] + listCorrect[i].Text.Length + listChoice.Values.ElementAt(i).Text.Length + 1, SpanTypes.InclusiveInclusive);
                        }
                    }
                    txtInput.SetText(ssbt, BufferType.Normal);
                    for (int i = 0; i < flexAnswer.ChildCount; i++)
                    {
                        View viewChild = flexAnswer.GetChildAt(i);
                        if (viewChild.Enabled)
                        {
                            viewChild.Enabled = false;
                        }
                    }
                    btnNext.Visibility = ViewStates.Visible;
                }
                else
                {
                    input = "";
                    listIndex = new List<int>();
                    for (int i = 0; i < listString.Count; i++)
                    {
                        if (i < listString.Count - 1)
                        {
                            if (i < listChoice.Keys.Count)
                            {
                                input += listString[i] + listChoice.Keys.ElementAt(i).Text;
                                listIndex.Add(input.Length - listChoice.Keys.ElementAt(i).Text.Length);
                            }
                            else
                            {
                                input += listString[i] + "     ";
                                listIndex.Add(input.Length - 5);
                            }
                        }
                        else
                        {
                            input += listString[i];
                        }
                    }
                    SpannableStringBuilder ssbt = new SpannableStringBuilder(input);
                    for (int i = 0; i < listIndex.Count; i++)
                    {
                        if (i < listChoice.Keys.Count)
                        {
                            ssbt.SetSpan(new StyleSpan(TypefaceStyle.Bold), listIndex[i], listIndex[i] + listChoice.Keys.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                            ssbt.SetSpan(new ForegroundColorSpan(Color.ParseColor("#A7B0B7")), listIndex[i], listIndex[i] + listChoice.Keys.ElementAt(i).Text.Length, SpanTypes.InclusiveInclusive);
                        }
                        else
                        {
                            ssbt.SetSpan(new UnderlineSpan(), listIndex[i], listIndex[i] + 5, SpanTypes.InclusiveInclusive);
                        }
                    }
                    txtInput.SetText(ssbt, BufferType.Normal);
                }

            };
            AddView(view, new LinearLayout.LayoutParams(-1, -1));
        }


    }
}