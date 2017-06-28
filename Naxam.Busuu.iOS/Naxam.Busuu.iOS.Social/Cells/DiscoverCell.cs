using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using Naxam.Busuu.Social.Models;
using CoreGraphics;
using MvvmCross.Platform.Converters;
using System.Globalization;

namespace Naxam.Busuu.iOS.Social
{
    public partial class DiscoverCell : MvxCollectionViewCell
    {
       
        private readonly MvxImageViewLoader _loaderImageUser;
        private readonly MvxImageViewLoader _loaderImgSpeak;
        private readonly MvxImageViewLoader _loaderImgLearn;

        public DiscoverCell(IntPtr handle) : base(handle)
        {
            _loaderImageUser = new MvxImageViewLoader(() => this.ImageUser); 
            _loaderImgSpeak = new MvxImageViewLoader(() => this.ImgSpeak);
            _loaderImgLearn = new MvxImageViewLoader(() => this.ImageLan);

            this.DelayBind(() =>
            {
                var setBinding = this.CreateBindingSet<DiscoverCell, Discover>();
                setBinding.Bind(_loaderImageUser).To(d => d.Avatar).WithConversion(new ImageUriValueConverter(), null);
                setBinding.Bind(NameUser).To(d => d.Name);
                setBinding.Bind(Country).To(d => d.Country);
                setBinding.Bind(_loaderImgSpeak).To(d => d.ImageSpeakLanguage).WithConversion(new ImageUriValueConverter(), null);
                setBinding.Bind(ViewSpeak).For(d => d.Hidden).To(d => d.Speak).WithConversion(new InverseValueConverter(), null);
				setBinding.Bind(audioViewBottomConstraint).For(x => x.Active).To(d => d.Speak);
				setBinding.Bind(audioViewTopConstraint).For(x => x.Active).To(d => d.Speak);
                setBinding.Bind(WriteLabel).For(d => d.Hidden).To(d => d.Speak);
				setBinding.Bind(WriteLabel).To(d => d.Write);
                setBinding.Bind(_loaderImgLearn).To(d => d.ImageLearn).WithConversion(new ImageUriValueConverter(), null);
                setBinding.Bind(TextLan).To(d => d.TextLearn);
                setBinding.Apply();
            });
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

			Layer.ShadowRadius = 2;
			Layer.ShadowOpacity = 0.3f;
			Layer.ShadowOffset = new CGSize(2, 2);

			ViewCell.Layer.CornerRadius = 5;
			ViewCell.Layer.MasksToBounds = true;

			var bbcolor = UIColor.FromRGB(224, 230, 235);

			ViewLan.Layer.BorderWidth = 1;
			ViewLan.Layer.BorderColor = bbcolor.CGColor;

			ViewHome.Layer.BorderWidth = 1;
			ViewHome.Layer.BorderColor = bbcolor.CGColor;
			ViewHome.Layer.CornerRadius = 2;
			ViewHome.Layer.MasksToBounds = true;

            ImageUser.Layer.CornerRadius = ImageUser.Frame.Width / 2;
            ImgSpeak.Layer.CornerRadius = ImgSpeak.Frame.Width / 2;
            ImageLan.Layer.CornerRadius = ImageLan.Frame.Width / 2;
            ButtonPlay.Layer.CornerRadius = ButtonPlay.Frame.Width / 2;

            ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(8, 10, 8, 8);
		}

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
        }
    }

	public class InverseValueConverter : MvxValueConverter<bool, bool>
	{
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo cultureInfo)
		{
			return !value;
		}
	}

	public class ImageUriValueConverter : MvxValueConverter<string, string>
	{
		protected override string Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
		{
			return "res:" + value;
		}
	}
}