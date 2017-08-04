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
    public class MemoriseViewModel : MvxViewModel
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

        public MemoriseViewModel(ILearningService learningService)
        {
            this.learningService = learningService;
        }
        public async void Init(ExerciseModel ex)
        {
            Exercise = ex;
            ex.Units = await learningService.GetUnitByExercise(ex);
        }

    }
}
