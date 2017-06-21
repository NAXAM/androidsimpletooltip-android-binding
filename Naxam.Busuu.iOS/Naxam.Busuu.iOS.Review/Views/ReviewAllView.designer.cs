// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Naxam.Busuu.iOS.Review.Views
{
    [Register ("ReviewAllView")]
    partial class ReviewAllView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SubTotalTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (SubTotalTextField != null) {
                SubTotalTextField.Dispose ();
                SubTotalTextField = null;
            }
        }
    }
}