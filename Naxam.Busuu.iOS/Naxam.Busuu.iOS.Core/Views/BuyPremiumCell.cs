using System;
using Foundation;
using UIKit;

namespace Naxam.Busuu.iOS.Core.Views
{
    public class BuyPremiumCell: UIView
    {
		public static readonly UINib Nib = UINib.FromName("BuyPremiumCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString("BuyPremiumCell");

		public static BuyPremiumCell Create()
		{
			return (BuyPremiumCell)Nib.Instantiate(null, null)[0];
		}
    }
}
