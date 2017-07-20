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
    public class VocabularyViewModel : MvxViewModel
    {
        private ExerciseModel _exercise;
        readonly ILearningService learningService;

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
        public VocabularyViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }
        public async void Init(ExerciseModel ex)
        {
            Exercise = ex;
            Exercise.Units = await learningService.GetUnitByExercise(ex);
        }
    }
}
