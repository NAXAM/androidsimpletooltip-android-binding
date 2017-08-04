// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Notification.Views
{
	[Register ("NotificationView")]
	partial class NotificationView
	{
		[Outlet]
		UIKit.UITableView NotificationTableView { get; set; }

		[Outlet]
		UIKit.UIView ViewShadow { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NotificationTableView != null) {
				NotificationTableView.Dispose ();
				NotificationTableView = null;
			}

			if (ViewShadow != null) {
				ViewShadow.Dispose ();
				ViewShadow = null;
			}
		}
	}
}
