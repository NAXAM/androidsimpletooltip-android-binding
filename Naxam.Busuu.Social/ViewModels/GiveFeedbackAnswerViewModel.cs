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

        private IMvxCommand _BackCmd;

        public IMvxCommand BackCmd
        {
            get { return _BackCmd = _BackCmd ?? new MvxCommand(RunBackCmd); }

        }

        void RunBackCmd()
        {

        }





        private IMvxCommand _SendCmd;

        public IMvxCommand SendCmd
        {
            get { return _SendCmd = _SendCmd ?? new MvxCommand(RunsendCmd); }

        }

        void RunsendCmd()
        {
            // do stuff when clicking a send button
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
                UrlImage= "http://3.bp.blogspot.com/-tCecMKwoSts/VMmKTA1BtbI/AAAAAAAAWK8/AM-yDDTbrgs/s1600/anh-ngoc-trinh-dep-hoa-than-thanh-cong-chua-bong-bong-xinh-nhu-bup-be-13.jpg",
                Answer= "social practices such as kinship and marriage, expressive forms such as art, music, dance, ritual, and religion, and technologies such as tool usage, cooking, shelter, and clothing are said to be cultural universals",
                Question= "What are the thing that define a culture?",
                Comment="",
                Rating = 3
            };

        }


    }
}
