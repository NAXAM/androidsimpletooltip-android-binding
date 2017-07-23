using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModel
{
    public class DialogueViewModel : MvxViewModel
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
            Exercise = ex ?? new ExerciseModel();
            Random random = new Random();
            List<UnitModel> listTest = new List<UnitModel>();
            for (int i = 0; i < 4; i++)
            {
                listTest.Add(new Busuu.Learning.Model.UnitModel
                {
                    Title = i % 2 == 0 ? "Chipu" : "So Ji Sub",

                    Input = new List<string>
                {
                    "Câu Số %% Hàng Số %%  Sai Hay Đúng"
                },
                    Images = new List<string> {
                    i%2==0?"http://eva-img.24hstatic.com/upload/2-2017/images/2017-04-20/1492655255-chi-pu.jpg":"http://newsen.vn/data/news/2015/3/11/17/So-Ji-Sub-bat-ngo-tai-xuat-man-anh-rong-Newsen-vn-0-1426043851.jpg",
                },
                    Audios = new List<AudioModel> {
                    new AudioModel
                    {
                        Link = "http://funnyneel.com/image/files/i/01-2014/beautiful-trees-v.jpg",
                        Start = i*10,
                        End = (i+1)*10-1
                    }
                },
                    Answers = new List<AnswerModel>
                {
                    new AnswerModel
                    {
                        Text = " Một 1 " + random.Next(1,1000)+" ",
                        Value = true
                    },
                     new AnswerModel
                    {
                        Text = " Hai 2 " + random.Next(1,1000)+" ",
                        Value  = true,
                        Position = 1
                    }

                }
                });
            }
             
            Exercise.Units = listTest;
        }

    }
}
