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

        public DiscoverCell(IntPtr handle) : base(string.Empty, handle)
        {
            _loaderImageUser = new MvxImageViewLoader(() => this.ImageUser); 
            _loaderImgSpeak = new MvxImageViewLoader(() => this.ImgSpeak);
            _loaderImgLearn = new MvxImageViewLoader(() => this.ImageLan);

            this.DelayBind(() =>
            {
                var setBinding = this.CreateBindingSet<DiscoverCell, Discover>();
                setBinding.Bind(_loaderImageUser).To(d => d.Avatar);
                setBinding.Bind(NameUser).To(d => d.Name);
                setBinding.Bind(Country).To(d => d.Country);
                setBinding.Bind(_loaderImgSpeak).To(d => d.ImageSpeakLanguage);
                setBinding.Bind(ViewSpeak).For(d => d.Hidden).To(d => d.Speak);
                setBinding.Bind(WriteLabel).For(d => d.Hidden).To(d => d.Speak).WithConversion(new InverseValueConverter(), null);
				setBinding.Bind(WriteLabel).To(d => d.Write);
                setBinding.Bind(_loaderImgLearn).To(d => d.ImageLearn);
                setBinding.Bind(TextLan).To(d => d.TextLearn);
                setBinding.Apply();
            });
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

			Layer.ShadowRadius = 2;
            Layer.ShadowOpacity = 0.3f;
            Layer.ShadowOffset = new CGSize(2, 2);
            this.ClipsToBounds = false;

            ViewCell.Layer.CornerRadius = 5;
            ViewCell.Layer.MasksToBounds = true;
            ViewCell.ClipsToBounds = true;

			var bbcolor = UIColor.FromRGB(242, 245, 248);
            ViewHome.Layer.BorderWidth = 1;
            ViewHome.Layer.BorderColor = bbcolor.CGColor;
			ViewHome.Layer.CornerRadius = 2;
			ViewHome.Layer.MasksToBounds = true;
			ViewHome.ClipsToBounds = true;			
        }
    }

	public class InverseValueConverter : MvxValueConverter<bool, bool>
	{
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo cultureInfo)
		{
			return !value;
		}
	}
}