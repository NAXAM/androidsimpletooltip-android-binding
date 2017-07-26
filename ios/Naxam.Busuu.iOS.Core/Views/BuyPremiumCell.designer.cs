// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Core
{
	[Register ("BuyPremiumCell")]
	partial class BuyPremiumCell
	{
		[Outlet]
		UIKit.UIButton btnGo { get; set; }

		[Outlet]
		UIKit.UIView contentView { get; set; }

		[Outlet]
		UIKit.UILabel lbPR { get; set; }

		[Outlet]
		UIKit.UIView viewRipple { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnGo != null) {
				btnGo.Dispose ();
				btnGo = null;
			}

			if (lbPR != null) {
				lbPR.Dispose ();
				lbPR = null;
			}

			if (viewRipple != null) {
				viewRipple.Dispose ();
				viewRipple = null;
			}

			if (contentView != null) {
				contentView.Dispose ();
				contentView = null;
			}
		}
	}
}
