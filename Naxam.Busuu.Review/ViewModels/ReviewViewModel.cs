using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.ViewModels;
using Naxam.Busuu.Review.Models;
using Naxam.Busuu.Review.Services;

namespace Naxam.Busuu.Review.ViewModels
{
    public class ReviewViewModel : MvxViewModel
    {
        readonly IReviewService _reviewService;

		public ReviewViewModel(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		public async override void Start()
		{
			Reviews =  await _reviewService.GetAllReview();
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

        IMvxCommand _favoriteCommand;
		public IMvxCommand FavoriteCommand
		{
			get
			{
                return (_favoriteCommand = _favoriteCommand ?? new MvxCommand<ReviewModel>(ExecuteFavoriteCommand));
			}
		}

        private void ExecuteFavoriteCommand(ReviewModel item)
        {
            item.IsFavorite = !item.IsFavorite;
        }

		IMvxCommand _playSoundCommand;
		public IMvxCommand PlaySoundCommand
		{
			get
			{
				return (_playSoundCommand = _playSoundCommand ?? new MvxCommand<ReviewModel>(ExecutePlayCommand));
			}
		}

        void ExecutePlayCommand(ReviewModel review)
        {
            
        }

        IMvxCommand _goPremiumCommand;
        public IMvxCommand GoPremiumCommand
        {
			get
			{
				return (_goPremiumCommand = _goPremiumCommand ?? new MvxCommand(ExecuteGoPremiumCommand));
			}
        }

        private void ExecuteGoPremiumCommand()
        {
            ShowViewModel<PremiumViewModel>();
        }
    }
}
