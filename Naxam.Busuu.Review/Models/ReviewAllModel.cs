    using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Review.Models
{
    public class ReviewAllModel:MvxNotifyPropertyChanged
    {
        string _imgWord;
		public string ImgWord{ get => _imgWord; set=>_imgWord = value;}
        string _imgStrength;
        public string ImgStrength { get => _imgStrength; set => _imgStrength = value; }
		
		public string Title{get;set;}

        public string SubTitle { get; set; }

        public bool IsFavorite { get; set; }

		static string[] words = new[] { "hello", "I'm", "What's your name?", "Where are you from?" };

        static string[] images = new[] { "a","b","c","d","e","f", "g", "h", "i", "j","k", "l", "m"};

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
							IsFavorite = i % 2 == 0 ? true : false,
							ImgWord = string.Format("http://placekitten.com/{0}/{0}", random.Next(20) + 300),
							ImgStrength = string.Format("http://placekitten.com/{0}/{0}", random.Next(20) + 300),
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
