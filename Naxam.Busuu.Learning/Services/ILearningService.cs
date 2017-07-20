using Naxam.Busuu.Learning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Services
{
    public interface ILearningService
    {
        Task<LessonModel[]> GetAllLesson();
        Task<UnitModel[]> GetUnitByExercise(ExerciseModel ex);
        Task<TipModel> GetTipByUnit(UnitModel unit);
    }
}
