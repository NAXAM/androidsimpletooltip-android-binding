using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Model
{
    public class VocabularyQuestionModel : MvxNotifyPropertyChanged
    {
        private string _imageDescriptionName;

        public string imageDescriptionName
        {
            get { return _imageDescriptionName; }
            set
            {
                if (_imageDescriptionName != value)
                {
                    _imageDescriptionName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _questionSentence;

        public string questionSentence
        {
            get { return _questionSentence; }
            set
            {
                if (_questionSentence != value)
                {
                    _questionSentence = value;
                    RaisePropertyChanged();
                }
            }
        }

        private List<string> _listAnswer;

        public List<string> listAnswer
        {
            get { return _listAnswer; }
            set
            {
                if (_listAnswer != value)
                {
                    _listAnswer = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _trueAnswer;

        public int trueAnswer
        {
            get { return _trueAnswer; }
            set
            {
                if (_trueAnswer != value)
                {
                    _trueAnswer = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
