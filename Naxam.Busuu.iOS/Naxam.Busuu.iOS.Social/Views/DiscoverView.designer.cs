// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social
{
	[Register ("DiscoverView")]
	partial class DiscoverView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UICollectionView DiscoverCollectionView { get; set; }

		[Outlet]
		UIKit.UIView ViewBarItem { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DiscoverCollectionView != null) {
				DiscoverCollectionView.Dispose ();
				DiscoverCollectionView = null;
			}

			if (ViewBarItem != null) {
				ViewBarItem.Dispose ();
				ViewBarItem = null;
			}
		}
	}
}
