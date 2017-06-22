using System;
using Naxam.Busuu.Review.Models;

namespace Naxam.Busuu.Review.Services
{
    public interface IReviewGenesisService
    {
        ReviewAllModel CreateNewReview(string extra = "");
    }
}
