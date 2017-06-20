using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Profile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModel
{
    public class ChooseLanguageViewModel : MvxViewModel
    {
        private MvxObservableCollection<LanguageModel> _languages;

        public MvxObservableCollection<LanguageModel> Languages
        {
            get { return _languages; }
            set
            {
                if (_languages != value)
                {
                    _languages = value;
                    RaisePropertyChanged(() => Languages);
                }
            }
        }

        private LanguageModel _selectedLanguage;

        public LanguageModel SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    if (_selectedLanguage != null)
                    {
                        ShowViewModel<ForgotPasswordViewModel>(_selectedLanguage);
                    }
                    RaisePropertyChanged();
                }
            }
        }


        public ChooseLanguageViewModel()
        {
            string[] flags = new string[] {
                "flag_small_arabic.png",
                 "flag_small_chinese.png",
                  "flag_small_english.png",
                   "flag_small_french.png",
                    "flag_small_german.png",
                     "flag_small_indonesia.png",
                      "flag_small_italian.png",
                       "flag_small_japanese.png",
                        "flag_small_polish.png",
                         "flag_small_portuguese.png",
                          "flag_small_russian.png",
                           "flag_small_spanish.png",
                            "flag_small_turkish.png"
            };
            string[] langs = new string[] {
                "Arabic",
                 "Chinese",
                  "English",
                   "French",
                    "German",
                     "Indonesia",
                      "Italian",
                       "Japanese",
                        "Polish",
                         "Portuguese",
                          "Russian",
                           "Spanish",
                            "Turkish"
            };
            Languages = new MvxObservableCollection<LanguageModel>();
            for (int i = 0; i < 13; i++)
            {
                Languages.Add(new LanguageModel
                {
                    Flag = flags[i],
                    Language = langs[i]
                });
            }
           
            
        }

    }
}
