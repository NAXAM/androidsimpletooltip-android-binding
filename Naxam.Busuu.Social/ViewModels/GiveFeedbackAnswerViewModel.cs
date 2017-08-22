using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Social.ViewModels
{
    public class GiveFeedbackAnswerViewModel: MvxViewModel
    {
        private GiveFeedbackAnswerModel _Feedback;

        public GiveFeedbackAnswerModel Feedback
        {
            get { return _Feedback; }
            set
            {
                if (_Feedback != value)
                {
                    _Feedback = value;
                    RaisePropertyChanged();
                }
            }
        }
        // constructor here
        public GiveFeedbackAnswerViewModel()
        {
            createData();

        }
        private void createData()
        {
            Feedback = new GiveFeedbackAnswerModel
            {
                UrlImage="",
                Answer="",
                Question="",
                Comment="",
                Rate=3
            };

        }


    }
}
