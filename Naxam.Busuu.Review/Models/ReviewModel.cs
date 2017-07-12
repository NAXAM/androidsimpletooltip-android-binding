    using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Review.Models
{
    public class ReviewModel : MvxNotifyPropertyChanged
    {
        string _imgWord;
        public string ImgWord 
        {
			get { return _imgWord; }
			set
			{
				if (_imgWord != value)
				{
					_imgWord = value;
					RaisePropertyChanged();
				}
			}
        }

        int _strengthlevel;
		public int StrengthLevel
		{
			get { return _strengthlevel; }
			set
			{
				if (_strengthlevel != value)
				{
					_strengthlevel = value;
					RaisePropertyChanged();
				}
			}
		}

        string _title;
		public string Title
		{
			get { return _title; }
			set
			{
				if (_title != value)
				{
					_title = value;
					RaisePropertyChanged();
				}
			}
		}

        string _subTitle;
		public string SubTitle
		{
			get { return _subTitle; }
			set
			{
				if (_subTitle != value)
				{
					_subTitle = value;
					RaisePropertyChanged();
				}
			}
		}

        bool _isFavorite;
		public bool IsFavorite
		{
			get { return _isFavorite; }
			set
			{
				if (_isFavorite != value)
				{
					_isFavorite = value;
					RaisePropertyChanged();
				}
			}
		}

		//public MvxCommand FlipSelected
		//{
  //          get { return new MvxCommand(() => 
  //                                  { this.IsFavorite = !this.IsFavorite;
  //                                    RaisePropertyChanged(); 
  //                                  });
  //              }
		//}

    }
}
