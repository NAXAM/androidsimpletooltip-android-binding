    using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Review.Models
{
    public class ReviewAllModel:MvxNotifyPropertyChanged
    {
        public string ImgWord { get; set; }

        public int StrengthLevel { get; set; }

		public string Title{get;set;}

        public string SubTitle { get; set; }

        public bool IsFavorite { get; set; }

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
