using System;

using Foundation;
using UIKit;

namespace Naxam.Busuu.iOS.Review.Views
{
    public partial class ReviewTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ReviewTableViewCell");
        public static readonly UINib Nib;

        static ReviewTableViewCell()
        {
            Nib = UINib.FromName("ReviewTableViewCell", NSBundle.MainBundle);
        }

        protected ReviewTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
