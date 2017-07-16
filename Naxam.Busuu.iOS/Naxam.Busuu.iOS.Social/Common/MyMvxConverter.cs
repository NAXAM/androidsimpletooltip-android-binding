using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Social.Common
{
    public class MyMvxConverter
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

		public class TextHowDidValueConverter : MvxValueConverter<string, string>
		{
			protected override string Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
			{
				return "How did " + value + " do?";
			}
		}

        public class DatetimeStringValueConverter : MvxValueConverter<DateTime, string>
		{
            protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo cultureInfo)
			{
				return value.ToString("f").Replace(" PM", "PM").Replace(" AM", "AM");
			}
		}

        // Lười quá đành phải làm thế  ahuhu T.T
        public class ImageForFriendsValueConverter : MvxValueConverter<string, string>
		{
			protected override string Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
			{
				return "res:list_flagcut_enc.png";
			}
		}
    }
}
