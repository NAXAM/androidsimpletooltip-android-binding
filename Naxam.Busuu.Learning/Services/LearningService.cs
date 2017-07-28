using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naxam.Busuu.Learning.Model;

namespace Naxam.Busuu.Learning.Services
{
    public class LearningService : ILearningService
    {
        public async Task<LessonModel[]> GetAllLesson()
        {
            Random random = new Random();
            string[] color = new string[]
            {
                "#58B0F8","#B02018","#FEAB35"
            };


            string[] icons = new string[]
            {
                "http://app4e.net/busuu/lesson1.png",
                "http://app4e.net/busuu/lesson2.png",
                "http://app4e.net/busuu/lesson3.png",
            };

            List<LessonModel> Lessons = new List<LessonModel>();
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
            return Lessons.ToArray();
        }


        public async Task<TipModel> GetTipByUnit(UnitModel unit)
        {
            List<string> lstTip = new List<string>() { "mot hai ba", "ba bon ngay" };
            TipModel tip = new TipModel
            {
                Tip = "\"Hi\" is a slightly more casual way of saying \"hello\". However, we use both words and we can say both at any time of day.",
                Samples = lstTip,
                Detail = "\"Hi\" is a slightly more casual way of saying \"hello\". However, we use both words and we can say both at any time of day."
            };
            return tip;
        }



        public async Task<UnitModel[]> GetUnitByExercise(ExerciseModel ex)
        {
            UnitModel.UnitType[] lstType = new UnitModel.UnitType[] {
            UnitModel.UnitType.FillSentence,UnitModel.UnitType.ChooseWord,UnitModel.UnitType.SelectWord
            };


            ex = ex ?? new ExerciseModel();

            List<UnitModel> listUnit = new List<UnitModel>();
            for (int i = 0; i < 5; i++)
            {
                listUnit.Add(new UnitModel
                {
                    Title = "Chọn từ đúng",
                    Type = lstType[i % 3],

                    Tip = await GetTipByUnit(null),
                    Input = new List<string>
                {
                    "Tôi là %% người %%  trai nhất naxam"
                },
                    Images = new List<string>
                    {
                        // "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg",
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
                        Value = true,
                        Image = "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg"
                    },
                     new AnswerModel
                    {
                        Text = "xấu",
                        Value  = true,
                        Position = 1,
                        Image = "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg"
                    },
                      new AnswerModel
                    {
                        Text = "đẹp",
                        Image = "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg"
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
            ex.Units = listUnit;
            return listUnit.ToArray();
        }

        private List<TopicModel> GetTopic(string color)
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
            List<TopicModel> Topicsx = new List<TopicModel>();
            for (int i = 0; i < 6; i++)
            {
                Topicsx.Add(new TopicModel
                {
                    Toppic = "Topic " + random.Next(1, 1000),
                    Time = random.Next(1, 50),
                    Color = color,
                    Exercises = new List<ExerciseModel>
                    {
                        new ExerciseModel{
                            Type = ExerciseModel.ExerciseType.Discover,
                            IsDone = true,
                            Color = color,
                            Name = "Cai Gi Do AI Biet   ",
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
    }
}
