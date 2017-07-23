using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Start.Model;
using Naxam.Busuu.ViewModels;

namespace Naxam.Busuu.Start.ViewModel
{
    public class RegisterViewModel : MvxViewModel
    {

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _policy;

        public string Policy
        {
            get { return _policy; }
            set
            {
                if (_policy != value)
                {
                    _policy = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _phoneCode;

        public string PhoneCode
        {
            get { return _phoneCode; }
            set
            {
                if (_phoneCode != value)
                {
                    _phoneCode = value;
                    RaisePropertyChanged();
                }
            }
        }



        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged();
                }
            }
        }

		public IMvxCommand LoginCommend
		{
			get { return new MvxCommand(() => ShowViewModel<MainTabBarViewModel>()); }
		}
              
        public IMvxCommand PhoneCodeCommand
        {
            get { return new MvxCommand(() => ShowViewModel<ChooseCountryViewModel>()); }
        }

        public void Init(CountryModel country)
        {
            PhoneCode = country.PhoneCode;
            if (string.IsNullOrEmpty(PhoneCode))
                PhoneCode = "+1";
            
        }
    }
}
