// This file has been autogenerated from a class added in the UI designer.

using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Social.ViewModels;
using PatridgeDev;
using UIKit;
using Naxam.Busuu.iOS.Social.Common;

namespace Naxam.Busuu.iOS.Social.Views
{
    [MvxFromStoryboard(StoryboardName = "Social")]
    public partial class SocialDetailView : MvxViewController<SocialDetailViewModel>
	{
        private int sdvid = 1;
		private PDRatingView ratingView;
        private readonly MvxImageViewLoader _loaderImageUser;
        private readonly MvxImageViewLoader _loaderImgQuestion;

		public SocialDetailView (IntPtr handle) : base (handle)
		{
			_loaderImageUser = new MvxImageViewLoader(() => this.imgUserAvatar);
            _loaderImgQuestion = new MvxImageViewLoader(() => this.imgQuestion);
		}

        public override void ViewDidLoad()
        {
            this.Request = new MvxViewModelRequest<SocialDetailViewModel>(null, null);

            base.ViewDidLoad();

			var setBinding = this.CreateBindingSet<SocialDetailView, SocialDetailViewModel>();
            setBinding.Bind(_loaderImageUser).To(d => d.SocialDetailData[sdvid].Avatar).WithConversion(new MyMvxConverter.ImageUriValueConverter(), null);
			setBinding.Bind(lblUserName).To(d => d.SocialDetailData[sdvid].Name);
			setBinding.Bind(lblCountry).To(d => d.SocialDetailData[sdvid].Country);
			setBinding.Bind(_loaderImgQuestion).To(d => d.SocialDetailData[sdvid].ImgQuestion).WithConversion(new MyMvxConverter.ImageUriValueConverter(), null);
			setBinding.Bind(textQuestion).To(d => d.SocialDetailData[sdvid].TextQuestion);
			setBinding.Bind(ViewAudioPlayer).For(d => d.Hidden).To(d => d.SocialDetailData[sdvid].Speak).WithConversion(new MyMvxConverter.InverseValueConverter(), null);
			setBinding.Bind(audioViewBottomConstraint).For(x => x.Active).To(d => d.SocialDetailData[sdvid].Speak);
			setBinding.Bind(audioViewTopConstraint).For(x => x.Active).To(d => d.SocialDetailData[sdvid].Speak);
			setBinding.Bind(WriteText).For(d => d.Hidden).To(d => d.SocialDetailData[sdvid].Speak);
			setBinding.Bind(WriteText).To(d => d.SocialDetailData[sdvid].Write);
            setBinding.Bind(lblTimePublic).To(d => d.SocialDetailData[sdvid].PublicTime);
            setBinding.Bind(lblRate).To(d => d.SocialDetailData[sdvid].Star).WithConversion(new MyMvxConverter.TextRateValueConverter(), null);
			setBinding.Apply();

			var imgstar = UIImage.FromBundle("play_icon_small");
			SliderSpeak.SetThumbImage(imgstar, UIControlState.Normal);
			SliderSpeak.SetThumbImage(imgstar, UIControlState.Selected);
			SliderSpeak.SetThumbImage(imgstar, UIControlState.Highlighted);

			var ratingConfig = new RatingConfig(UIImage.FromBundle("Stars" + "/grey_star"),
									UIImage.FromBundle("Stars" + "/yellow_star_d"),
									UIImage.FromBundle("Stars" + "/yellow_star_d"));

			ratingConfig.ItemPadding = 1;
			var ratingFrame = new CGRect(CGPoint.Empty, new CGSize(100, 24));

			ratingView = new PDRatingView(ratingFrame, ratingConfig);

            ViewRate.Add(ratingView);
            ViewRate.SendSubviewToBack(ratingView);

			decimal rating = Convert.ToDecimal(lblRate.Text.Replace("(", "").Replace(")", ""));

			ratingView.AverageRating = rating;

			imgUserAvatar.Layer.CornerRadius = imgUserAvatar.Frame.Width / 2;
			ButtonAudioPlay.Layer.CornerRadius = ButtonAudioPlay.Frame.Width / 2;
			ViewAudioPlayer.Layer.CornerRadius = 2;
			ViewAudioPlayer.Layer.MasksToBounds = true;
			imgQuestion.Layer.CornerRadius = 2;
			imgQuestion.Layer.MasksToBounds = true;

			var bbcolor = UIColor.FromRGB(217, 217, 217);

			ViewQuestion.Layer.BorderWidth = 0.5f;
			ViewQuestion.Layer.BorderColor = bbcolor.CGColor;
			ViewQuestion.Layer.CornerRadius = 2;
			ViewQuestion.Layer.MasksToBounds = true;

			ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);

			var img = UIImage.FromBundle("play_icon_small");
			SliderSpeak.SetThumbImage(img, UIControlState.Normal);
			SliderSpeak.SetThumbImage(img, UIControlState.Selected);
			SliderSpeak.SetThumbImage(img, UIControlState.Highlighted);

			ViewBackGroud.Layer.ShadowRadius = 2;
			ViewBackGroud.Layer.ShadowOpacity = 0.3f;
			ViewBackGroud.Layer.ShadowOffset = new CGSize(2, 2);
		}		
	}
}
