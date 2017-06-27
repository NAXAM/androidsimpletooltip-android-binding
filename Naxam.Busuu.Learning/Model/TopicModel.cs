using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Model
{
    public class TopicModel : MvxNotifyPropertyChanged
    {
        private string _toppic;

        public string Toppic
        {
            get { return _toppic; }
            set
            {
                if (_toppic != value)
                {
                    _toppic = value;
                    RaisePropertyChanged();
                }
            }
        }
        //minutes
        private int _time;

        public int Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<ExerciseModel> _exercises;

        public IList<ExerciseModel> Exercises
        {
            get { return _exercises; }
            set
            {
                if (_exercises != value)
                {
                    _exercises = value;
                    RaisePropertyChanged();
                }
            }
        }


    }
}
