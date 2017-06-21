using System;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Review.ViewModels
{
    public class ReviewAllViewModel : MvxViewModel
    {
        public ReviewAllViewModel()
        {
        }

		public override void Start()
		{
            _subTotal = 100;
			base.Start();
		}

		double _subTotal;

		public double SubTotal
		{
			get
			{
				return _subTotal;
			}
			set
			{
				_subTotal = value;
				RaisePropertyChanged(() => SubTotal);
			}
		}
    }
}
