using System;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Core.ViewModels
{
    public class GoMainTabViewModel : MvxViewModel
    {
		public IMvxCommand btnDoneCommand
		{
            get { return new MvxCommand(() => ShowViewModel(MainTabViewModel)); }
		}
    }
}
