using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Naxam.Busuu.iOS.Core.Converter
{
    public class FriendsImgSpeakConverter : MvxValueConverter<string, string>
    {
		// Lười quá đành phải làm thế  ahuhu T.T
		protected override string Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
		{
			return "res:list_flagcut_enc.png";
		}
    }
}
