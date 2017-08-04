using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.Model;
using Naxam.Busuu.Learning.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModel
{
    public class DialogueViewModel : MvxViewModel
    {
        readonly ILearningService learningService;
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
        public DialogueViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }

        public async void Init(ExerciseModel ex)
        {
            Exercise = ex;
            List<UnitModel> lstUnit = new List<UnitModel>();
            lstUnit.Add(new UnitModel
            {
                Title = "Jack",
                Input = new List<string> { "hello, %% Jack" },
                Images = new List<string> { "http://newsen.vn/data/news/2015/3/11/17/So-Ji-Sub-bat-ngo-tai-xuat-man-anh-rong-Newsen-vn-0-1426043851.jpg" },
                Answers = new List<AnswerModel> { new AnswerModel {
                    Text = "I'm",
                    Value = true,
                } },
                Audios = new List<AudioModel> {
                    new AudioModel{ Link="" }
                }
            });
            lstUnit.Add(new UnitModel
            {
                Title = "Martha",
                Input = new List<string> { "%%, I'm Martha" },
                Images = new List<string> { "http://eva-img.24hstatic.com/upload/2-2017/images/2017-04-20/1492655255-chi-pu.jpg" },
                Answers = new List<AnswerModel> { new AnswerModel {
                    Text = "Hi",
                    Value = true,
                } },
                Audios = new List<AudioModel> {
                    new AudioModel{ Link="" }
                }
            });
            lstUnit.Add(new UnitModel
            {
                Title = "Jack",
                Input = new List<string> { "%%. %%" },
                Images = new List<string> { "http://newsen.vn/data/news/2015/3/11/17/So-Ji-Sub-bat-ngo-tai-xuat-man-anh-rong-Newsen-vn-0-1426043851.jpg" },
                Answers = new List<AnswerModel> { new AnswerModel {
                    Text = "Nice to meet you",
                    Value = true,
                },
                    new AnswerModel {
                    Text = "How's it going ?",
                    Value = true,
                    Position = 1
                }},
                Audios = new List<AudioModel> {
                    new AudioModel{ Link="" }
                }
            });
            lstUnit.Add(new UnitModel
            {
                Title = "Martha",
                Input = new List<string> { "%%" },
                Images = new List<string> { "http://eva-img.24hstatic.com/upload/2-2017/images/2017-04-20/1492655255-chi-pu.jpg" },
                Answers = new List<AnswerModel> { new AnswerModel {
                    Text = "Fine, thanks",
                    Value = true,
                } },
                Audios = new List<AudioModel> {
                    new AudioModel{ Link="" }
                }
            });
            Exercise.Units = lstUnit;
        }

    }
}
