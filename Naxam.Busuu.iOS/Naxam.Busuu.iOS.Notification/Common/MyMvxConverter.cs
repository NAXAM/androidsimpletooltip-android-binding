using System;
using System.Globalization;
using Foundation;
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

		public class DetailValueConverter : MvxValueConverter<string, NSAttributedString>
		{
			protected override NSAttributedString Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
			{
				var firstAttributes = new UIStringAttributes
				{
					Font = UIFont.BoldSystemFontOfSize(14f)
				};

                var prettyString = new NSMutableAttributedString(value.Replace("*", ""));

                string[] abcd = value.Split('*');

                prettyString.SetAttributes(firstAttributes.Dictionary, new NSRange(0, abcd[1].Length));

                return prettyString;
			}
		}

		public class DatetimeStringValueConverter : MvxValueConverter<DateTime, string>
		{
			protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo cultureInfo)
			{
                DateTime nowDate = DateTime.Now;

                int diff2 = (int)(nowDate - value).TotalDays;

                if (diff2 == 0)
                {
                    return "Today";
                }
                else if ((diff2 > 0) && (diff2 <= 3))
                {
                    return diff2.ToString() + " days ago";
                }
                else
                {
                    return value.ToString("f").Replace(" PM", "PM").Replace(" AM", "AM");
                }
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
