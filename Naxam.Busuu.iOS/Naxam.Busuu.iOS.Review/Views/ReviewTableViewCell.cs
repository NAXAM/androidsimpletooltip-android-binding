using System;
using System.Globalization;
using FFImageLoading;
using FFImageLoading.Work;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Platform.Converters;
using Naxam.Busuu.Review.Models;
using UIKit;


namespace Naxam.Busuu.iOS.Review.Views
{
    public partial class ReviewTableViewCell : MvxTableViewCell
    {
		private ReviewModel item;
		public ReviewModel Item { get => item; set => item = value; }

        public void SetupCell()
        {
            lbTitle.Text = item.Title;
            lbSubtitle.Text = item.SubTitle;
            switch (item.StrengthLevel)
            {
                case 0: imgStrength.Image = UIImage.FromBundle("strength_0"); break;
                case 1: imgStrength.Image = UIImage.FromBundle("strength_1"); break;
                case 2: imgStrength.Image = UIImage.FromBundle("strength_2"); break;
                case 3: imgStrength.Image = UIImage.FromBundle("strength_3"); break;
                case 4: imgStrength.Image = UIImage.FromBundle("strength_4"); break;
                default:
                    imgStrength.Image = UIImage.FromBundle("strength_0"); break;
            }
            if(item.IsFavorite)
            {
                btnStar.SetImage(UIImage.FromBundle("rating_star_gold"), UIControlState.Normal);
            }else
            {
                btnStar.SetImage(UIImage.FromBundle("rating_star_grey"), UIControlState.Normal);
            }

            ImageService.Instance.LoadUrl(item.ImgWord).
                        ErrorPlaceholder("image_placeholder.png",ImageSource.ApplicationBundle).
                        LoadingPlaceholder("placeholder", ImageSource.CompiledResource).
                        Into(imgWord);
        }

        protected ReviewTableViewCell(IntPtr handle) : base(handle)
        {
			// Note: this .ctor should not contain any initialization logic.
        }

        partial void btnStar_TouchUpInside(NSObject sender)
        {
            item.IsFavorite = !item.IsFavorite;
			if (item.IsFavorite)
			{
				btnStar.SetImage(UIImage.FromBundle("rating_star_gold"), UIControlState.Normal);
			}
			else
			{
				btnStar.SetImage(UIImage.FromBundle("rating_star_grey"), UIControlState.Normal);
			}
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            imgWord.Layer.CornerRadius = 4;
        }
    }
}
