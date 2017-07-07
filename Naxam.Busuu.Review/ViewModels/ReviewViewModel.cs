using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Review.Models;
using Naxam.Busuu.Review.Services;

namespace Naxam.Busuu.Review.ViewModels
{
    public class ReviewAllViewModel : MvxViewModel
    {
        readonly IReviewService _reviewService;

		public ReviewAllViewModel(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		public async override void Start()
		{
			Reviews =  await _reviewService.GetAllReview();
            FavoriteReviews = new List<ReviewModel>();
            Filterediews = new List<ReviewModel>();
            foreach (var item in Reviews)
            {
                if (item.IsFavorite)
                {
                    FavoriteReviews.Add(item);
                }
            }
            base.Start();
		}

        private List<ReviewModel> _reviews;

        public List<ReviewModel> Reviews
        {
			get { return _reviews; }
			set
			{
				if (_reviews != value)
				{
					_reviews = value;
					RaisePropertyChanged(() => Reviews);
				}
			}
        }

		private List<ReviewModel> _favoriteReviews;

		public List<ReviewModel> FavoriteReviews
		{
			get { return _favoriteReviews; }
			set
			{
				if (_favoriteReviews != value)
				{
					_favoriteReviews = value;
					RaisePropertyChanged(() => FavoriteReviews);
				}
			}
		}

        private List<ReviewModel> _filteredReviews;

        public List<ReviewModel> Filterediews
		{
			get { return _filteredReviews; }
			set
			{
				if (_filteredReviews != value)
				{
					_filteredReviews = value;
					RaisePropertyChanged(() => Filterediews);
				}
			}
		}

		private string _searchTerm;
		public string SearchTerm
		{
			get { return _searchTerm; }
			set
			{
				_searchTerm = value;
				Filterediews.Clear();
				Filterediews.AddRange(
					Reviews.Where(e => e.Title.ToUpper().Contains(_searchTerm.ToUpper()) || string.IsNullOrWhiteSpace(_searchTerm))
				);
				RaisePropertyChanged(() => SearchTerm);
				RaisePropertyChanged(() => Filterediews);
			}
		}
    }
}
