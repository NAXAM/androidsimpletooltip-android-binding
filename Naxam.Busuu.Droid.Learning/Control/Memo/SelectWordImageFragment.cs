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
using Android.Support.V7.Widget;
using Naxam.Busuu.Droid.Learning.Adapters;
using Naxam.Busuu.Learning.Model;
using static Android.Support.V7.Widget.RecyclerView;
using Android.Graphics;
using Android.Views.Animations;
using Android.Animation;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class SelectWordImageFragment : MemoriseFragmentBase
    {
        public override event EventHandler<bool> NextClicked;
        TextView txtQuestion;
        NXPlayButton btnPlay;
        Button btnNext;
        RecyclerView recyclerView;

        public SelectWordImageFragment(UnitModel Item)
        {
            this.Item = Item;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.select_word_with_image_layout, container, false);
            Init(view);
            return view;
        }

        bool corrects, clicked;
        private void Init(View view)
        {
            txtQuestion = view.FindViewById<TextView>(Resource.Id.txtQuestion);
            btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            SelectWordImageRecyclerViewAdapter adapter = new SelectWordImageRecyclerViewAdapter(Context, Item.Answers);
            GridLayoutManager grid = new GridLayoutManager(Context, 1);
            recyclerView.SetLayoutManager(grid);
            recyclerView.SetAdapter(adapter);
            ItemtouchListener touch = new ItemtouchListener(Context);

            touch.Clicked += (s, e) =>
            {
                if (clicked)
                    return;
                clicked = true;
                View itemView = ((View)s);
                ImageView imgResult = itemView.FindViewById<ImageView>(Resource.Id.imgResult);
                TextView txtAnswer = itemView.FindViewById<TextView>(Resource.Id.txtAnswer);
                txtAnswer.SetTextColor(Color.White);
                imgResult.Visibility = ViewStates.Visible;
                if (Item.Answers[e].Value)
                {
                    itemView.SetBackgroundColor(Color.ParseColor("#74B825"));
                    corrects = true;
                }
                else
                {
                    itemView.SetBackgroundColor(Color.ParseColor("#EE6253"));

                    float distance = Util.Util.PxFromDp(Context, 8);
                    AnimatorSet mAnimatorSet = new AnimatorSet();
                    var anim = ObjectAnimator.OfFloat(itemView, "TranslationX", distance, -distance, 0);
                    anim.RepeatCount = 10;
                    anim.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                    mAnimatorSet.Play(anim);
                    

                    mAnimatorSet.SetDuration(50);
                    mAnimatorSet.Start();

                }
                for (int i = 0; i < Item.Answers.Count; i++)
                {
                    var count = recyclerView.ChildCount;
                    if (i == e)
                    {
                        continue;
                    }
                    if (Item.Answers[i].Value)
                    {
                        recyclerView.GetChildAt(i).SetBackgroundColor(Color.ParseColor("#74B825"));
                        recyclerView.GetChildAt(i).FindViewById<TextView>(Resource.Id.txtAnswer).SetTextColor(Color.White);
                        recyclerView.GetChildAt(i).FindViewById<ImageView>(Resource.Id.imgResult).Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        recyclerView.GetChildAt(i).FindViewById<TextView>(Resource.Id.txtAnswer).Alpha = 0.8f;
                        var img = recyclerView.GetChildAt(i).FindViewById<ImageView>(Resource.Id.imgResult);
                        img.SetBackgroundColor(Color.ParseColor("#80ffffff"));
                        img.SetImageResource(0);
                        img.Visibility = ViewStates.Visible;
                    }
                }
            };
            recyclerView.AddOnItemTouchListener(touch);
            txtQuestion.Text = Item.Title;

        }
    }
    public class GestureListener : GestureDetector.SimpleOnGestureListener
    {
        public override bool OnSingleTapUp(MotionEvent e)
        {
            return true;
        }
    }

    public class ItemtouchListener : Java.Lang.Object, IOnItemTouchListener
    {
        public event EventHandler<int> Clicked;
        private GestureDetector gestureDetector;
        public ItemtouchListener(Context context)
        {
            var ges = new GestureDetector.SimpleOnGestureListener();

            gestureDetector = new GestureDetector(context, new GestureListener());
        }
        public bool OnInterceptTouchEvent(RecyclerView recyclerView, MotionEvent e)
        {
            View child = recyclerView.FindChildViewUnder(e.GetX(), e.GetY());
            if (child != null && gestureDetector.OnTouchEvent(e))
            {
                Clicked?.Invoke(child, recyclerView.GetChildAdapterPosition(child));
            }
            return false;
        }

        public void OnRequestDisallowInterceptTouchEvent(bool disallow)
        {

        }

        public void OnTouchEvent(RecyclerView recyclerView, MotionEvent @event)
        {

        }
    }
}