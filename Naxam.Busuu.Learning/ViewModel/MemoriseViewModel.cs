using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModel
{
    public class MemoriseViewModel : MvxViewModel
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
                UnitModel.UnitType.FillSentence,UnitModel.UnitType.SelectWord,UnitModel.UnitType.ChooseWord
            };
            Exercise = ex ?? new ExerciseModel();
            List<UnitModel> listUnit = new List<UnitModel>();
            for (int i = 0; i < 5; i++)
            {
                listUnit.Add(new UnitModel
                {
                    Title = "Chọn từ đúng",
                    Type = lstType[i % 3],

                    Input = new List<string>
                {
                    "Tôi là %% người %%  trai nhất naxam",
                        "Tôi là %% người %%  trai nhất naxam",
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
                        Value = (i+1)%3!=0
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
            Exercise.Units = listUnit;
        }

    }
}
