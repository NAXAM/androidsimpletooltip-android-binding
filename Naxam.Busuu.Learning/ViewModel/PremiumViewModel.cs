using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModel
{
    public class PremiumViewModel : MvxViewModel
    {
        private IMvxCommand _SeePlansCommand;

        public IMvxCommand SeePlansCommand
        {
            get { return _SeePlansCommand = _SeePlansCommand ?? new MvxCommand(RunSeePlansCommand); }

        }

        void RunSeePlansCommand()
        {
            ShowViewModel<BuyPremiumViewModel>();
        }



    }
}
