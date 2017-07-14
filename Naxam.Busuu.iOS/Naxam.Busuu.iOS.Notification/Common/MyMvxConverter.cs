using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using UIKit;

namespace Naxam.Busuu.iOS.Notification.Common
{
    public class MyMvxConverter
    {
		public class ImageUriValueConverter : MvxValueConverter<string, string>
		{
			protected override string Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
			{
				return "res:" + value;
			}
		}

        public class ColorValueConverter : MvxValueConverter<bool, UIColor>
		{
            protected override UIColor Convert(bool value, Type targetType, object parameter, CultureInfo cultureInfo)
			{
                UIColor abcd;
                if (value)
                {
                    abcd = UIColor.White;
                }
                else
                {
                    abcd = UIColor.FromRGB(242, 245, 248);
                }

                return abcd;
			}
		}
    }
}
