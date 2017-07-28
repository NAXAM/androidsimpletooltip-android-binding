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
    [Register ("LessonTableViewCell")]
    partial class LessonTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnDownload { get; set; }

        [Outlet]
        UIKit.UIImageView imgLesson { get; set; }

        [Outlet]
        UIKit.UILabel lbNumber { get; set; }

        [Outlet]
        UIKit.UILabel lbTitle { get; set; }

        [Outlet]
        UIKit.NSLayoutConstraint numberLblHeightConstraint { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (btnDownload != null) {
                btnDownload.Dispose ();
                btnDownload = null;
            }

            if (imgLesson != null) {
                imgLesson.Dispose ();
                imgLesson = null;
            }

            if (lbNumber != null) {
                lbNumber.Dispose ();
                lbNumber = null;
            }

            if (lbTitle != null) {
                lbTitle.Dispose ();
                lbTitle = null;
            }

            if (numberLblHeightConstraint != null) {
                numberLblHeightConstraint.Dispose ();
                numberLblHeightConstraint = null;
            }
        }
    }
}
