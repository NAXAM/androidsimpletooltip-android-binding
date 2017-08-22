using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Social.Models
{
    public class GiveFeedbackAnswerModel: MvxNotifyPropertyChanged
    {

        private string _urlImage;

        public string UrlImage
        {
            get { return _urlImage; }
            set
            {
                if (_urlImage != value)
                {
                    _urlImage = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _question;

        public string Question
        {
            get { return _question; }
            set
            {
                if (_question != value)
                {
                    _question = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _answer;

        public string Answer
        {
            get { return _answer; }
            set
            {
                if (_answer != value)
                {
                    _answer = value;
                    RaisePropertyChanged();

                }
            }
        }

        private float _rate;

        public float Rate
        {
            get { return _rate; }
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _comment;

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    RaisePropertyChanged();
                }
            }
        }


    }
}
