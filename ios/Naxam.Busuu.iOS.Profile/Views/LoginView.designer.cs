// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Profile
{
	[Register ("LoginView")]
	partial class LoginView
	{
		[Outlet]
		UIKit.UIButton btnFacebook { get; set; }

		[Outlet]
		UIKit.UIButton btnGoogle { get; set; }

		[Outlet]
		UIKit.UIButton btnLogin { get; set; }

		[Outlet]
		UIKit.UIView viewbtnFacebook { get; set; }

		[Outlet]
		UIKit.UIView viewbtnGoogle { get; set; }

		[Outlet]
		UIKit.UIView ViewShadow { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ViewShadow != null) {
				ViewShadow.Dispose ();
				ViewShadow = null;
			}

			if (btnFacebook != null) {
				btnFacebook.Dispose ();
				btnFacebook = null;
			}

			if (btnGoogle != null) {
				btnGoogle.Dispose ();
				btnGoogle = null;
			}

			if (viewbtnFacebook != null) {
				viewbtnFacebook.Dispose ();
				viewbtnFacebook = null;
			}

			if (viewbtnGoogle != null) {
				viewbtnGoogle.Dispose ();
				viewbtnGoogle = null;
			}

			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}
		}
	}
}
