using System;
using System.Collections.Generic;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Review.Models;

namespace Naxam.Busuu.Review.ViewModels
{
    public class ReviewAllViewModel : MvxViewModel
    {
        private List<ReviewAllModel> _reviews;

        public List<ReviewAllModel> Reviews
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

		private List<ReviewAllModel> _favoriteReviews;

		public List<ReviewAllModel> FavoriteReviews
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

        private ICommand _markFavoriteCommand;

        public ICommand MarkFavoriteCommand
        {
            get
            {
				_markFavoriteCommand = _markFavoriteCommand ?? new MvxCommand<ReviewAllModel>(MarkFavorite);
				return _markFavoriteCommand;
            }
        }

        private void MarkFavorite(ReviewAllModel review)
        {
            if(review!=null)
            {
                review.IsFavorite = !review.IsFavorite;
            }
        }

        //mock data
        public ReviewAllViewModel()
        {
            Reviews = new List<ReviewAllModel>(ReviewAllModel.MockData);
            //foreach (var item in Reviews)
            //{
            //    if(item.IsFavorite == true)
            //    {
            //        FavoriteReviews.Add(item);
            //    }
            //}
        }
    }
}
