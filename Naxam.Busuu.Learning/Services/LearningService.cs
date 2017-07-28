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
        UnitModel.UnitType[] typeUnit = new UnitModel.UnitType[] {
                UnitModel.UnitType.ChooseWord,
                UnitModel.UnitType.FillSentence,
                UnitModel.UnitType.MatchingSentence,
                UnitModel.UnitType.SelectWord,
                UnitModel.UnitType.SelectWordImage,
                UnitModel.UnitType.CompleteSentence
            };
        ExerciseModel.ExerciseType[] typeExercise = new ExerciseModel.ExerciseType[] {
             //   ExerciseModel.ExerciseType.Conversation,
                ExerciseModel.ExerciseType.Dialogue,
            //    ExerciseModel.ExerciseType.Discover,
           //     ExerciseModel.ExerciseType.Evolution,
                ExerciseModel.ExerciseType.Memorise,
             //   ExerciseModel.ExerciseType.Practice,
                ExerciseModel.ExerciseType.Vocabulary
            };
        Random random = new Random();
        public async Task<LessonModel[]> GetAllLesson()
        {

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
            ExerciseModel ex = new ExerciseModel();


            ex.Type = typeExercise[random.Next(1, 100) % 3];
            ex.Name = "Exercise " + random.Next(1, 100);
            return ex;
        }


        private UnitModel GetDataByUnitType(UnitModel.UnitType type, bool hasImage, bool hasAudio)
        {
            UnitModel unit = new UnitModel();
            switch (type)
            {
                case UnitModel.UnitType.FillSentence:
                    unit.Input = new List<string> {
                        "What's your name? %%  " };
                    unit.Title = "Select the correct word(s) to fill the gap.";
                    unit.Images = new List<string>() { };
                    unit.Answers = new List<AnswerModel> {
                        new AnswerModel
                        {
                            Text="I'm from...",
                            Value = false
                        },
                        new AnswerModel
                        {
                            Text="I'm...",
                            Value = true
                        }
                        ,
                        new AnswerModel
                        {
                            Text="she is from...",
                            Value = false
                        },
                    };
                    unit.Type = UnitModel.UnitType.FillSentence;
                    break;
                case UnitModel.UnitType.ChooseWord:
                    unit.Title = "Bấm vào từ đối nghĩa của từ\"Tall\" ";
                    unit.Answers = new List<AnswerModel> {
                        new AnswerModel
                        {
                            Text="Old",
                            Value = false
                        },
                        new AnswerModel
                        {
                            Text="Short",
                            Value = true
                        } ,
                        new AnswerModel
                        {
                            Text="Tall",
                            Value = false
                        },
                    };

                    unit.Type = UnitModel.UnitType.ChooseWord;
                    break;
                case UnitModel.UnitType.MatchingSentence:
                    unit.Title = "Nối cặp/cụm từ";
                    unit.Answers = new List<AnswerModel> {
                        new AnswerModel
                        {
                            Text="Sinh Viên",
                            Value = true
                        },
                        new AnswerModel
                        {
                            Text="Bác Sĩ",
                            Value = true
                        } ,
                        new AnswerModel
                        {
                            Text="Giáo Viên",
                            Value = true
                        },
                    };
                    unit.Input = new List<string> {
                        "Student","Doctor","Teacher" };
                    unit.Type = UnitModel.UnitType.MatchingSentence;
                    break;
                case UnitModel.UnitType.SelectWord:
                    unit.Title = "Chọn các từ có nghia là \"Phụ Nữ\" ";
                    unit.Answers = new List<AnswerModel> {
                        new AnswerModel
                        {
                            Text="Man",
                            Value = false
                        },
                        new AnswerModel
                        {
                            Text="Woman",
                            Value = true
                        }
                        ,
                         new AnswerModel
                        {
                            Text="Girl",
                            Value = true
                        }
                        ,
                        new AnswerModel
                        {
                            Text="Boy",
                            Value = false
                        },
                        new AnswerModel
                        {
                            Text="Friend",
                            Value = false
                        }
                    };
                    unit.Type = UnitModel.UnitType.SelectWord;
                    break;
                case UnitModel.UnitType.SelectWordImage:
                    unit.Title = "Con trai";
                    unit.Answers = new List<AnswerModel> {
                        new AnswerModel
                        {
                            Text="Brother",
                            Value = false,
                            Image="https://cdn.busuu.com/v1.0/jpgmedium1/media/img/1_1_7_N_I__enc__10_1450281721.jpg"
                        },
                        new AnswerModel
                        {
                            Text="Mother",
                            Value = false,
                            Image="https://cdn.busuu.com/v1.0/jpgmedium1/media/img/1_1_7_N_I__enc__7_1450281192.jpg"
                        }
                        ,
                         new AnswerModel
                        {
                            Text="Son",
                            Value = true,
                            Image="https://cdn.busuu.com/v1.0/jpgmedium1/media/img/father_1455729260.jpg"
                        }
                    };
                    unit.Type = UnitModel.UnitType.SelectWordImage;

                    break;
                case UnitModel.UnitType.CompleteSentence:
                    unit.Title = "Điền vào chỗ trống";
                    unit.Input = new List<string> { "%% Quỳnh Anh"};
                    unit.Answers = new List<AnswerModel> {
                         new AnswerModel
                        {
                            Text="i'm",
                        }
                    };
                    unit.Type = UnitModel.UnitType.CompleteSentence;
                    break;
                case UnitModel.UnitType.HearAndRepeat:
                    unit.Title = "Điền vào chỗ trống";
                    unit.Input = new List<string> { "%% Quỳnh Anh" };
                    unit.Answers = new List<AnswerModel> {
                         new AnswerModel
                        {
                            Text="i'm",
                        }
                    };
                    unit.Type = UnitModel.UnitType.HearAndRepeat;
                    break;
            }
            if (random.Next(1, 100) % 3 == 0)
            {
                unit.Tip = new TipModel
                {
                    Tip = "Tuyển tập cua gái đại pháp",
                    Detail = "1. thất bại hãy cua em khác,<\br>2. muốn thành công hãy đối mặt với thật bại",
                    Samples = new List<string> { "Phương án 1", "Phương án 2" }
                };
            }
            unit.Audios = new List<AudioModel>()
            {
                new AudioModel
                {
                    Link=""
                }
            };
            if (hasAudio) { }

            if (hasImage)
            {
                unit.Images = new List<string>
                {
                    //   "http://newsen.vn/data/news/2015/3/11/17/So-Ji-Sub-bat-ngo-tai-xuat-man-anh-rong-Newsen-vn-0-1426043851.jpg"
                };
            }
            return unit;
        }



        private List<TopicModel> GetTopic(string color)
        {
            List<ExerciseModel> lstExercise = new List<ExerciseModel>();
            for (int i = 0; i < 3; i++)
            {
                lstExercise.Add(GetRandomExercise());
            }
            List<TopicModel> Topicsx = new List<TopicModel>();
            for (int i = 0; i < 6; i++)
            {
                Topicsx.Add(new TopicModel
                {
                    Toppic = "Topic " + random.Next(1, 1000),
                    Time = random.Next(1, 50),
                    Color = color,
<<<<<<< HEAD
                    Exercises = lstExercise
=======
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
>>>>>>> ios-sonnt-learning
                });
            }
            return Topicsx;
        }

        public async Task<UnitModel[]> GetUnitByExercise(ExerciseModel ex)
        {

            List<UnitModel> lstUnit = new List<UnitModel>();
            if (ex.Type != ExerciseModel.ExerciseType.Dialogue)
                for (int i = 0; i < 6; i++)
                {
                    lstUnit.Add(GetDataByUnitType(typeUnit[random.Next(1, 100) % 6], random.Next(1, 100) % 2 == 0, random.Next(1, 100) % 2 == 0));
                }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    lstUnit.Add(GetDataByUnitType(UnitModel.UnitType.FillSentence, random.Next(1, 100) % 2 == 0, random.Next(1, 100) % 2 == 0));
                }
            }
            return lstUnit.ToArray();
        }
    }
}
