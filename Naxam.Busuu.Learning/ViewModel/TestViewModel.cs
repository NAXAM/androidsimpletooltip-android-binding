using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.ViewModel
{
    public class TestViewModel : MvxViewModel
    {
        private IMvxCommand _NextCommand;

        public IMvxCommand NextCommand
        {
            get { return _NextCommand = _NextCommand ?? new MvxCommand(RunNextCommand); }

        }

        void RunNextCommand()
        {
            ShowViewModel<TestFillSentenceViewModel>();
        }
    }
}
