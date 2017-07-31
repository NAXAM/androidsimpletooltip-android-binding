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
using Naxam.Busuu.Learning.Model;
using Com.Google.Android.Flexbox;
using Android.Graphics.Drawables;
using Android.Graphics;
using static Android.Views.GestureDetector;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class OrderWordFragment : BaseFragment
    {
        public override event EventHandler<int> NextClicked;
        TextView txtGuide, txtTitle, txtAnswer;
        FlexboxLayout FillFlex, DisplayFlex;
        LinearLayout LayoutAnswer;
        Rect currentRect, fillRect;
        List<string> input;
        List<string> answer;
        float dX, dY;
        float oX, oY;
        bool busy;
        RelativeLayout root;
        private GestureDetector gestureDetector;
        public OrderWordFragment(UnitModel Item)
        {
            this.Item = Item;
            gestureDetector = new GestureDetector(Context, new SimpleGestureListener());
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.order_word_layout, container, false);
            InitView(view);
            root = (RelativeLayout)view;
            return view;
        }

        private void InitView(View view)
        {
            fillRect = new Rect();
            currentRect = new Rect();
            txtTitle = view.FindViewById<TextView>(Resource.Id.txtTitle);
            txtGuide = view.FindViewById<TextView>(Resource.Id.txtGuide);
            txtAnswer = view.FindViewById<TextView>(Resource.Id.txtAnswer);
            FillFlex = view.FindViewById<FlexboxLayout>(Resource.Id.FillFlexBox);
            DisplayFlex = view.FindViewById<FlexboxLayout>(Resource.Id.DisplayFlexBox);
            //LayoutAnswer = view.FindViewById<LinearLayout>(Resource.Id.FillFlexBox);
            Random random = new Random();

            answer = new List<string>(Item.Input[0].Split(' '));
            input = new List<string>();
            while (input.Count < answer.Count)
            {
                string temp = answer[random.Next(1, 100) % answer.Count];
                if (!input.Contains(temp))
                {
                    input.Add(temp);
                }
            }
            int dpMargin = (int)Util.Util.PxFromDp(Context, 8);
            for (int i = 0; i < input.Count; i++)
            {
                LinearLayout layout = new LinearLayout(Context);
                TextView txtAnswer = new TextView(Context);
                txtAnswer.Background = Util.BackgroundUtil.BackgroundRound(Context, dpMargin / 8, Color.White);
                txtAnswer.Text = input[i];
                txtAnswer.SetPadding(dpMargin, dpMargin, dpMargin, dpMargin);
                if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    txtAnswer.Elevation = dpMargin / 4;
                }
                var param = new FlexboxLayout.LayoutParams(-2, -2);
                var textParam = new LinearLayout.LayoutParams(-2, -2);
                textParam.SetMargins(dpMargin, dpMargin / 2, dpMargin, dpMargin / 2);

                layout.AddView(txtAnswer, textParam);
                DisplayFlex.AddView(layout, param);
                layout.Touch -= Layout_Touch;
                layout.Touch += Layout_Touch;
            }
        }

        private void Layout_Touch(object sender, View.TouchEventArgs e)
        {
            LinearLayout layout = (LinearLayout)sender;
            var param = new FlexboxLayout.LayoutParams(-2, -2);
            txtGuide.GetHitRect(fillRect);
            if (gestureDetector.OnTouchEvent(e.Event))
            {
                if (layout.Parent == DisplayFlex)
                {
                    ((ViewGroup)layout.Parent).RemoveView(layout);
                    FillFlex.AddView(layout, FillFlex.ChildCount, param);
                }
                else
                {
                    ((ViewGroup)layout.Parent).RemoveView(layout);
                    DisplayFlex.AddView(layout, DisplayFlex.ChildCount, param);
                }
            }
            else
            {
                switch (e.Event.Action)
                {
                    case MotionEventActions.Down:
                        oX = layout.GetX();
                        oY = layout.GetY();
                        dX = layout.GetX() - e.Event.RawX;
                        dY = layout.GetY() - e.Event.RawY;
                        break;
                    case MotionEventActions.Up:
                        if (currentRect.Intersect(fillRect))
                        {
                            if (layout.Parent == DisplayFlex)
                            {
                                DisplayFlex.RemoveView(layout);
                                FillFlex.AddView(layout, FillFlex.ChildCount, param);
                            }
                            else
                            {
                                layout.SetX(oX);
                                layout.SetY(oY);
                            }
                        }
                        else
                        {
                            if (layout.Parent == DisplayFlex)
                            {
                                layout.SetX(oX);
                                layout.SetY(oY);
                            }
                            else
                            {
                                FillFlex.RemoveView(layout);
                                DisplayFlex.AddView(layout, param);
                            }
                        }

                        break;
                    case MotionEventActions.Move:
                        layout.GetHitRect(currentRect);
                        if (currentRect.Intersect(fillRect))
                        {
                            txtGuide.SetBackgroundColor(Color.ParseColor("#A8AFB8"));
                        }
                        else
                        {
                            txtGuide.SetBackgroundColor(Color.ParseColor("#D8DDE7"));
                        }
                        layout.SetX(e.Event.RawX + dX);
                        layout.SetY(e.Event.RawY + dY);
                        break;
                }
            }
        }
    }
    class SimpleGestureListener : GestureDetector.SimpleOnGestureListener
    {
        public override bool OnSingleTapUp(MotionEvent e)
        {
            return true;
        }
    }

}