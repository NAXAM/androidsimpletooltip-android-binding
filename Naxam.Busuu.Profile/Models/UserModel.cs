using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.Models
{
    public enum Gender
    {
        Male,
        Female,
        Undisclosed
    }
    public class UserModel : MvxNotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _photo;

        public string Photo
        {
            get { return _photo; }
            set
            {
                if (_photo != value)
                {
                    _photo = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _aboutMe;

        public string AboutMe
        {
            get { return _aboutMe; }
            set
            {
                if (_aboutMe != value)
                {
                    _aboutMe = value;
                    RaisePropertyChanged();
                }
            }
        }

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

        private Gender _Gender;

        public Gender Gender
        {
            get { return _Gender; }
            set
            {
                if (_Gender != value)
                {
                    _Gender = value;
                    RaisePropertyChanged();
                }
            }
        }

        private CountryModel _country;

        public CountryModel Country
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

        private IList<LanguageModel> _language;

        public IList<LanguageModel> Language
        {
            get { return _language; }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<object> _MyExercises;

        public IList<object> MyExercises
        {
            get { return _MyExercises; }
            set
            {
                if (_MyExercises != value)
                {
                    _MyExercises = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<object> _MyCorrections;

        public IList<object> MyCorrections
        {
            get { return _MyCorrections; }
            set
            {
                if (_MyCorrections != value)
                {
                    _MyCorrections = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<UserModel> _friends;

        public IList<UserModel> Friends
        {
            get { return _friends; }
            set
            {
                if (_friends != value)
                {
                    _friends = value;
                    RaisePropertyChanged();
                }
            }
        }




    }
}
