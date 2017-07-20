// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Core
{
    [Register ("BuyPremiumCell")]
    partial class BuyPremiumCell
    {
        [Outlet]
        UIKit.UIButton btnGo { get; set; }


        [Outlet]
        UIKit.UILabel lbPR { get; set; }

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
        }
    }
}