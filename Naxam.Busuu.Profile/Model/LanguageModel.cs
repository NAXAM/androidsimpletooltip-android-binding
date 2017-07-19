using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.Model
{
    public class LanguageModel: MvxNotifyPropertyChanged
    {
        private string _flag;

        public string Flag
        {
            get { return _flag; }
            set
            {
                if (_flag != value)
                {
                    _flag = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _language;

        public string Language
        {
            get { return _language; }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    RaisePropertyChanged();
                }
            }
        }


        private int _percent;

        public int Percent
        {
            get { return _percent; }
            set
            {
                if (_percent != value)
                {
                    _percent = value;
                    RaisePropertyChanged();
                }
            }
        }


        private bool _isCurrent;

        public bool IsCurrent
        {
            get { return _isCurrent; }
            set
            {
                if (_isCurrent != value)
                {
                    _isCurrent = value;
                    RaisePropertyChanged();
                }
            }
        }


    }
}
