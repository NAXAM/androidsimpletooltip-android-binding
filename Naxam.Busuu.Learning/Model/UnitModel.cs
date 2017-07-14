using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Model
{
    public class UnitModel : MvxNotifyPropertyChanged
    {
        public enum UnitType
        {
            Unknow,
            SelectWord,
            FillSentence
        }

        private UnitType _Type;

        public UnitType Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    RaisePropertyChanged();
                }
            }
        }



        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<string> _images;

        public IList<string> Images
        {
            get { return _images; }
            set
            {
                if (_images != value)
                {
                    _images = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<AudioModel> _audios;

        public IList<AudioModel> Audios
        {
            get { return _audios; }
            set
            {
                if (_audios != value)
                {
                    _audios = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<string> _input;
        public IList<string> Input
        {
            get { return _input; }
            set
            {
                if (_input != value)
                {
                    _input = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<AnswerModel> _answers;

        public IList<AnswerModel> Answers
        {
            get { return _answers; }
            set
            {
                if (_answers != value)
                {
                    _answers = value;
                    RaisePropertyChanged();
                }
            }
        }

    }
}
