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
using Android.Support.V7.App;
using Android.Text;
using Java.Lang;
using Android.Graphics;
using Android.Text.Style;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Naxam.Busuu.Droid.Social.Views
{
    [Activity(Label = "GiveFeedbackAnswerView", MainLauncher =true)]
    public class GiveFeedbackAnswerView : MvxAppCompatActivity, ITextWatcher, View.IOnClickListener
    {
        private ImageView btnSend;
        private EditText edtAnswer;
        private int Start;
        private Toolbar toolbar;
        private int len;
        public void AfterTextChanged(IEditable s)
        {
            if (Start + 1 <= s.Length() && len <= s.Length())
                s.SetSpan(new ForegroundColorSpan(Color.ParseColor("#00D800")), Start, Start + 1, SpanTypes.InclusiveInclusive);
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            len = s.Length();
            Start = start;
        }

        public void OnClick(View v)
        {
            if (v.Id == btnSend.Id) {
                Dialog dialog = new Dialog(v.Context);
                dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
                dialog.SetContentView(Resource.Layout.dialog_send);
                dialog.Show();

            }
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_give_feedback_answer);
            init();
            SupportActionBar.Hide();


        }
        private void init()
        {
           edtAnswer = (EditText)FindViewById(Resource.Id.edtAnswer);
           edtAnswer.AddTextChangedListener(this);
            btnSend = (ImageView)FindViewById(Resource.Id.imgSend);
            btnSend.SetOnClickListener(this);
            btnSend.Focusable = true;
            btnSend.RequestFocus();

        }
    }
}