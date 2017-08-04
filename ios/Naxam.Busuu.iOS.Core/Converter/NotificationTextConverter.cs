using System;
using System.Globalization;
using Foundation;
using MvvmCross.Platform.Converters;
using UIKit;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class NotificationTextConverter : MvxValueConverter<string, NSAttributedString>
    {
		protected override NSAttributedString Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
		{
			var firstAttributes = new UIStringAttributes
			{
				Font = UIFont.BoldSystemFontOfSize(14f)
			};

			var prettyString = new NSMutableAttributedString(value.Replace("|", ""));

			string[] abcd = value.Split('|');

            if (abcd.Length >= 2)
            {
                prettyString.SetAttributes(firstAttributes.Dictionary, new NSRange(0, abcd[0].Length));
            }

			return prettyString;
		}
    }
}
