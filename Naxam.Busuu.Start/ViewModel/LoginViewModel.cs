﻿using MvvmCross.Core.ViewModels;
using System.Text.RegularExpressions;
using Naxam.Busuu.ViewModels;

namespace Naxam.Busuu.Start.ViewModel
{
    public class LoginViewModel : MvxViewModel
    {
        private bool _IsEnableLoginBtn;

        public bool IsEnableLoginBtn
        {
            get { return _IsEnableLoginBtn; }
            set
            {
                if (_IsEnableLoginBtn != value)
                {
                    _IsEnableLoginBtn = value;
                    RaisePropertyChanged(()=> IsEnableLoginBtn);
                }
            }
        }


        private string _TextPass;

        public string TextPass
        {
            get { return _TextPass; }
            set
            {
                if (_TextPass != value)
                {
                    _TextPass = value;
                    IsEnableLoginBtn = CheckPhoneNumber(TextEmail, TextPass);
                    RaisePropertyChanged();
                }
            }
        }

        private string _TextEmail;

        public string TextEmail
        {
            get { return _TextEmail; }
            set
            {
                if (_TextEmail != value)
                {
                    _TextEmail = value;
                    IsEnableLoginBtn = CheckPhoneNumber(TextEmail, TextPass);
                    RaisePropertyChanged();
                }
            }
        }
        private IMvxCommand _ForgotPasswordCommand;

        public IMvxCommand ForgotPasswordCommand
        {
            get { return _ForgotPasswordCommand = _ForgotPasswordCommand ?? new MvxCommand(RunForgotPasswordCommand); }

        }

        void RunForgotPasswordCommand()
        {
            ShowViewModel<ForgotPasswordViewModel>();
        }

        public IMvxCommand LoginCommend
        {
            get { return new MvxCommand(() => ShowViewModel<MainTabBarViewModel>()); }
        }
            
        // Validating information here 

        private bool CheckPhoneNumber(string email, string pass)
        {
            Regex regex = new Regex("^[a-zA-Z0-9-_\\.]+@[a-z0-9]+\\.[a-z]{2,4}$");
            bool checkMail = regex.IsMatch(email);

            Regex regexP = new Regex("^+?[0-9]{9,13}$");
            bool checkPhone = regexP.IsMatch(email);
            return (checkMail || checkPhone) && pass.Length >= 6;
        }


    }
}
