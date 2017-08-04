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
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Droid.Learning.Control.Memo;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class SelectWordFragment : BaseFragment
    {
        public override event EventHandler<int> NextClicked;
        private event EventHandler<AnswerModel> AnswerClick;

        public SelectWordFragment(UnitModel Item)
        {
            this.Item = Item;
        }
         
        public int OrientationScreen;
        Dictionary<TextView, AnswerModel> listChoice;
        List<TextView> listTextViewCorrect;

        bool result;
        int imageCount, CountAnswer;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            imageCount = Item.Images!=null? Item.Images.Count:0;
            listChoice = new Dictionary<TextView, AnswerModel>();
            listTextViewCorrect = new List<TextView>();
            CountAnswer = Item.Answers.Where(d => d.Value).ToList().Count;
            View view = inflater.Inflate(imageCount > 0 ? Resource.Layout.select_words_layout : Resource.Layout.select_words_layout_non_image, container,false);
            Init(view);
            return view;
        }

        public void Init(View view)
        {
            TextView txtQuestion = view.FindViewById<TextView>(Resource.Id.txtQuestion);
            ImageView imgImage = view.FindViewById<ImageView>(Resource.Id.imgImage);
            NXPlayButton btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btnPlay);
            FlexboxLayout flexbox = view.FindViewById<FlexboxLayout>(Resource.Id.flexAnswer);
            Button btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Visibility = ViewStates.Gone;
            btnNext.Click += (s, e) =>
            {
                NextClicked?.Invoke(btnNext, result?1:0);
            };
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
                var layoutImage = imgImage.LayoutParameters;
                if (Context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
                {
                    layoutImage.Width = (int)measuredWidth / 2;
                    layoutImage.Height = (int)measuredWidth * 3 / 8;
                }
                if (Context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait)
                {
                    layoutImage.Width = (int)measuredWidth;
                    layoutImage.Height = (int)measuredWidth * 3 / 4;
                }
                if (Context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Square)
                {
                    layoutImage.Width = (int)measuredWidth;
                    layoutImage.Height = (int)measuredWidth * 3 / 4;
                }
                imgImage.LayoutParameters = layoutImage;
                Glide.With(Context).Load(Item.Images[0]).CenterCrop().Into(imgImage);
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
                if (!listChoice.Keys.Contains((TextView)s))
                {
                    listChoice.Add((TextView)s, e);
                    if (listChoice.Count >= CountAnswer)
                    {
                        btnNext.Visibility = ViewStates.Visible;
                        result = true;
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
                            result = false;
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
            };
        }
    }
}