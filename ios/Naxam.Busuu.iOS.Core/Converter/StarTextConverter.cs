using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class StarTextConverter : MvxValueConverter<double, string>
    {
		protected override string Convert(double value, Type targetType, object parameter, CultureInfo cultureInfo)
		{
			return "(" + value + ")";
		}
    }
}
