using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Review.Models;

namespace Naxam.Busuu.Review.ViewModels
{
    public class ReviewAllViewModel : MvxViewModel
    {
        private MvxObservableCollection<ReviewAllModel> _reviews;
        public MvxObservableCollection<ReviewAllModel> Reviews
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

        //mock data
        public ReviewAllViewModel()
        {
            Reviews = new MvxObservableCollection<ReviewAllModel>(ReviewAllModel.MockData);
        }
    }
}
