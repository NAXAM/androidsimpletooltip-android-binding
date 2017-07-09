using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Social.Common
{
    public static class MyMvxConverter
    {
		public class InverseValueConverter : MvxValueConverter<bool, bool>
		{
			protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo cultureInfo)
			{
				return !value;
			}
		}

		public class ImageUriValueConverter : MvxValueConverter<string, string>
		{
			protected override string Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
			{
				return "res:" + value;
			}
		}

		public class TextRateValueConverter : MvxValueConverter<double, string>
		{
			protected override string Convert(double value, Type targetType, object parameter, CultureInfo cultureInfo)
			{
				return "(" + value + ")";
			}
		}
    }
}
