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
using Android.Graphics;
using Naxam.Busuu.Learning.Model;
using Android.Views.Animations;
using Android.Animation;
using Com.Bumptech.Glide;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class MemoChooseWord : MemoriseFragmentBase
    {
        public override event EventHandler<bool> NextClicked; 
        private LinearLayout vocabularyQuestionLayout;
        private ImageView imDescription;
        private TextView tvQuestion;
        private Button btVocabularyContinue;

        bool correct;

        public MemoChooseWord(UnitModel Item)
        {
            this.Item = Item;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.vocabulary_question, container, false);
            InitInterface(view);
            return view;
        }

        public void InitInterface(View view)
        {
            vocabularyQuestionLayout = view.FindViewById<LinearLayout>(Resource.Id.vocabulary_question_layout);
            imDescription = view.FindViewById<ImageView>(Resource.Id.im_vocabulary_description);
            tvQuestion = view.FindViewById<TextView>(Resource.Id.tv_vocabulary_question);
            btVocabularyContinue = view.FindViewById<Button>(Resource.Id.bt_vocabulary_continue);
            btVocabularyContinue.Click += (s, e) => {
                NextClicked?.Invoke(btVocabularyContinue, correct);
            };
            tvQuestion.Text = Item.Title;
            if (Item.Images.Count > 0)
            {
                imDescription.Visibility = ViewStates.Visible;
                Glide.With(Context).Load(Item.Images[0]).Into(imDescription);
            }
            else
            {
                imDescription.Visibility = ViewStates.Gone;
            }
            

            for (int i = 0; i < Item.Answers.Count; i++)
            {
                TextView btnAnswer = new TextView(Context);
                if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    btnAnswer.Elevation = Util.Util.PxFromDp(Context, 2);
                }
                var temp = Item.Answers[i];
                LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(WindowManagerLayoutParams.MatchParent, (int)Util.Util.PxFromDp(Context, 48));
                param.Gravity = GravityFlags.Center;
                btnAnswer.Gravity = GravityFlags.Center;
                param.RightMargin = 64;
                param.LeftMargin = 64;
                param.BottomMargin = 32;
                btnAnswer.Text = temp.Text;
                btnAnswer.LayoutParameters = param;
                btnAnswer.SetBackgroundColor(Color.White);
                btnAnswer.SetTextColor(Color.Black);
                btnAnswer.TransformationMethod = null;
                vocabularyQuestionLayout.AddView(btnAnswer);
                btnAnswer.Click += (s, e) =>
                {
                    btnAnswer.SetBackgroundColor(temp.Value ? Color.ParseColor("#74B826") : Color.ParseColor("#E54532"));
                    btnAnswer.SetTextColor(Color.White);

                    correct = temp.Value;
                    if (!temp.Value)
                    {
                        int translateDistance = (int)Util.Util.PxFromDp(Context, 8);
                        TranslateAnimation translate = new TranslateAnimation(0, translateDistance, 0, 0);
                        translate.Duration = 100;
                        translate.FillAfter = true;
                        TranslateAnimation translate2 = new TranslateAnimation(0, -translateDistance, 0, 0);
                        translate2.Duration = 100;
                        translate2.FillAfter = true;
                        TranslateAnimation translate3 = new TranslateAnimation(0, translateDistance, 0, 0);
                        translate3.Duration = 100;
                        translate3.FillAfter = true;
                        TranslateAnimation translate4 = new TranslateAnimation(0, -translateDistance, 0, 0);
                        translate4.Duration = 100;
                        translate4.FillAfter = true;

                        translate.SetAnimationListener(new AnimationListener {
                            AnimationEnd = (anim) => {
                                translate2.Start();
                            }
                        });
                        translate.SetAnimationListener(new AnimationListener
                        {
                            AnimationEnd = (anim) => {
                                translate2.Start();
                            }
                        });
                        translate2.SetAnimationListener(new AnimationListener
                        {
                            AnimationEnd = (anim) => {
                                translate3.Start();
                            }
                        });
                        translate3.SetAnimationListener(new AnimationListener
                        {
                            AnimationEnd = (anim) => {
                                translate4.Start();
                            }
                        });
                    


                        btnAnswer.StartAnimation(translate);
                        if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                        {
                            btnAnswer.Elevation = Util.Util.PxFromDp(Context, 2);
                        }

                    }

                    for (int j = 0; j < vocabularyQuestionLayout.ChildCount; j++)
                    {
                        View child = vocabularyQuestionLayout.GetChildAt(j);
                        child.Enabled = false;
                    }

                    btVocabularyContinue.Visibility = ViewStates.Visible;
                };
            }
        }
    }
}