﻿using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Core.ViewModel;

namespace Naxam.Busuu.Core.ViewModels
{
    public class PremiumViewModel : MvxViewModel
    {
		public List<string> _adText;
		public List<string> AdText
		{
			get { return _adText; }
			set
			{
				if (_adText != value)
				{
					_adText = value;
					RaisePropertyChanged();
				}
			}
		}

        private List<PremiumFeatureModel> _features;
        public List<PremiumFeatureModel> Features
		{
			get { return _features; }
			set
			{
				if (_features != value)
				{
					_features = value;
					RaisePropertyChanged();
				}
			}
		}

		private int _discount;

		public int Discount
		{
			get { return _discount; }
			set
			{
				if (_discount != value)
				{
					_discount = value;
					RaisePropertyChanged();
				}
			}
		}

        private IMvxCommand _SeePlansCommand;

        public IMvxCommand SeePlansCommand
        {
            get { return _SeePlansCommand = _SeePlansCommand ?? new MvxCommand(RunSeePlansCommand); }

        }

        void RunSeePlansCommand()
        {
            ShowViewModel<BuyPremiumViewModel>();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            var random = new Random();
            var ad = new[] {"22 hours of Busuu Premium = 1 college semester of language study",
                "Make the most of busuu and unlocl all featers now",
                "Limited time only", 
                "You're flying through our lessions!", 
                "So here's a"+Discount+" discount on our annual plan"};
            var features = new[] {"Test your knowledge with fun Quizzes",
                "Develop your language with Grammar units",
                "Practice what you've learnt and get corrected by native speaker"};
            Discount = random.Next(0, 100);
            AdText = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                AdText.Add(ad[random.Next(0, 4)]);
                AdText.Add(features[random.Next(0,3)]);
            }
        }
    }
}
