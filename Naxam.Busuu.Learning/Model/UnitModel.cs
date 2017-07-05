﻿using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Model
{
    public class UnitModel : MvxNotifyPropertyChanged
    {
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

        private IList<string> _audios;

        public IList<string> Audios
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
