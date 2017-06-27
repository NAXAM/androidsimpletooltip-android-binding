﻿using System;
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
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics;
using Android.Views.Animations;
using Android.Animation;
using Android.Content.Res;
using System.Threading.Tasks;
using Android.Gestures;
using MvvmCross.Binding.Droid.Target;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class LessonHeaderBackground : FrameLayout 
    { 
        public Color BackgroundColor { get; set; }
        Color SecondColor;
        Context context;
        bool expand, busy;
        TextView txtLesson, textView,txtTitle;
        ImageView btnDownload;
        int[][] states = new int[][] {
                            new int[] { Android.Resource.Attribute.StateEnabled}, // enabled
                            new int[] {-Android.Resource.Attribute.StateEnabled}, // disabled
                           // new int[] {-Android.Resource.Attribute.StateChecked}, // unchecked
                           // new int[] { Android.Resource.Attribute.StatePressed}  // pressed
                        };
        protected LessonHeaderBackground(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
        public LessonHeaderBackground(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            this.context = context;
            Initialize(attrs);
        }

        public LessonHeaderBackground(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            this.context = context;
            Initialize(attrs);
        }

        ColorStateList listTxt;

        public void InitAnim(float x,float y)
        {
            if (busy)
                return;
            busy = true;
            textView = new TextView(context);
            txtLesson = FindViewById<TextView>(Resource.Id.txtLesson);
            txtTitle = FindViewById<TextView>(Resource.Id.txtTitle);
            btnDownload = FindViewById<ImageView>(Resource.Id.btnDownload);
            FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(0, 0);
            textView.SetX(x);
            textView.SetY(y);
            textView.LayoutParameters = layoutParams;
            PaintDrawable paint = new PaintDrawable(BackgroundColor);
            paint.Shape = new RectShape();
            paint.SetCornerRadius(1000);
            textView.SetBackgroundDrawable(paint);
            this.AddView(textView, 0);
            ValueAnimator anim = ValueAnimator.OfInt(0, 100);

            anim.AddUpdateListener(new AnimatorUpdateListener(
                (d) =>
                {
                    int val = (int)d.AnimatedValue;
                    FrameLayout.LayoutParams layoutParamsTV = (FrameLayout.LayoutParams)textView.LayoutParameters;
                    layoutParamsTV.Height += val;
                    layoutParamsTV.Width += val;
                    textView.SetX(textView.GetX() - val / 2);
                    textView.SetY(textView.GetY() - val / 2);
                    textView.LayoutParameters = layoutParams;
                    if (expand)
                    {
                        txtLesson.SetTextSize(ComplexUnitType.Dip, Util.Util.DpFromPx(context, txtLesson.TextSize) - 1);
                    }
                    else
                    {
                        txtLesson.SetTextSize(ComplexUnitType.Dip, Util.Util.DpFromPx(context, txtLesson.TextSize) + 1);
                    }

                }));
            anim.AddListener(new AnimatorListener
            {
                AnimationStartHandle = (start) =>
                {
                    busy = true;
                },
                AnimationEndHandle = (end) =>
                {
                    busy = false;
                    RemoveView(textView);
                    SetBackgroundColor(BackgroundColor); 
                    Color temp = BackgroundColor;
                    BackgroundColor = SecondColor;
                    if (!expand)
                    {
                        int[] colorsTxt = new int[] { new Color(255, 255, 255), new Color(255, 255, 255) };
                        listTxt = new ColorStateList(states, colorsTxt);
                        txtLesson.SetTextSize(ComplexUnitType.Dip, 26);
                        int[] colors = new int[] { Color.White, Color.White };
                        ColorStateList myList = new ColorStateList(states, colors);
                        //btnDownload.ImageTintList = myList;
                    }
                    else
                    {
                        int[] colorsTxt = new int[] { new Color(55, 145, 206), new Color(55, 145, 206) };
                        listTxt = new ColorStateList(states, colorsTxt);
                        txtLesson.SetTextSize(ComplexUnitType.Dip, 16);
                        int[] colors = new int[] { new Color(204, 204, 204), new Color(204, 204, 204), };
                        ColorStateList myList = new ColorStateList(states, colors);
                        // btnDownload.ImageTintList = myList;

                    }

                    expand = !expand;
                    
                    txtLesson.SetTextColor(listTxt);
                    txtTitle.SetTextColor(listTxt);
                    SecondColor = temp;
                }
            });
            anim.SetDuration(100);
            anim.Start();
        }

        private void Initialize(IAttributeSet attrs)
        {
          //  TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.LessonHeader);
           // BackgroundColor = a.GetColor(Resource.Styleable.LessonHeader_BackgroundColor, Color.Red);
            SecondColor = Color.White;
           // a.Recycle(); 

        }

       

        protected override void Dispose(bool disposing)
        {
            busy = false;
            base.Dispose(disposing);

        }

 
    }
}