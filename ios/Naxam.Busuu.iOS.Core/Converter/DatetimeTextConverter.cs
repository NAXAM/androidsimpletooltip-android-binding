using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class DatetimeTextConverter : MvxValueConverter<DateTime, string>
    {
		protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo cultureInfo)
		{
			return value.ToString("f").Replace(" PM", "PM").Replace(" AM", "AM");
		}
    }
}
