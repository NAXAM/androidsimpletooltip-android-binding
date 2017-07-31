using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class GeneralProfileViewModel : MvxViewModel
    {
        private IMvxCommand _SettingCommand;

        public IMvxCommand SettingCommand
        {
            get { return _SettingCommand = _SettingCommand ?? new MvxCommand(RunSettingCommand); }

        }

        void RunSettingCommand()
        {
            ShowViewModel<ProfileSettingViewModel>();
        }


    }
}
