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

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class OrderWordFragment : BaseFragment
    {
        public override event EventHandler<int> NextClicked;
        TextView txtGuide, txtTitle, txtAnswer;
        FlexboxLayout FillFlex, DisplayFlex;
        LinearLayout AnswerLayout;
        List<string> input;
        List<string> answer;
        public OrderWordFragment(UnitModel Item)
        {
            this.Item = Item;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.order_word_layout, container, false);
            InitView(view);
            return view;
        }

        private void InitView(View view)
        {
            txtTitle = view.FindViewById<TextView>(Resource.Id.txtTitle);
            txtGuide = view.FindViewById<TextView>(Resource.Id.txtGuide);
            txtAnswer = view.FindViewById<TextView>(Resource.Id.txtAnswer);
            FillFlex = view.FindViewById<FlexboxLayout>(Resource.Id.FillFlexBox);
            DisplayFlex = view.FindViewById<FlexboxLayout>(Resource.Id.DisplayFlexBox);
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
                textParam.SetMargins(dpMargin, dpMargin/2, dpMargin, dpMargin / 2);
         
                layout.AddView(txtAnswer, textParam);
                DisplayFlex.AddView(layout, param);
            }
        }


    }


}