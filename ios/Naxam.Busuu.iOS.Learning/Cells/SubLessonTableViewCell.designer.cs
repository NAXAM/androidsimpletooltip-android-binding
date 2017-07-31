// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Learning
{
	[Register ("SubLessonTableViewCell")]
	partial class SubLessonTableViewCell
	{
		[Outlet]
		UIKit.UIView exerciseView { get; set; }

		[Outlet]
		UIKit.UILabel lbTime { get; set; }

		[Outlet]
		UIKit.UILabel lbTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (exerciseView != null) {
				exerciseView.Dispose ();
				exerciseView = null;
			}

			if (lbTime != null) {
				lbTime.Dispose ();
				lbTime = null;
			}

			if (lbTitle != null) {
				lbTitle.Dispose ();
				lbTitle = null;
			}
		}
	}
}
