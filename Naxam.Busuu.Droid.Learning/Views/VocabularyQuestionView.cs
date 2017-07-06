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
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Learning.Model;
using Android.Graphics;
using Android.Views.Animations;
using Naxam.Busuu.Droid.Learning.Control;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity]
    public class VocabularyQuestionView : MvxAppCompatActivity
    {
        private LinearLayout vocabularyQuestionLayout;
        private ImageView imDescription;
        private TextView tvQuestion;
        private Button btVocabularyContinue;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait ? Resource.Layout.vocabulary_question : Resource.Layout.landscape_vocabulary_question);

            InitInterface();
        }

        public void InitInterface()
        {
            vocabularyQuestionLayout = FindViewById<LinearLayout>(Resource.Id.vocabulary_question_layout);
            imDescription = FindViewById<ImageView>(Resource.Id.im_vocabulary_description);
            tvQuestion = FindViewById<TextView>(Resource.Id.tv_vocabulary_question);
            btVocabularyContinue = FindViewById<Button>(Resource.Id.bt_vocabulary_continue);

            VocabularyQuestionModel model = new VocabularyQuestionModel()
            {
                imageDescriptionName = "tall_short_couble.jpg",
                questionSentence = "Click on the word opposite of the word \"tall\".",
                listAnswer = new List<string>()
                {
                    "old",
                    "short",
                    "tall"
                },
                trueAnswer = 2
            };

            tvQuestion.Text = model.questionSentence;
            for (int i = 0; i < model.listAnswer.Count; i++)
            {
                VocalbularyAnswer answer = new VocalbularyAnswer(this)
                {
                    isTrue = i == model.trueAnswer ? true : false
                };
                LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, WindowManagerLayoutParams.WrapContent);
                param.RightMargin = 64;
                param.LeftMargin = 64;
                param.BottomMargin = 32;
                answer.Text = model.listAnswer[i];
                answer.LayoutParameters = param;
                answer.SetBackgroundColor(Color.White);
                answer.SetTextColor(Color.Black);
                answer.TransformationMethod = null;
                vocabularyQuestionLayout.AddView(answer);
                answer.Click += Answer_Click;
            }


        }

        private void Answer_Click(object sender, EventArgs e)
        {
            ((VocalbularyAnswer)sender).SetBackgroundColor(((VocalbularyAnswer)sender).isTrue ? Color.Green : Color.Red);
            ((VocalbularyAnswer)sender).SetTextColor(Color.White);
            if (((VocalbularyAnswer)sender).isTrue == false)
            {
                TranslateAnimation translate = new TranslateAnimation(0, 10, 0, 0);
                translate.Duration = 50;
                translate.FillAfter = true;
                ((VocalbularyAnswer)sender).StartAnimation(translate);
                if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat) ((VocalbularyAnswer)sender).Elevation = 8;
                translate.SetAnimationListener(new AnimationListener()
                {
                    AnimationEnd = (a) =>
                    {
                        TranslateAnimation translate2 = new TranslateAnimation(0, -10, 0, 0);
                        translate2.Duration = 50;
                        translate2.FillAfter = true;
                        ((VocalbularyAnswer)sender).StartAnimation(translate2);
                        translate2.SetAnimationListener(new AnimationListener()
                        {
                            AnimationEnd = (b) =>
                            {
                                TranslateAnimation translate3 = new TranslateAnimation(0, 10, 0, 0);
                                translate3.Duration = 50;
                                translate3.FillAfter = true;
                                ((VocalbularyAnswer)sender).StartAnimation(translate3);
                                translate3.SetAnimationListener(new AnimationListener()
                                {
                                    AnimationEnd = (c) =>
                                    {
                                        TranslateAnimation translate4 = new TranslateAnimation(0, -5, 0, 0);
                                        translate4.Duration = 50;
                                        translate4.FillAfter = true;
                                        ((VocalbularyAnswer)sender).StartAnimation(translate4);

                                    }
                                });
                            }
                        });
                    }
                });
            }

            for (int i = 0; i < vocabularyQuestionLayout.ChildCount; i++)
            {
                View child = vocabularyQuestionLayout.GetChildAt(i);
                child.Enabled = false;
            }

            btVocabularyContinue.Visibility = ViewStates.Visible;
        }
    }

    public class VocalbularyAnswer : Button
    {
        public bool isTrue;

        public VocalbularyAnswer(Context context) : base(context)
        {
        }

        public VocalbularyAnswer(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public VocalbularyAnswer(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public VocalbularyAnswer(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected VocalbularyAnswer(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }


    }
}