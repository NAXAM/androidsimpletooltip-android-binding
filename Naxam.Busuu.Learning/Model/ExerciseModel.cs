using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Model
{


    public class ExerciseModel : MvxNotifyPropertyChanged
    {
        //public enum ExerciseType
        //{
        //    HearConversation,
        //    FillConversation,
        //    FillSentence,
        //    OrderWord,
        //    Write,
        //    Synonymous
        //}

        public enum ExerciseType
        {
            Vocabulary,
            Memorise,
            Discover,
            Evolution,
            Practice,
            Dialogue,
            Conversation
        }

        private ExerciseType _type;

        public ExerciseType Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _color;

        public string Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    RaisePropertyChanged();
                }
            }
        }



        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _isDone;

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (_isDone != value)
                {
                    _isDone = value;
                    RaisePropertyChanged();
                }
            }
        }


        private IList<UnitModel> _units;

        public IList<UnitModel> Units
        {
            get { return _units; }
            set
            {
                if (_units != value)
                {
                    _units = value;
                    RaisePropertyChanged();
                }
            }
        }


    }
}
