using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModel
{
    public class MainViewModel : MvxViewModel
    {
        private IMvxCommand _GoPremiumCommand;

        public IMvxCommand GoPremiumCommand
        {
            get { return _GoPremiumCommand = _GoPremiumCommand ?? new MvxCommand(RunGoPremiumCommand); }

        }

        void RunGoPremiumCommand()
        {
            ShowViewModel<PremiumViewModel>();
        }

        private IMvxCommand _ChangeLanguageCommand;

        public IMvxCommand ChangeLanguageCommand
        {
            get { return _ChangeLanguageCommand = _ChangeLanguageCommand ?? new MvxCommand(RunChangeLanguageCommand); }

        }

        void RunChangeLanguageCommand()
        {
            ShowViewModel<ChangeLanguageViewModel>();
        }


    }
}
