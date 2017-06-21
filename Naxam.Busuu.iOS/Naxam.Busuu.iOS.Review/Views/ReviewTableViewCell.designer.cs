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
    [Register ("ReviewTableViewCell")]
    partial class ReviewTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnPlay { get; set; }

        [Outlet]
        UIKit.UIButton btnStar { get; set; }

        [Outlet]
        UIKit.UIImageView imgSignal { get; set; }

        [Outlet]
        UIKit.UIImageView imgWord { get; set; }

        [Outlet]
        UIKit.UILabel lbSubtitle { get; set; }

        [Outlet]
        UIKit.UILabel lbTitle { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (imgWord != null) {
                imgWord.Dispose ();
                imgWord = null;
            }

            if (imgSignal != null) {
                imgSignal.Dispose ();
                imgSignal = null;
            }

            if (lbTitle != null) {
                lbTitle.Dispose ();
                lbTitle = null;
            }

            if (lbSubtitle != null) {
                lbSubtitle.Dispose ();
                lbSubtitle = null;
            }

            if (btnStar != null) {
                btnStar.Dispose ();
                btnStar = null;
            }

            if (btnPlay != null) {
                btnPlay.Dispose ();
                btnPlay = null;
            }
        }
    }
}
