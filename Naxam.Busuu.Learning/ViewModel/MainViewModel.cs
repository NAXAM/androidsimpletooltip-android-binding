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
                "#58B0F8","#B02018","#FEAB35"
           };
            Random random = new Random();


            string[] icons = new string[]
            {
                "http://app4e.net/busuu/lesson1.png",
                "http://app4e.net/busuu/lesson2.png",
                "http://app4e.net/busuu/lesson3.png",
            };


            Lessons = new MvxObservableCollection<LessonModel>();
            for (int i = 0; i < 10; i++)
            {
                var lesson = new LessonModel(GetTopic(color[i % 3]))
                {
                    Id = i,
                    LessonNumber = "Lesson " + random.Next(1, 50),
                    LessonName = " title " + random.Next(1, 50),
                    Color = color[i % 3],
                    Percent = random.Next(1, 100),
                    Icon = icons[i % 3]
                };
                Lessons.Add(lesson);
            }
        }

        private MvxObservableCollection<TopicModel> GetTopic(string color)
        {
            Random random = new Random();
            List<UnitModel> listUnit = new List<UnitModel>();
            for (int i = 0; i < 5; i++)
            {
                listUnit.Add(new UnitModel
                {
                    Title = "Chọn từ đúng",
                    Type = UnitModel.UnitType.FillSentence,

                    Input = new List<string>
                {
                    "Tôi là %% người %%  trai nhất naxam"
                },
                    Images = new List<string> {
                    "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg",
                },
                    Audios = new List<AudioModel> {
                    new AudioModel
                    {
                        Link = "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg"
                    }
                },
                    Answers = new List<AnswerModel>
                {
                    new AnswerModel
                    {
                        Text = "thảo",
                        Value = true
                    },
                     new AnswerModel
                    {
                        Text = "xấu",
                        Value  = true,
                        Position = 1
                    },
                      new AnswerModel
                    {
                        Text = "đẹp"
                    },
                       new AnswerModel
                    {
                        Text = "nghĩa"
                    }
                       ,
                       new AnswerModel
                    {
                        Text = "sơn"
                    },
                       new AnswerModel
                    {
                        Text = "dị"
                    }
                }
                });
            }
            Topicsx = new MvxObservableCollection<TopicModel>();
            for (int i = 0; i < 6; i++)
            {
                Topicsx.Add(new TopicModel
                {
                    Toppic = "Topic " + random.Next(1, 1000),
                    Time = random.Next(1, 50),
                    Color = color,
                    Exercises = new MvxObservableCollection<ExerciseModel>
                    {
                        new ExerciseModel{
                            Type = ExerciseModel.ExerciseType.Discover,
                            IsDone = true,
                            Color = color,
                            Name = "Cai Gi Do AI Biet Duoc",
                            Units = listUnit
                        },
                        new ExerciseModel{
                            Type = ExerciseModel.ExerciseType.Vocabulary,
                            Color = color,
                            Name = "Cai Gi Do AI Biet Duoc",
                            Units = listUnit
                        },
                        new ExerciseModel{
                            Type = ExerciseModel.ExerciseType.Memorise,
                            Color = color,
                             Name = "Cai Gi Do AI Biet Duoc",
                            Units = listUnit
                        },
                        new ExerciseModel{
                            Type = ExerciseModel.ExerciseType.Practice,
                            IsDone = true,
                            Color = color,
                            Name = "Cai Gi Do AI Biet Duoc",
                            Units = listUnit
                        },
                    }
                });
            }
            return Topicsx;
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

        private IMvxCommand _ExerciseClickCommand;

        public IMvxCommand ExerciseClickCommand
        {
            get { return _ExerciseClickCommand = _ExerciseClickCommand ?? new MvxCommand<ExerciseClickEventArg>(RunExerciseClickCommand); }

        }

        void RunExerciseClickCommand(ExerciseClickEventArg e)
        {
            ShowViewModel<MemoriseViewModel>(e.Exercise);
        }


    }
}
