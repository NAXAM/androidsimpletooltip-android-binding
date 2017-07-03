using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModel
{
    public class MainViewModel : MvxViewModel
    {
        private IMvxCommand _GoPremiumCommand;

        public IMvxCommand GoPremiumCommand
        {
            get { return _GoPremiumCommand = _GoPremiumCommand ?? new MvxCommand(RunGoPremiumCommand); }

        }

        void RunGoPremiumCommand()
        {
            ShowViewModel<PremiumViewModel>();
        }

        private IMvxCommand _ChangeLanguageCommand;

        public IMvxCommand ChangeLanguageCommand
        {
            get { return _ChangeLanguageCommand = _ChangeLanguageCommand ?? new MvxCommand(RunChangeLanguageCommand); }

        }

        void RunChangeLanguageCommand()
        {
            ShowViewModel<ChangeLanguageViewModel>();
        }

        private MvxObservableCollection<LessonModel> _lessons;

        public MvxObservableCollection<LessonModel> Lessons
        {
            get { return _lessons; }
            set
            {
                if (_lessons != value)
                {
                    _lessons = value;
                    RaisePropertyChanged();
                }
            }
        }


        private MvxObservableCollection<TopicModel> _topics;

        public MvxObservableCollection<TopicModel> Topicsx
        {
            get { return _topics; }
            set
            {
                if (_topics != value)
                {
                    _topics = value;
                    RaisePropertyChanged();
                }
            }
        }

        public override void Appeared()
        {
            base.Appeared();
            string[] color = new string[]
           {
                "#58B0F8","#B02018"
           };
            Random random = new Random();
            Topicsx = new MvxObservableCollection<TopicModel>();
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


            Lessons = new MvxObservableCollection<LessonModel>();
            for (int i = 0; i < 10; i++)
            {
                var lesson = new LessonModel(Topicsx)
                {
                    Id = i,
                    Name = "Lesson " + random.Next(1, 50),
                    Title = " title " + random.Next(1, 50),
                    Color = color[i % 2],
                    Percent = random.Next(1, 100),
                    Icon = "http://www.jeremedia.ca/japan/domo1.jpg"
                };
                lesson.DownloadHandle += Lesson_DownloadHandle;
                Lessons.Add(lesson);
            }
        }

        private void Lesson_DownloadHandle(object sender, LessonModel e)
        {

        }

        private IMvxCommand _DownloadCommand;

        public IMvxCommand DownloadCommand
        {
            get { return _DownloadCommand = _DownloadCommand ?? new MvxCommand(RunDownloadCommand); }

        }

        void RunDownloadCommand()
        {
            
        }
    }
}
