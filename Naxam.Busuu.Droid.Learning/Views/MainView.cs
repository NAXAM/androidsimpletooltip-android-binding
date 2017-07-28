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
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity(Theme = "@style/AppTheme.NoActionBar",MainLauncher = false)]
    public class MainView : MvxAppCompatActivity
    {
        NXMvxExpandableListView expLessons;
        BottomNavigationViewEx menu;
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.LearnActivity);
            menu = FindViewById<BottomNavigationViewEx>(Resource.Id.menu_bottom);
            menu.EnableShiftingMode(false);
            OnGroupClickListener GroupClick = new OnGroupClickListener(this);
            menu.NavigationItemSelected += Menu_NavigationItemSelected;
            expLessons = FindViewById<NXMvxExpandableListView>(Resource.Id.expLessons);
            NXMvxExpandableListAdapter adapter = new NXMvxExpandableListAdapter(this, (IList<LessonModel>)expLessons.ItemsSource, (IMvxAndroidBindingContext)BindingContext)
            {
                GroupTemplateId = expLessons.GroupTemplateId,
                ItemTemplateId = expLessons.ItemTemplateId
            };
            adapter.ExerciseClick += (s, e) =>
            {
                if (expLessons.ExerciseClickCommand == null)
                    return;
                if (expLessons.ExerciseClickCommand.CanExecute(e))
                {
                    expLessons.ExerciseClickCommand.Execute(e);
                }
            };
            adapter.DownloadClick += (s, e) =>
            {
                if (expLessons.DownloadCommand == null)
                    return;
                if (expLessons.DownloadCommand.CanExecute(e))
                {
                    expLessons.DownloadCommand.Execute(e);
                }
            };
            adapter.DoneAnim += (s, e) =>
            {
                if (!e)
                {
                    expLessons.ExpandGroup((int)s);
                }
                else
                {
                    expLessons.CollapseGroup((int)s);
                }
            };
            expLessons.SetAdapter(adapter);

            expLessons.SetOnGroupClickListener(GroupClick);
            expLessons.SetOnTouchListener(GroupClick);
            expLessons.GroupExpand += ExpLessons_GroupExpand;
            expLessons.GroupCollapse += ExpLessons_GroupCollapse;
            expLessons.OffsetTopAndBottom(0);
        }


        MvxObservableCollection<LessonModel> GetData()
        {
            string[] color = new string[]
          {
                "#58B0F8","#B02018"
          };
            Random random = new Random();
            var Topicsx = new MvxObservableCollection<TopicModel>();
            for (int i = 0; i < 10; i++)
            {
                Topicsx.Add(new TopicModel
                {
                    Toppic = "Topic " + random.Next(1, 1000),
                    Time = random.Next(1, 50),
                    Exercises = new MvxObservableCollection<ExerciseModel>
                    {
                        new ExerciseModel(),
                        new ExerciseModel(),
                        new ExerciseModel(),
                    }
                });
            }

            string[] icons = new string[]
            {

            };


            var Lessons = new MvxObservableCollection<LessonModel>();
            for (int i = 0; i < 10; i++)
            {
                var lesson = new LessonModel(Topicsx)
                {
                    Id = i,
                    LessonNumber = "Lesson " + random.Next(1, 50),
                    LessonName = " title " + random.Next(1, 50),
                    Color = color[i % 2],
                    Percent = random.Next(1, 100),
                    Icon = "http://www.jeremedia.ca/japan/domo1.jpg"
                };
                Lessons.Add(lesson);
            }

            return Lessons;
        }

        private void ExpLessons_GroupCollapse(object sender, ExpandableListView.GroupCollapseEventArgs e)
        {
            //expLessons.SetSelection(e.GroupPosition);
            // expLessons.SmoothScrollToPosition(e.GroupPosition);
            //  expLessons.OverScrollMode = OverScrollMode.Always;
            expLessons.SmoothScrollToPositionFromTop(e.GroupPosition, 0, 500);

        }



        private void ExpLessons_GroupExpand(object sender, ExpandableListView.GroupExpandEventArgs e)
        {
            expLessons.SmoothScrollToPositionFromTop(e.GroupPosition, 0, 500);
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
            //  view.InitAnim(x, y);
            //view.IsExpand = parent.IsGroupExpanded(groupPosition);
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