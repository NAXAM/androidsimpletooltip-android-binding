using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.Model
{
    public class UserModel : MvxNotifyPropertyChanged
    {
        private string _avatarImage;

        public string avatarImage
        {
            get { return _avatarImage; }
            set
            {
                if (_avatarImage != value)
                {
                    _avatarImage = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _username;

        public string username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _password;

        public string password
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

        private string _selfDescription;

        public string selfDescription
        {
            get { return _selfDescription; }
            set
            {
                if (_selfDescription != value)
                {
                    _selfDescription = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _fullName;

        public string fullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _email;

        public string email
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

        private string _phoneNumber;

        public string phoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    RaisePropertyChanged();
                }
            }
        }


        private CountryModel _country;

        public CountryModel country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _gender;

        public int gender
        {
            get { return _gender; }
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    RaisePropertyChanged();
                }
            }
        }

        private List<LanguageModel> _speakLanguages;

        public List<LanguageModel> speakLanguages
        {
            get { return _speakLanguages; }
            set
            {
                if (_speakLanguages != value)
                {
                    _speakLanguages = value;
                    RaisePropertyChanged();
                }
            }
        }

        private LanguageModel _interfaceLanguage;

        public LanguageModel interfaceLanguage
        {
            get { return _interfaceLanguage; }
            set
            {
                if (_interfaceLanguage != value)
                {
                    _interfaceLanguage = value;
                    RaisePropertyChanged();
                }
            }
        }


        private List<VoucherModel> _voucher;

        public List<VoucherModel> voucher
        {
            get { return _voucher; }
            set
            {
                if (_voucher != value)
                {
                    _voucher = value;
                    RaisePropertyChanged();
                }
            }
        }

    }
}
