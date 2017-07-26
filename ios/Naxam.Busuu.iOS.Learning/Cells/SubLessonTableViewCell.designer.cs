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
        UIKit.UIImageView imgSubLesson { get; set; }

        [Outlet]
        UIKit.UILabel lbTime { get; set; }

        [Outlet]
        UIKit.UILabel lbTitle { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (lbTitle != null) {
                lbTitle.Dispose ();
                lbTitle = null;
            }

            if (lbTime != null) {
                lbTime.Dispose ();
                lbTime = null;
            }

            if (imgSubLesson != null) {
                imgSubLesson.Dispose ();
                imgSubLesson = null;
            }
        }
    }
}
