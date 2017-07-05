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

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class AlternativeWayView : LinearLayout
    {

        public UnitModel Item;

        public AlternativeWayView(Context context) : base(context)
        {
        }

        public AlternativeWayView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public AlternativeWayView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public AlternativeWayView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected AlternativeWayView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }



        private event EventHandler<bool> AnswerClick;
        public View Init()
        {
            RemoveAllViews();
            View view = LayoutInflater.FromContext(Context).Inflate(Resource.Layout.llternative_way_layout, null);
            TextView txtTitle = view.FindViewById<TextView>(Resource.Id.txtTitle);
            FlexboxLayout flexbox = view.FindViewById<FlexboxLayout>(Resource.Id.flexAnswer);
            Button btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Visibility = ViewStates.Gone;
            int margin = (int)Util.Util.PxFromDp(Context, 8);
            for (int i = 0; i < Item.Answers.Count; i++)
            {
                TextView btn = new TextView(Context);
                btn.SetTextColor(Color.White);
                Typeface face = Typeface.CreateFromAsset(Context.Assets, "fonts/museo_sans300.otf");
                btn.SetTypeface(face, TypefaceStyle.Normal);
                btn.SetPadding(margin*2, margin, margin * 2, margin);
                if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    btn.Elevation = margin/4;
                }
                btn.SetTextSize(ComplexUnitType.Sp, 24);
                btn.TransformationMethod = null;

                var answer = Item.Answers.ElementAt(i);
                btn.Text = answer.Text;
                flexbox.AddView(btn, new ViewGroup.LayoutParams(-2, -2));
                FlexboxLayout.LayoutParams layoutparam = (FlexboxLayout.LayoutParams)btn.LayoutParameters;
                
                layoutparam.SetMargins(margin, margin, margin, margin);
                btn.SetBackgroundResource(Resource.Drawable.background_round_4dp_blue);
                btn.Click += (s, e) =>
                {
                    if (answer.Value)
                    {
                        AnswerClick?.Invoke(answer, true);
                        btn.SetBackgroundResource(Resource.Drawable.background_round_4dp_green);
                    }
                    else
                    {
                        btn.SetBackgroundResource(Resource.Drawable.background_round_4dp_red);
                        AnswerClick?.Invoke(answer, false);
                    }

                };
            }
            AnswerClick += (s, e) =>
            {
                AnswerModel ex = Item.Answers.Where(d => d.Value).FirstOrDefault();
                AnswerModel answer = (AnswerModel)s;
                for (int i = 0; i < flexbox.ChildCount; i++)
                {
                    View v = flexbox.GetChildAt(i);
                    if (v.GetType() == typeof(TextView))
                    {
                        TextView btn = (TextView)v;
                        if (!e && ex.Text == btn.Text)
                        {
                            btn.SetBackgroundResource(Resource.Drawable.background_round_4dp_green);
                        }
                        btn.Enabled = false; 
                    }
                }
                btnNext.Visibility = ViewStates.Visible;
            }; 
            AddView(view,new LinearLayout.LayoutParams(-1,-1)); 
            
            return view;
        }


    }
}