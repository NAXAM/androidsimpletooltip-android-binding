using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Naxam.Busuu.Review.Models;

namespace Naxam.Busuu.Review.Services
{
    public interface IReviewService
    {
        Task<List<ReviewModel>> GetAllReview();
    }
}
