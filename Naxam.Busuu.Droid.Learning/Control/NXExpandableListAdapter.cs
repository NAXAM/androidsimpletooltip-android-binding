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
using Java.Lang;
using Naxam.Busuu.Learning.Model;
using MvvmCross.Binding.Droid.Views;
using Android.Graphics.Drawables;
using Android.Animation;
using Android.Util;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using MvvmCross.Binding.Droid.BindingContext;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class NXExpandableListAdapter : MvxExpandableListAdapter,View.IOnTouchListener
    {
        Context context; IList<LessonModel> ItemSource;
        public GestureDetector TapDetector;
        public Color BackgroundColor { get; set; }
        Color SecondColor;
        int[][] states = new int[][] {
                            new int[] { Android.Resource.Attribute.StateEnabled}, // enabled
                            new int[] {-Android.Resource.Attribute.StateEnabled}, // disabled
                           // new int[] {-Android.Resource.Attribute.StateChecked}, // unchecked
                           // new int[] { Android.Resource.Attribute.StatePressed}  // pressed
                        };
        public NXExpandableListAdapter(Context context, IList<LessonModel> ItemSource, IMvxAndroidBindingContext bindingContex) : base(context, bindingContex)
        {
            this.context = context;
            this.ItemSource = ItemSource;
            TapDetector = new GestureDetector(new GestureDetector.SimpleOnGestureListener());
            TapDetector.SingleTapConfirmed += TapDetector_SingleTapConfirmed;
        }
    
        private void TapDetector_SingleTapConfirmed(object sender, GestureDetector.SingleTapConfirmedEventArgs e)
        {
            
        }

        public TopicModel ChildAt(int groupPosition, int childPosition)
        {
            return ItemSource.ElementAt(groupPosition).ElementAt(childPosition);
        }


        public LessonModel GroupAt(int groupPosition)
        {
            return ItemSource.ElementAt(groupPosition);
        }
        
        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            LessonHeaderBackground view = (LessonHeaderBackground)base.GetGroupView(groupPosition, isExpanded, convertView, parent);
             
            //view.SetOnTouchListener(this);
            
            return view;
        }

        

        public bool OnTouch(View v, MotionEvent e)
        { 
            //TapDetector.OnTouchEvent(e);
            return true;
        } 
         
        bool busy, expand;
        TextView textView, txtLesson;
        ImageView btnDownload;
        public void InitAnim(FrameLayout view,GestureDetector.SingleTapConfirmedEventArgs e)
        {
            if (busy)
                return;
            busy = true;
            textView = new TextView(context);
            txtLesson = view.FindViewById<TextView>(Resource.Id.txtLesson);
            btnDownload = view.FindViewById<ImageView>(Resource.Id.btnDownload);
            FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(0, 0);
            textView.SetX(e.Event.GetX());
            textView.SetY(e.Event.GetY());
            textView.LayoutParameters = layoutParams;
            PaintDrawable paint = new PaintDrawable(BackgroundColor);
            paint.Shape = new RectShape();
            paint.SetCornerRadius(1000);
            textView.SetBackgroundDrawable(paint);
            view.AddView(textView, 0);
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
                    view.RemoveView(textView);
                    view.SetBackgroundColor(BackgroundColor);
                    txtLesson.SetTextColor(Android.Graphics.Color.Red);
                    Color temp = BackgroundColor;
                    BackgroundColor = SecondColor;
                    if (!expand)
                    {
                        txtLesson.SetTextSize(ComplexUnitType.Dip, 26);
                        int[] colors = new int[] { Color.White, Color.White };
                        Android.Content.Res.ColorStateList myList = new Android.Content.Res.ColorStateList(states, colors);
                        //btnDownload.ImageTintList = myList;
                    }
                    else
                    {
                        txtLesson.SetTextSize(ComplexUnitType.Dip, 16);
                        int[] colors = new int[] { new Color(204, 204, 204), new Color(204, 204, 204), };
                        Android.Content.Res.ColorStateList myList = new Android.Content.Res.ColorStateList(states, colors);
                        // btnDownload.ImageTintList = myList;

                    }

                    expand = !expand;
                    int[] colorsTxt = new int[] { SecondColor, SecondColor };
                    Android.Content.Res.ColorStateList listTxt = new Android.Content.Res.ColorStateList(states, colorsTxt);
                    txtLesson.SetTextColor(listTxt);
                    SecondColor = temp;
                }
            });
            anim.SetDuration(100);
            anim.Start();
        }
    }
}