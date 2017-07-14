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
using Com.Bumptech.Glide;
using Naxam.Busuu.Learning.Model;
using Com.Google.Android.Flexbox;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using System.Text.RegularExpressions;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class ConversationFillListSentence : LinearLayout
    {
        public IList<UnitModel> Items;
        int CountAnswer;
        public int OrientationScreen;
        List<AnswerModel> listTextIndex;
        List<AnswerModel> listAnswer;


        private int focusIndex;
        public ConversationFillListSentence(Context context) : base(context)
        {
        }

        public ConversationFillListSentence(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public ConversationFillListSentence(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public ConversationFillListSentence(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected ConversationFillListSentence(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public View Init()
        {
            RemoveAllViews();
            int margin = (int)Util.Util.PxFromDp(Context, 4);
            listAnswer = new List<AnswerModel>();
            listTextIndex = new List<AnswerModel>();
            foreach (var item in Items.Select(d => d.Answers))
            {
                listAnswer.AddRange(item);
            }
            Random random = new Random();
            CountAnswer = listAnswer.Count;
            listTextIndex = listAnswer.Where(d => d.Value).Select(d => new AnswerModel
            {
                Text = "##########",
                Value = true
            }).ToList();
            List<AudioModel> listAudio = Items.Select(d => d.Audios.FirstOrDefault()).ToList();
            View view = LayoutInflater.FromContext(Context).Inflate(Resource.Layout.conversation_fill_list_sentence_layout, null);
            ListView lstView = view.FindViewById<ListView>(Resource.Id.lstView);
            NXPlayButton btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btnPlay);
            ListConversationAdapter adapter = new ListConversationAdapter(Context, Items, listTextIndex);
            lstView.DividerHeight = 0;
            lstView.Divider = null;
            lstView.ItemClick += (s, e) =>
            {
                AudioModel audio = Items.ElementAt(e.Position).Audios.FirstOrDefault();
                btnPlay.Play(audio.Start, audio.End);
            };
            btnPlay.PositionChanged += (s, e) =>
            {
                AudioModel selectedAudio = listAudio.Where(d => d.Start <= e / 1000 && d.End > e / 1000).FirstOrDefault();
                focusIndex = listAudio.IndexOf(selectedAudio);
                lstView.SetSelection(focusIndex);
                if (adapter.FocusIndex != focusIndex)
                {
                    adapter.FocusIndex = focusIndex;
                    adapter.NotifyDataSetChanged();
                }

            };

            FlexboxLayout flexbox = view.FindViewById<FlexboxLayout>(Resource.Id.flexAnswer);
            if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            {
                ((LinearLayout)flexbox.Parent).Elevation = margin / 2;
                btnPlay.Elevation = margin / 2 + 2;
            }
            Button btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Visibility = ViewStates.Gone;
            
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
                    TextView btn = new TextView(Context);
                    btn.SetTextColor(Color.Black);
                    btn.SetPadding(margin * 2, margin, margin * 2, margin);
                    if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                    {
                        btn.Elevation = margin / 2 + 1;
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
            if (Items[0].Audios.Count > 0)
            {
                btnPlay.Visibility = ViewStates.Visible;
            }
            else
            {
                btnPlay.Visibility = ViewStates.Gone;
            }

            for (int i = 0; i < listAnswer.Count; i++)
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
            AddView(view, new LinearLayout.LayoutParams(-1, -1));

            return view;
        }
        private Drawable GetBackground(Color color)
        {
            PaintDrawable background = new PaintDrawable(color);
            background.SetCornerRadius((int)Util.Util.PxFromDp(Context, 4));
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
    }
}