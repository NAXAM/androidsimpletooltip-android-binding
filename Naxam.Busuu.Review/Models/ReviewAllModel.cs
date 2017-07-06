    using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Review.Models
{
    public class ReviewAllModel:MvxNotifyPropertyChanged
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

        MvxCommand _flipSelected;
		public MvxCommand FlipSelected
		{
			get { return new MvxCommand(() => this.IsFavorite = !this.IsFavorite); }
		}

		static string[] words = new[] { "hello", "I'm", "What's your name?", "Where are you from?" };

        static string[] strengths = new[] { "entity_strength_0", "entity_strength_1", "entity_strength_2", "entity_strength_3" };

		private static List<ReviewAllModel> mockData;

		public static List<ReviewAllModel> MockData
		{
			get
			{
				if (mockData == null)
				{
					var random = new Random();
					var items = new List<ReviewAllModel>();
					for (int i = 0; i < 100; i++)
					{
                        items.Add(new ReviewAllModel
                        {
                            Title = words[random.Next(0, words.Length - 1)],
                            SubTitle = words[random.Next(0, words.Length - 1)],
                            StrengthLevel = random.Next(0, 4),
                            IsFavorite = i % 2 == 0 ? true : false,
                            ImgWord = string.Format("http://placekitten.com/{0}/{0}", random.Next(20) + 300)
						});
					}
					mockData = items;
				}
				return mockData;
			}
			set => mockData = value;
		}
    }
}
