// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Review.Views
{
    [Register ("ReviewAllView")]
    partial class ReviewAllView
    {
        [Outlet]
        UIKit.UIButton btnDiscovery { get; set; }


        [Outlet]
        UIKit.UIButton btnFriends { get; set; }


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


        [Action ("btnDiscovery_TouchUpInside:")]
        partial void btnDiscovery_TouchUpInside (Foundation.NSObject sender);


        [Action ("btnFriends_TouchUpInside:")]
        partial void btnFriends_TouchUpInside (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnDiscovery != null) {
                btnDiscovery.Dispose ();
                btnDiscovery = null;
            }

            if (btnFriends != null) {
                btnFriends.Dispose ();
                btnFriends = null;
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