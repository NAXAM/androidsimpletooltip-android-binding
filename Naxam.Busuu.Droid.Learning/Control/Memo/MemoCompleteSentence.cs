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
using Com.Bumptech.Glide;
using Android.Text;
using static Android.Widget.TextView;
using Android.Text.Style;
using Android.Graphics;
using Android.Views.InputMethods;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class CompleteSentenceFragment : BaseFragment
    {
        public override event EventHandler<int> NextClicked;

        private ImageView imgImage;
        private NXPlayButton btnPlay;
        private TextView txtTitle;
        private TextView txtQuestion;
        private TextView txtTrueAnswer;
        private EditText edtAnswer;
        private Button btContinue;

        bool correct;

        public CompleteSentenceFragment(UnitModel item)
        {
            this.Item = item;
        }

        private string[] listInputString;
        private int targetIndex = 0;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.complete_sentence_question, container, false);

            InitInterface(view);
            return view;
        }

        public void InitInterface(View view)
        {
            imgImage = view.FindViewById<ImageView>(Resource.Id.img_complete_sentence_description);
            btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btn_complete_sentence_play);
            txtTitle = view.FindViewById<TextView>(Resource.Id.txt_compelte_sentence_title);
            txtQuestion = view.FindViewById<TextView>(Resource.Id.txt_compelte_sentence_question);
            txtTrueAnswer = view.FindViewById<TextView>(Resource.Id.txt_compelte_sentence_true_answer);
            edtAnswer = view.FindViewById<EditText>(Resource.Id.edt_compelte_sentence_question);
            btContinue = view.FindViewById<Button>(Resource.Id.bt_complete_sentence_continue);
            listInputString = SplitStringToList(Item.Input[0]);
            targetIndex = GetTargetIndex(Item.Input[0]);

            if (Item.Images == null) imgImage.Visibility = ViewStates.Invisible;
            else
            {
                Glide.With(this).Load(Item.Images[0]).CenterCrop().Into(imgImage);
            }

            if (Item.Audios == null) btnPlay.Visibility = ViewStates.Invisible;
            if (Item.Title != null) txtTitle.Text = Item.Title;
            else txtTitle.Visibility = ViewStates.Invisible;

            txtQuestion.SetText(CreateStringBuilder(listInputString, targetIndex), BufferType.Normal);
            txtTrueAnswer.SetText(CreateTrueAnswer(listInputString, Item.Answers[0].Text, targetIndex), BufferType.Normal);
            txtTrueAnswer.Visibility = ViewStates.Invisible;

            txtQuestion.Click += (s, e) =>
            {
                InputMethodManager imm = (InputMethodManager)view.Context.GetSystemService(Context.InputMethodService);
                imm.ShowSoftInput(edtAnswer, InputMethodManager.ShowImplicit);
            };

            edtAnswer.TextChanged += (s, e) =>
            {
                if (edtAnswer.Text.Trim().Length != 0) txtQuestion.SetText(FillingAnswer(edtAnswer.Text, Color.Blue), BufferType.Normal);
                else txtQuestion.SetText(CreateStringBuilder(listInputString, targetIndex), BufferType.Normal);
            };

            btContinue.Click += (s, e) =>
            {
                txtQuestion.SetText(CheckAnswer(edtAnswer.Text.Trim().ToLower(), Item.Answers[0].Text.Trim().ToLower()), BufferType.Normal);
                txtTrueAnswer.Visibility = ViewStates.Visible;
                txtQuestion.Enabled = false;
                btContinue.Enabled = true;
                NextClicked?.Invoke(btContinue, correct ? 1 : 0);
            };
        }

        public SpannableStringBuilder FillingAnswer(string inputString, Color color)
        {
            SpannableStringBuilder builder = new SpannableStringBuilder();
            for (int i = 0; i < listInputString.Length; i++)
            {
                builder.Append(listInputString[i]);
                if (i < listInputString.Length - 1)
                {
                    SpannableString spstring = new SpannableString(inputString);
                    spstring.SetSpan(new ForegroundColorSpan(color), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
                    builder.Append(spstring);
                }
            }
            return builder;
        }

        public SpannableStringBuilder CheckAnswer(string inputString, string compareString)
        {
            SpannableStringBuilder builder = new SpannableStringBuilder();
            correct = true;
            if (inputString.Trim().Equals(compareString.Trim()))
            {
                for (int i = 0; i < listInputString.Length; i++)
                {
                    builder.Append(listInputString[i]);
                    if (i < listInputString.Length - 1)
                    {
                        SpannableString spstring = new SpannableString(inputString);
                        spstring.SetSpan(new ForegroundColorSpan(Resources.GetColor(Resource.Color.colorAnswer)), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
                        builder.Append(spstring);
                    }
                }
            }
            else
            {
                correct = false;
                for (int i = 0; i < listInputString.Length; i++)
                {
                    builder.Append(listInputString[i]);
                    if (i < listInputString.Length - 1)
                    {
                        SpannableString spstring = new SpannableString(inputString + " " + compareString);
                        spstring.SetSpan(new ForegroundColorSpan(Color.Red), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
                        spstring.SetSpan(new StrikethroughSpan(), 0, inputString.Length, SpanTypes.ExclusiveExclusive);
                        spstring.SetSpan(new ForegroundColorSpan(Resources.GetColor(Resource.Color.colorAnswer)), inputString.Length + 1, inputString.Length + 1 + compareString.Length, SpanTypes.ExclusiveExclusive);
                        builder.Append(spstring);
                    }
                }
            }

            return builder;
        }

        public SpannableStringBuilder CreateStringBuilder(string[] listInputString, int target)
        {
            SpannableStringBuilder builder = new SpannableStringBuilder();
            for (int i = 0; i < listInputString.Length; i++)
            {
                builder.Append(listInputString[i]);
                if (i < listInputString.Length - 1)
                {
                    builder.Append("_____");
                }
            }

            return builder;

        }

        public SpannableStringBuilder CreateTrueAnswer(string[] listInputString, string trueAnswer, int targetIndex)
        {

            SpannableStringBuilder builder = new SpannableStringBuilder();
            for (int i = 0; i < listInputString.Length; i++)
            {
                builder.Append(listInputString[i]);
                if (i < listInputString.Length - 1)
                {
                    SpannableString spstring = new SpannableString(trueAnswer);
                    spstring.SetSpan(new StyleSpan(TypefaceStyle.Bold), 0, trueAnswer.Length, SpanTypes.ExclusiveExclusive);
                    builder.Append(spstring);
                }
            }

            return builder;
        }
        public string[] SplitStringToList(string inputString)
        {
            inputString = " " + inputString + " ";
            return inputString.Split(new string[] { "%%" }, StringSplitOptions.None);
        }

        public int GetTargetIndex(string inputString)
        {
            int start = 0, count = 0;
            for (int i = 1; i < inputString.Length; i++)
            {
                if (inputString[i].Equals('%') && inputString[i].Equals('%'))
                {
                    return i - 1;
                }
            }
            return 0;
        }
    }
}