using System;
using FFImageLoading;
using FFImageLoading.Work;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.Review.Models;
using UIKit;


namespace Naxam.Busuu.iOS.Review.Views
{
    public partial class ReviewTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ReviewTableViewCell");
        public static readonly UINib Nib;

        private readonly MvxImageViewLoader imgWordViewLoader;
        private readonly MvxImageViewLoader imgStrengthViewLoader;
        static ReviewTableViewCell()
        {
            Nib = UINib.FromName("ReviewTableViewCell", NSBundle.MainBundle);
        }

		public static ReviewTableViewCell Create()
		{
			return (ReviewTableViewCell)Nib.Instantiate(null, null)[0];
		}

        protected ReviewTableViewCell(IntPtr handle) : base(handle)
        {
			// Note: this .ctor should not contain any initialization logic.
            imgWordViewLoader = new MvxImageViewLoader(() => imgWord);
            imgStrengthViewLoader = new MvxImageViewLoader(() => imgStrength);
            this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<ReviewTableViewCell, ReviewAllModel>();
				set.Bind(lbTitle).To(m => m.Title);
				set.Bind(lbSubtitle).To(m => m.SubTitle);
				set.Bind(imgWordViewLoader).To(m => m.ImgWord);
                set.Bind(imgStrengthViewLoader).To(m=>m.ImgStrength);
				set.Apply();
			});
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            btnPlay.Layer.CornerRadius = btnPlay.Bounds.Height / 2;
            btnStar.Layer.CornerRadius = btnStar.Bounds.Height / 2;
        }

		protected void UpdateContent()
		{
            ImageService.Instance.LoadUrl()
						.ErrorPlaceholder("star_active.png", ImageSource.ApplicationBundle)
						.LoadingPlaceholder("star_active.png", ImageSource.CompiledResource)
						.Into(imgWord);
		}

    }
}
