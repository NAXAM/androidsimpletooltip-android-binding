// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social.Cells
{
	[Register ("FriendsCell")]
	partial class FriendsCell
	{
		[Outlet]
		UIKit.UIImageView imgLan { get; set; }

		[Outlet]
		UIKit.UIImageView imgUserAvatar { get; set; }

		[Outlet]
		UIKit.UILabel lblCountry { get; set; }

		[Outlet]
		UIKit.UILabel lblUserName { get; set; }

		[Outlet]
		UIKit.UILabel textLan { get; set; }

		[Action ("ButtonRate_TouchUpInside:")]
		partial void ButtonRate_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (imgUserAvatar != null) {
				imgUserAvatar.Dispose ();
				imgUserAvatar = null;
			}

			if (lblUserName != null) {
				lblUserName.Dispose ();
				lblUserName = null;
			}

			if (lblCountry != null) {
				lblCountry.Dispose ();
				lblCountry = null;
			}

			if (imgLan != null) {
				imgLan.Dispose ();
				imgLan = null;
			}

			if (textLan != null) {
				textLan.Dispose ();
				textLan = null;
			}
		}
	}
}
