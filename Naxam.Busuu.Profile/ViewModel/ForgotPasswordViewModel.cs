﻿using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Profile.Model;
using Naxam.Busuu.Profile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModel
{
    public class ForgotPasswordViewModel : MvxViewModel
    {
        private string _emailPhone;

        public string EmailPhone
        {
            get { return _emailPhone; }
            set
            {
                if (_emailPhone != value)
                {
                    _emailPhone = value;
                    RaisePropertyChanged(() => EmailPhone);
                }
            }
        }
        IDialogService dailogService;
        public ForgotPasswordViewModel(IDialogService dailogService)
        {
            this.dailogService = dailogService;
           
        }

        public void Init(LanguageModel firstNavObject)
        {
            // use firstNavObject
        }

        private IMvxCommand _forgotPasswordCommand;

        public IMvxCommand ForgotPasswordCommand
        {
            get { return _forgotPasswordCommand = _forgotPasswordCommand ?? new MvxCommand(RunForgotPasswordCommand); }

        }

        void RunForgotPasswordCommand()
        {
            ShowViewModel<RegisterViewModel>(EmailPhone);
        }



    }
}
