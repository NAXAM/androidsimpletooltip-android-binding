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
using Android.Graphics;
using Com.Bumptech.Glide;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class MemoSelectWord : LinearLayout
    {

        public UnitModel Item;
        int CountAnswer;
        List<TextView> listTextViewCorrect;
        public int OrientationScreen;
        Dictionary<TextView, AnswerModel> listChoice;
        public MemoSelectWord(Context context) : base(context)
        {
        }

        public MemoSelectWord(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public MemoSelectWord(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public MemoSelectWord(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected MemoSelectWord(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }



        private event EventHandler<AnswerModel> AnswerClick;
        public View Init()
        {
            RemoveAllViews();
            int imageCount = Item.Images.Count;
            listChoice = new Dictionary<TextView, AnswerModel>();
            listTextViewCorrect = new List<TextView>();
            CountAnswer = Item.Answers.Where(d => d.Value).ToList().Count;
            View view = LayoutInflater.FromContext(Context).Inflate(OrientationScreen == 2 && imageCount > 0 ? Resource.Layout.landscape_select_words_layout:Resource.Layout.portrait_select_words_layout, null);
            TextView txtQuestion = view.FindViewById<TextView>(Resource.Id.txtQuestion);
            ImageView imgImage = view.FindViewById<ImageView>(Resource.Id.imgImage);
            NXPlayButton btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btnPlay);
            FlexboxLayout flexbox = view.FindViewById<FlexboxLayout>(Resource.Id.flexAnswer);
            Button btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Visibility = ViewStates.Gone;

            txtQuestion.Text = Item.Title;

            if (Item.Audios.Count > 0)
            {
                btnPlay.Visibility = ViewStates.Visible;
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
                imgImage.Visibility = ViewStates.Visible;
            }
            else
            {
                imgImage.Visibility = ViewStates.Gone;
            }





            int margin = (int)Util.Util.PxFromDp(Context, 8);
            for (int i = 0; i < Item.Answers.Count; i++)
            {
                TextView btn = new TextView(Context);
                btn.SetTextColor(Color.White);
                Typeface face = Typeface.CreateFromAsset(Context.Assets, "fonts/museo_sans300.otf");
                btn.SetTypeface(face, TypefaceStyle.Normal);
                btn.SetPadding(margin * 2, margin, margin * 2, margin);
                if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    btn.Elevation = margin / 4;
                }
                btn.SetTextSize(ComplexUnitType.Sp, 24);
                btn.TransformationMethod = null;

                var answer = Item.Answers.ElementAt(i);
                btn.Text = answer.Text;
                flexbox.AddView(btn, new ViewGroup.LayoutParams(-2, -2));
                FlexboxLayout.LayoutParams layoutparam = (FlexboxLayout.LayoutParams)btn.LayoutParameters;

                layoutparam.SetMargins(margin, margin, margin, margin);
                btn.Background = Util.BackgroundUtil.BackgroundRound(Context, 4, Color.ParseColor("#38A9F6"));
                btn.Click += (s, e) =>
                {
                    AnswerClick?.Invoke(btn, answer);
                };
                if (answer.Value && !listTextViewCorrect.Contains(btn))
                {
                    listTextViewCorrect.Add(btn);
                }
            }
            AnswerClick += (s, e) =>
            {
                //if (CountAnswer == 1)
                //{
                //    AnswerModel answerCorrect = Item.Answers.Where(d => d.Value).FirstOrDefault();
                //    TextView txtAnswer = (TextView)s;
                //    if (e.Value)
                //    {
                //        txtAnswer.Background = Util.BackgroundUtil.BackgroundRound(Context, 4, Color.ParseColor("#74B826"));
                //    }
                //    else
                //    {
                //        txtAnswer.Background = Util.BackgroundUtil.BackgroundRound(Context, 4, Color.ParseColor("#E54532"));
                //        foreach (var item in listTextViewCorrect)
                //        {
                //            item.Background = Util.BackgroundUtil.BackgroundRound(Context, 4, Color.ParseColor("#74B826"));
                //        }
                //    }

                //    btnNext.Visibility = ViewStates.Visible;
                //}
                //else
                //{
                if (!listChoice.Keys.Contains((TextView)s))
                {
                    listChoice.Add((TextView)s, e);
                    if (listChoice.Count >= CountAnswer)
                    {
                        btnNext.Visibility = ViewStates.Visible;

                        if (listChoice.Values.Where(d => d.Value).ToList().Count == CountAnswer)
                        {
                            foreach (var item in listTextViewCorrect)
                            {
                                item.Background = Util.BackgroundUtil.BackgroundRound(Context, 4, Color.ParseColor("#74B826"));
                            }
                        }
                        else
                        {
                            foreach (var item in listTextViewCorrect)
                            {
                                item.Background = Util.BackgroundUtil.BackgroundRound(Context, 4, Color.ParseColor("#74B826"));
                            }
                            foreach (var item in listChoice.Where(d => !d.Value.Value).ToList())
                            {
                                item.Key.Background = Util.BackgroundUtil.BackgroundRound(Context, 4, Color.ParseColor("#E54532"));
                            }
                        }
                        for (int i = 0; i < flexbox.ChildCount; i++)
                        {
                            View viewChild = flexbox.GetChildAt(i);
                            if (viewChild.Enabled)
                            {
                                viewChild.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        ((TextView)s).Background = Util.BackgroundUtil.BackgroundRound(Context, 4, Color.ParseColor("#CDEBF9"));
                    }
                }
                else
                {
                    listChoice.Remove((TextView)s);
                    ((TextView)s).Background = Util.BackgroundUtil.BackgroundRound(Context, 4, Color.ParseColor("#36A8F8"));
                }
                //}
            };
            AddView(view, new LinearLayout.LayoutParams(-1, -1));

            return view;
        }


    }
}