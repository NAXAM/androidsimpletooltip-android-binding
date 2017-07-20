using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModel
{
    public class VocabularyViewModel : MvxViewModel
    {
        private ExerciseModel _exercise;

        public ExerciseModel Exercise
        {
            get { return _exercise; }
            set
            {
                if (_exercise != value)
                {
                    _exercise = value;
                    RaisePropertyChanged();
                }
            }
        }

        public void Init(ExerciseModel ex)
        {
            UnitModel.UnitType[] lstType = new UnitModel.UnitType[] {
            UnitModel.UnitType.FillSentence,UnitModel.UnitType.ChooseWord,UnitModel.UnitType.SelectWord
            };
            List<string> lstTip = new List<string>() { "mot hai ba", "ba bon ngay" };

            Exercise = ex ?? new ExerciseModel();
            TipModel tip = new TipModel
            {
                Tip = "\"Hi\" is a slightly more casual way of saying \"hello\". However, we use both words and we can say both at any time of day.",
                Samples = lstTip,
                Detail = "\"Hi\" is a slightly more casual way of saying \"hello\". However, we use both words and we can say both at any time of day."
            };
            List<UnitModel> listUnit = new List<UnitModel>();
            for (int i = 0; i < 5; i++)
            {
                listUnit.Add(new UnitModel
                {
                    Title = "Chọn từ đúng",
                    Type = lstType[i % 3],

                    Tip = tip,
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
            Exercise.Units = listUnit;
        }
    }
}
