﻿using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class NotificationDatetimeConverter : MvxValueConverter<DateTime, string>
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
}
