using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class StartPageViewModel : MvxViewModel
    {
        private IMvxCommand _getStartedCommand;

        public IMvxCommand GetStartedCommand
        {
            get { return _getStartedCommand = _getStartedCommand ?? new MvxCommand(RunGetStartedCommand); }
        }

        void RunGetStartedCommand()
        {
            ShowViewModel<ChooseLanguageViewModel>();
        }


        private IMvxCommand _loginCommand;

        public IMvxCommand LoginCommand
        {
            get { return _loginCommand = _loginCommand ?? new MvxCommand(RunLoginCommand); }
        }

        void RunLoginCommand()
        {
            ShowViewModel<LoginViewModel>();
        }
    }
}
