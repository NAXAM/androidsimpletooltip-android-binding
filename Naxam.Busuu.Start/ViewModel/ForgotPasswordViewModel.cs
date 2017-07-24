using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Start.Model;
using Naxam.Busuu.Start.Service;

namespace Naxam.Busuu.Start.ViewModel
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
