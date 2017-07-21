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
    public class ChooseWordFragment : BaseFragment
    {
        public override event EventHandler<int> NextClicked; 
        private LinearLayout vocabularyQuestionLayout;
        private ImageView imDescription;
        private TextView tvQuestion;
        private Button btVocabularyContinue;

        bool correct;

        public ChooseWordFragment(UnitModel Item)
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
                NextClicked?.Invoke(btVocabularyContinue, correct?1:0);
            };
            tvQuestion.Text = Item.Title;
            if (Item.Images?.Count > 0)
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
                        AnimatorSet mAnimatorSet = new AnimatorSet();
                        var animx = ObjectAnimator.OfFloat(btnAnswer, "TranslationX", translateDistance, -translateDistance, 0);
                        animx.RepeatCount = 8;
                        animx.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                        mAnimatorSet.Play(animx);
                        mAnimatorSet.SetDuration(50);
                        mAnimatorSet.Start();
                        if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                        {
                            btnAnswer.Elevation = translateDistance/4;
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