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
using Android.Support.V4.View;
using Java.Lang;
using Naxam.Busuu.Learning.Model;
using System.Text.RegularExpressions;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using Com.Google.Android.Flexbox;
using Android.Util;

namespace Naxam.Busuu.Droid.Learning.Control.Conversation
{
    public class ConversationPagerAdapter : PagerAdapter
    {
        public event EventHandler<int> NextClickHandler;
        public Context context;
        public IList<UnitModel> Items;
        int CountAnswer;
        public int OrientationScreen;
        List<AnswerModel> listTextIndex;
        List<AnswerModel> listAnswer;
        private int focusIndex;


        public ConversationPagerAdapter(Context context, IList<UnitModel> Items)
        {
            this.context = context;
            this.Items = Items;
        }
        protected ConversationPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override int Count => 2;

        public override bool IsViewFromObject(View view, Java.Lang.Object objects)
        {
            return view == objects;
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            int margin = (int)Util.Util.PxFromDp(context, 4);
            listAnswer = new List<AnswerModel>();
            listTextIndex = new List<AnswerModel>();
            foreach (var item in Items.Select(d => d.Answers))
            {
                listAnswer.AddRange(item);
            }
            CountAnswer = listAnswer.Count;
            listTextIndex = listAnswer.Where(d => d.Value).Select(d => new AnswerModel
            {
                Text = "##########",
                Value = true
            }).ToList();
            View view = LayoutInflater.FromContext(context).Inflate(OrientationScreen == 2 ? Resource.Layout.conversation_fill_list_sentence_layout : Resource.Layout.conversation_fill_list_sentence_layout, null);
      
            ListView lstView = view.FindViewById<ListView>(Resource.Id.lstView);
            lstView.DividerHeight = 0;
            lstView.Divider = null;
            lstView.ItemClick += (s, e) =>
            {
                var audio = Items.ElementAt(e.Position).Audios;
            };
            NXPlayButton btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btnPlay);
            FlexboxLayout flexbox = view.FindViewById<FlexboxLayout>(Resource.Id.flexAnswer);
            Button btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Click += (s, e) => {
                NextClickHandler?.Invoke(btnNext, position);
            };
            if (Items[0].Audios.Count > 0)
            {
                btnPlay.Visibility = ViewStates.Visible;
            }
            else
            {
                btnPlay.Visibility = ViewStates.Gone;
            }
            if (position == 0)
            { 
                btnNext.Visibility = ViewStates.Visible;
                ListConversationNormalAdapter adapter = new ListConversationNormalAdapter(context, Items,1);
                lstView.Adapter = adapter;
            }
            else
            { 
                btnNext.Visibility = ViewStates.Gone;
                ListConversationAdapter adapter = new ListConversationAdapter(context, Items, listTextIndex);
                lstView.Adapter = adapter;
                adapter.AnswerClickedHandler += (s, e) =>
                {
                    if (focusIndex == -1)
                    {
                        return;
                    }

                    adapter.FocusIndex = e;
                    if (!listTextIndex[e].Text.Contains("####"))
                    {
                        var answer = listTextIndex[e];
                        TextView btn = new TextView(context);
                        btn.SetTextColor(Color.Black);
                        btn.SetPadding(margin * 2, margin, margin * 2, margin);
                        if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                        {
                            btn.Elevation = margin / 4;
                        }
                        btn.SetTextSize(ComplexUnitType.Sp, 14);
                        btn.TransformationMethod = null;


                        btn.Text = answer.Text;

                        flexbox.AddView(btn, new ViewGroup.LayoutParams(-2, -2));
                        FlexboxLayout.LayoutParams layoutparam = (FlexboxLayout.LayoutParams)btn.LayoutParameters;

                        layoutparam.SetMargins(margin, margin, margin, margin);

                        btn.Background = GetBackground(Color.White);
                        btn.Click += (ss, ee) =>
                        {
                            flexbox.RemoveView(btn);
                            btn.Dispose();
                            listTextIndex[focusIndex] = answer;
                        };
                        listTextIndex[e] = new AnswerModel
                        {
                            Value = true,
                            Text = "##########"
                        };
                    }
                    adapter.NotifyDataSetChanged();
                    focusIndex = e;
                };


                for (int i = 0; i < listAnswer.Count; i++)
                {
                    TextView btn = new TextView(context);
                    btn.SetTextColor(Color.Black);
                    btn.SetPadding(margin * 2, margin, margin * 2, margin);
                    if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                    {
                        btn.Elevation = margin / 4;
                    }
                    btn.SetTextSize(ComplexUnitType.Sp, 14);
                    btn.TransformationMethod = null;

                    var answer = listAnswer.ElementAt(i);
                    btn.Text = answer.Text;
                    flexbox.AddView(btn, new ViewGroup.LayoutParams(-2, -2));
                    FlexboxLayout.LayoutParams layoutparam = (FlexboxLayout.LayoutParams)btn.LayoutParameters;

                    layoutparam.SetMargins(margin, margin, margin, margin);

                    btn.Background = GetBackground(Color.White);
                    btn.Click += (s, e) =>
                    {
                        flexbox.RemoveView(btn);
                        btn.Dispose();
                        listTextIndex[focusIndex] = answer;
                        focusIndex = GetNextIndex();
                        adapter.FocusIndex = GetNextIndex();
                        adapter.NotifyDataSetChanged();
                        if (focusIndex == -1)
                        {
                            btnNext.Visibility = ViewStates.Visible;
                        }
                    };

                }
            }
            
            container.AddView(view);
            return view;
        }

        private Drawable GetBackground(Color color)
        {
            PaintDrawable background = new PaintDrawable(color);
            background.SetCornerRadius((int)Util.Util.PxFromDp(context, 4));
            background.Shape = new RectShape();
            return background;
        }
        private int GetNextIndex()
        {
            for (int i = 0; i < listTextIndex.Count; i++)
            {
                if (listTextIndex[i].Text.Contains("####") && listTextIndex[i].Value)
                {
                    return i;
                }
            }
            return -1;
        }
        private int GetIndexByRowPosition(int row, int position)
        {
            if (row >= Items.Count)
                return -1;
            int index = 0;
            for (int i = 0; i < row; i++)
            {
                var temp = " " + Items[i].Input[0].Trim() + " ";
                int count = Regex.Split(temp, "%%").Length - 1;
                if (count > 0)
                    index += count;
            }
            return index;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        { 
            container.RemoveView((View)@object);
        }
    }
}