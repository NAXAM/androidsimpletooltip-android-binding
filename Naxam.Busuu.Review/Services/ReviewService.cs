using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Naxam.Busuu.Review.Models;

namespace Naxam.Busuu.Review.Services
{
    public class ReviewService : IReviewService
    {
        string[] words = new[] { "hello", "I'm", "What's your name?", "Where are you from?" };

        public Task<List<ReviewModel>> GetAllReview()
        {
			var random = new Random();
			var items = new List<ReviewModel>();
			for (int i = 0; i < 1000; i++)
			{
				items.Add(new ReviewModel
				{
					Title = words[random.Next(0, words.Length - 1)],
					SubTitle = words[random.Next(0, words.Length - 1)],
					StrengthLevel = random.Next(0, 4),
					IsFavorite = i % 2 == 0 ? true : false,
					ImgWord = string.Format("http://placekitten.com/{0}/{0}", random.Next(20) + 300)
				});
			}
            return Task.FromResult(items);
        }
    }
}
