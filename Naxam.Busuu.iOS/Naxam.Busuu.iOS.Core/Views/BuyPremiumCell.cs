using Foundation;
using System;
using UIKit;
using ObjCRuntime;

namespace Naxam.Busuu.iOS.Core
{
    public partial class BuyPremiumCell : UIView
    {
        public BuyPremiumCell (IntPtr handle) : base (handle)
        {
        }

		public static BuyPremiumCell Create()
		{
			var arr = NSBundle.MainBundle.LoadNib("BuyPremiumCell", null, null);
			var v = Runtime.GetNSObject<BuyPremiumCell>(arr.ValueAt(0));
			v.Layer.ShadowColor = UIColor.LightGray.CGColor;
			v.Layer.ShadowOpacity = 0.8f;
			v.Layer.ShadowRadius = 3.0f;
			v.Layer.ShadowOffset = new CoreGraphics.CGSize(1.0, 1.0);
			return v;
		}

		public override void AwakeFromNib()
		{
			btnGo.Layer.CornerRadius = btnGo.Bounds.Size.Height / 2;
		}
    }
}