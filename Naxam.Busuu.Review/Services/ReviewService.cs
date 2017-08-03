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
            for (int i = 0; i < 100; i++)
            {
                items.Add(new ReviewModel
                {
                    Title = (char)random.Next(65, 90) + words[random.Next(0, words.Length - 1)] + " " + i,
                    SubTitle = words[random.Next(0, words.Length - 1)],
                    StrengthLevel = random.Next(0, 4),
                    IsFavorite = random.Next(0, 9) % 2 == 0 ? true : false,
                    ImgWord = string.Format("http://placekitten.com/{0}/{0}", random.Next(20) + 300),
                    SoundUrl = "Nokia-tune-Nokia-tune.mp3",
                    Sample = new ReviewModel
                    {
                        Title = (char)random.Next(65, 90) + words[random.Next(0, words.Length - 1)] + " " + i,
                        SubTitle = words[random.Next(0, words.Length - 1)],
                    }
                });
            }
            return Task.FromResult(items);
        }
    }
}
