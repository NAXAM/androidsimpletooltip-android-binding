using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Core.Converter
{
    public class NotifyTypeToTextConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // pass 
            ViewType type = (ViewType)value;
            if(type==ViewType.Accpect)
            {
                return "Binh Do has accepted your friend request";

            }
            else if (type == ViewType.Reply)    
            {
                return "Binh Do replied";
            }
            if (type == ViewType.Request)
            {
                return "Binh Do Request ";

            }
            else if (type == ViewType.Like)
            {
                return "Binh Do liked your comment";

            }
            if (type == ViewType.Correct)
            {
                return "Binh Do corrected your excise";

            }
            return "Binh Do nothing to show";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
