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
using Android.Animation;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class MemoConversationSentence : MemoriseFragmentBase
    {
        private ImageView imDescription;
        private TextView txtDescription;
        private TextView txtHint;
        private TextView txtShowHint;
        private LinearLayout layoutInputMethod;
        private LinearLayout layoutInputWrite;
        private LinearLayout layoutInputSpeak;
        private Button btWrite;
        private Button btSpeak;
        private Space viewSpace;
        private EditText edtAnswer;
        private Button btRecord;
        private RelativeLayout layoutSent;
        private TextView txtSuggestNumberWord;
        private Button btSent;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.conversation_question, container, false);
            InitInterface(view);
            return view;
        }

        public void InitInterface(View view)
        {
            imDescription = view.FindViewById<ImageView>(Resource.Id.im_description);
            txtDescription = view.FindViewById<TextView>(Resource.Id.txt_description);
            txtHint = view.FindViewById<TextView>(Resource.Id.txt_hint);
            txtShowHint = view.FindViewById<TextView>(Resource.Id.txt_show_hint);
            layoutInputMethod = view.FindViewById<LinearLayout>(Resource.Id.layout_input_method);
            layoutInputWrite = view.FindViewById<LinearLayout>(Resource.Id.layout_input_write);
            layoutInputSpeak = view.FindViewById<LinearLayout>(Resource.Id.layout_input_speak);
            btWrite = view.FindViewById<Button>(Resource.Id.bt_write);
            btSpeak = view.FindViewById<Button>(Resource.Id.bt_speak);
            edtAnswer = view.FindViewById<EditText>(Resource.Id.edt_answer);
            btRecord = view.FindViewById<Button>(Resource.Id.bt_record);
            layoutSent = view.FindViewById<RelativeLayout>(Resource.Id.layout_sent);
            txtSuggestNumberWord = view.FindViewById<TextView>(Resource.Id.txt_suggest_number_word);
            btSent = view.FindViewById<Button>(Resource.Id.bt_sent);
            viewSpace = view.FindViewById<Space>(Resource.Id.view_space);

            txtShowHint.Click += (s, e) =>
            {
                txtHint.Visibility = txtHint.Visibility == ViewStates.Gone ? ViewStates.Visible : ViewStates.Gone;
            };

            btWrite.Click += (s, e) =>
            {
                //layoutSent.Visibility = ViewStates.Visible;
                layoutInputSpeak.Visibility = ViewStates.Gone;
                ObjectAnimator animator = ObjectAnimator.OfInt(viewSpace, "LayoutParameters.Width", 64,0);
                animator.SetDuration(500);

                //animator.Update += (sa, ea) =>
                //{
                //    viewSpace.LayoutParameters.Width = (int)ea.Animation.AnimatedValue;
                //};

                //animator.AnimationEnd += (sa, ea) =>
                //{
                //    layoutInputMethod.Visibility = ViewStates.Gone;
                //    edtAnswer.Visibility = ViewStates.Visible;
                //};

                animator.Start();

            };

            btSpeak.Click += (s, e) =>
            {

            };
        }

        private void Animator_Update(object sender, ValueAnimator.AnimatorUpdateEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ResetToDefault()
        {
            txtHint.Visibility = ViewStates.Gone;
            layoutInputMethod.Visibility = ViewStates.Visible;
            btSpeak.Visibility = ViewStates.Visible;
            btWrite.Visibility = ViewStates.Visible;
            layoutSent.Visibility = ViewStates.Gone;
            txtSuggestNumberWord.Text = "3 words to complete";
            edtAnswer.Visibility = ViewStates.Gone;
            viewSpace.LayoutParameters.Width = 64;
        }


    }
}