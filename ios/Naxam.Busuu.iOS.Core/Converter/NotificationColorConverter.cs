﻿using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using UIKit;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class NotificationColorConverter : MvxValueConverter<bool, UIColor>
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
