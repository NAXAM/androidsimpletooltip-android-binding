using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Start.ViewModel
{
    public class StartPageViewModel : MvxViewModel
    {
        public IMvxCommand GetStartedCommand
        {
			get { return new MvxCommand(() => ShowViewModel<ChooseLanguageViewModel>()); }
        }

        public IMvxCommand LoginCommand
        {
            get { return new MvxCommand(() => ShowViewModel<LoginViewModel>()); }
        }
    }
}
