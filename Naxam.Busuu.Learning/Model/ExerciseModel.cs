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
        public enum ExerciseType
        {
            HearConversation,
            FillConversation,
            FillSentence,
            OrderWord,
            Write,
            Synonymous
        }

        private bool _withAudio;

        public bool WithAudio
        {
            get { return _withAudio; }
            set
            {
                if (_withAudio != value)
                {
                    _withAudio = value;
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

    }
}
