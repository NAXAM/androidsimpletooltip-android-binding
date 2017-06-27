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

        public MvxObservableCollection<TopicModel> Topics
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
            Topics = new MvxObservableCollection<TopicModel>();
            for (int i = 0; i < 10; i++)
            {
                Topics.Add(new TopicModel
                {
                    Exercises = new MvxObservableCollection<ExerciseModel>
                    {
                        new ExerciseModel(),
                        new ExerciseModel(),
                        new ExerciseModel(),
                    }
                });
            }
            string[] color = new string[]
            {
                "#58B0F8","#B02018"
            };
            Lessons = new MvxObservableCollection<LessonModel>();
            for (int i = 0; i < 10; i++)
            {
                Lessons.Add(new LessonModel(Topics)
                {
                    Id = i,
                    Name = "Lesson " + i,
                    Title = " title " + i,
                    Color = color[i % 2]
                });
            }
        }

    }
}
