using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Core.Models
{
    public class PremiumFeatureModel:MvxNotifyPropertyChanged
    {

        private string _features;

        public string Features
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

		private string _image;

		public string Image
		{
			get { return _image; }
			set
			{
				if (_image != value)
				{
					_image = value;
					RaisePropertyChanged();
				}
			}
		}
    }
}
