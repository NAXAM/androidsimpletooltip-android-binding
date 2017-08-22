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

namespace Naxam.Busuu.Droid.Social.Views
{
    [Activity(Label = "GiveFeedbackAnswerView", MainLauncher =true)]
    public class GiveFeedbackAnswerView : AppCompatActivity, ITextWatcher
    {
        private EditText edtAnswer;
        private int Start;
        private Toolbar toolbar;
        private int len;
        public void AfterTextChanged(IEditable s)
        {
            if (Start + 1 <= s.Length() && len <= s.Length())
                s.SetSpan(new ForegroundColorSpan(Color.ParseColor("#7A965F")), Start, Start + 1, SpanTypes.InclusiveInclusive);
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            // throw new NotImplementedException();
            len = s.Length();
            Start = start;
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            //throw new NotImplementedException();
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
        }
    }
}