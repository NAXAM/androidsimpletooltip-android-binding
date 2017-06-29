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
using MvvmCross.Droid.Support.V7.AppCompat;
using Com.Ittianyu.Bottomnavigationviewex;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Binding.Droid.Views;
using Naxam.Busuu.Droid.Learning.Control;
using MvvmCross.Binding.Droid.BindingContext;
using Naxam.Busuu.Learning.Model;
using System.Threading.Tasks;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Theme = "@style/AppTheme.NoActionBar")]
    public class MainView : MvxAppCompatActivity
    {
        MvxExpandableListView expLessons;
        BottomNavigationViewEx menu;
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.LearnActivity);
            menu = FindViewById<BottomNavigationViewEx>(Resource.Id.menu_bottom);
            menu.EnableShiftingMode(false);
            OnGroupClickListener GroupClick = new OnGroupClickListener(this);
            menu.NavigationItemSelected += Menu_NavigationItemSelected;
            expLessons = FindViewById<MvxExpandableListView>(Resource.Id.expLessons);
            expLessons.SetOnGroupClickListener(GroupClick);
            expLessons.SetOnTouchListener(GroupClick);
            expLessons.GroupExpand += ExpLessons_GroupExpand;
            expLessons.GroupCollapse += ExpLessons_GroupCollapse; 
            expLessons.Scroll += ExpLessons_Scroll;
        }

        private void ExpLessons_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {

        }

        private void ExpLessons_GroupCollapse(object sender, ExpandableListView.GroupCollapseEventArgs e)
        {
            //expLessons.SetSelection(e.GroupPosition);
            // expLessons.SmoothScrollToPosition(e.GroupPosition);
            //  expLessons.OverScrollMode = OverScrollMode.Always;
            expLessons.SmoothScrollToPositionFromTop(e.GroupPosition, 0);
        }

        private void ExpLessons_GroupExpand(object sender, ExpandableListView.GroupExpandEventArgs e)
        {
            expLessons.SmoothScrollToPositionFromTop(e.GroupPosition, 0); 
            //expLessons.SetSelection(e.GroupPosition);
        }

        float x;
        float y;
        public override bool OnTouchEvent(MotionEvent e)
        {
            x = e.GetX();
            y = e.GetY();
            return base.OnTouchEvent(e);
        }


        private void Menu_NavigationItemSelected(object sender, Android.Support.Design.Widget.BottomNavigationView.NavigationItemSelectedEventArgs e)
        {

        }
    }
    class OnGroupClickListener : Java.Lang.Object, ExpandableListView.IOnGroupClickListener, View.IOnTouchListener
    {
        public float x { set; get; }
        public float y { set; get; }
        Activity context;
        public OnGroupClickListener(Activity context)
        {
            this.context = context;
        }



        public bool OnGroupClick(ExpandableListView parent, View clickedView, int groupPosition, long id)
        {
            // parent.SetSelection(groupPosition);
            // parent.SmoothScrollToPosition(groupPosition);
            var view = (LessonHeaderBackground)clickedView;
            view.InitAnim(x, y);
            view.IsExpand = parent.IsGroupExpanded(groupPosition);
            return false;
        }

        public bool OnTouch(View v, MotionEvent e)
        {

            x = e.GetX();
            y = e.GetY();
            System.Diagnostics.Debug.WriteLine("====" + x + "====" + y);


            return false;
        }
    }
}