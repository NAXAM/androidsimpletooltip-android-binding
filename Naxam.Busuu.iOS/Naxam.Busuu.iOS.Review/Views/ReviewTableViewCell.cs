using System;
using System.Globalization;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Platform.Converters;
using Naxam.Busuu.Review.Models;
using UIKit;


namespace Naxam.Busuu.iOS.Review.Views
{
    public partial class ReviewTableViewCell : MvxTableViewCell
    {

		private readonly MvxImageViewLoader imgWordViewLoader;
        private readonly MvxImageViewLoader imgStrengthViewLoader;

        protected ReviewTableViewCell(IntPtr handle) : base(handle)
        {
			// Note: this .ctor should not contain any initialization logic.
            imgWordViewLoader = new MvxImageViewLoader(() => imgWord);
            imgStrengthViewLoader = new MvxImageViewLoader(() => imgStrength);

            this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<ReviewTableViewCell, ReviewModel>();
				set.Bind(lbTitle).To(m => m.Title);
				set.Bind(lbSubtitle).To(m => m.SubTitle);
                set.Bind(imgWordViewLoader).To(m => m.IsFavorite).WithConversion(new FavoriteImageValueConverter(),null);
                set.Bind(btnStar).To(vm=>vm.FlipSelected);
                set.Bind(imgStrengthViewLoader).To(m=>m.StrengthLevel).WithConversion(new ImageStrengthValueConverter(),null);
				set.Apply();
			});
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            imgWord.Layer.CornerRadius = 4;
            btnPlay.Layer.CornerRadius = btnPlay.Bounds.Height / 2;
            btnStar.Layer.CornerRadius = btnStar.Bounds.Height / 2;
        }
    }

	class ButtonConverter : MvxValueConverter<bool, UIColor>
	{
		UIColor selectedColour = UIColor.FromRGB(128, 128, 128);
		UIColor unSelectedColour = UIColor.GroupTableViewBackgroundColor;
		protected override UIColor Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
			return value ? selectedColour : unSelectedColour;
		}
		protected override bool ConvertBack(UIColor value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == selectedColour;
		}
	}

    public class FavoriteImageValueConverter:MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? "res:star_active" : "res:star";
        }
    }

	public class ImageUriValueConverter : MvxValueConverter<string, string>
	{
		protected override string Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
		{
			return "res:" + value;
		}
	}

    public class ImageStrengthValueConverter: MvxValueConverter<int, string>
    {
        protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 0:
                    return "res:entity_strength_0";
				case 1:
					return "res:entity_strength_1";
				case 2:
					return "res:entity_strength_2";
				case 3:
					return "res:entity_strength_3";
				case 4:
					return "res:entity_strength_4";
                default:
                    return "res:entity_strength_0";

            }
        }
    }

}
