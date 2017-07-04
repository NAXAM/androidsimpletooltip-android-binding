using System;
using System.Collections.Generic;
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

        //mock data
        public ReviewAllViewModel()
        {
            Reviews = new List<ReviewAllModel>(ReviewAllModel.MockData);
        }
    }
}
