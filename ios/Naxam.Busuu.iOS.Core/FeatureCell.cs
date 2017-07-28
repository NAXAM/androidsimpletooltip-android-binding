using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platform.Converters;
using System.Globalization;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.iOS.Core
{
    public partial class FeatureCell : MvxTableViewCell
    {
        private readonly MvxImageViewLoader imgFeatureLoader;
        public FeatureCell (IntPtr handle) : base (handle)
        {
            imgFeatureLoader = new MvxImageViewLoader(() => imgFeature);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
			this.DelayBind(() =>
			{
				var setBinding = this.CreateBindingSet<FeatureCell, PremiumFeatureModel>();
				setBinding.Bind(lbFeature).To(m => m.Feature);
				setBinding.Bind(imgFeatureLoader).To(m => m.Image).WithConversion(new ImageUriValueConverter(), null);
				setBinding.Apply();
			});
            imgFeature.TintColor = UIColor.FromRGB(249,155,42);
        }
    }

	public class ImageUriValueConverter : MvxValueConverter<string, string>
	{
		protected override string Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
		{
			return "res:" + value;
		}
	}
}