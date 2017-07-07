// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Review.Views
{
	[Register ("ReviewAllView")]
	partial class ReviewAllView
	{
		[Outlet]
		UIKit.UIButton btnAll { get; set; }

		[Outlet]
		UIKit.UIButton btnFavorite { get; set; }

		[Outlet]
		UIKit.UILabel lbButtonClicked { get; set; }

		[Outlet]
		UIKit.UITableView ReviewTableView { get; set; }

		[Outlet]
		UIKit.UISearchBar searchBar { get; set; }

		[Outlet]
		UIKit.UIView uiViewButton { get; set; }

		[Outlet]
		UIKit.UIView uiViewSlide { get; set; }

		[Action ("btnAll_TouchUpInside:")]
		partial void btnAll_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnFavorite_TouchUpInside:")]
		partial void btnFavorite_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAll != null) {
				btnAll.Dispose ();
				btnAll = null;
			}

			if (btnFavorite != null) {
				btnFavorite.Dispose ();
				btnFavorite = null;
			}

			if (lbButtonClicked != null) {
				lbButtonClicked.Dispose ();
				lbButtonClicked = null;
			}

			if (ReviewTableView != null) {
				ReviewTableView.Dispose ();
				ReviewTableView = null;
			}

			if (searchBar != null) {
				searchBar.Dispose ();
				searchBar = null;
			}

			if (uiViewButton != null) {
				uiViewButton.Dispose ();
				uiViewButton = null;
			}

			if (uiViewSlide != null) {
				uiViewSlide.Dispose ();
				uiViewSlide = null;
			}
		}
	}
}
