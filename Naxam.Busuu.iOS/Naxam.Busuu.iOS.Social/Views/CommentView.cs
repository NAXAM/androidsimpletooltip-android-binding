using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using Naxam.Busuu.iOS.Social.Common;
using Naxam.Busuu.Social.ViewModels;
using PatridgeDev;
using UIKit;

namespace Naxam.Busuu.iOS.Social.Views
{
    [MvxFromStoryboard(StoryboardName = "Social")]
	public partial class CommentView : MvxViewController<CommentViewModel>
	{
		PDRatingView ratingView;
		readonly MvxImageViewLoader _loaderImgQuestion;

		public CommentView (IntPtr handle) : base (handle)
		{
			_loaderImgQuestion = new MvxImageViewLoader(() => this.imgQuestion);
		}

        public override void ViewDidLoad()
        {
            this.Request = new MvxViewModelRequest<CommentViewModel>(null, null);

            base.ViewDidLoad();

			ViewShadow.Layer.ShadowRadius = 2;
			ViewShadow.Layer.ShadowOpacity = 0.3f;
			ViewShadow.Layer.ShadowOffset = new CGSize(2, 2);

			var setBinding = this.CreateBindingSet<CommentView, CommentViewModel>();
			setBinding.Bind(_loaderImgQuestion).To(d => d.CommentData.ImgQuestion).WithConversion(new MyMvxConverter.ImageUriValueConverter(), null);
			setBinding.Bind(textQuestion).To(d => d.CommentData.TextQuestion);
            setBinding.Bind(ViewForSpeak).For(d => d.Hidden).To(d => d.CommentData.Speak).WithConversion(new MyMvxConverter.InverseValueConverter(), null);
			setBinding.Bind(audioViewBottomConstraint).For(x => x.Active).To(d => d.CommentData.Speak);
			setBinding.Bind(audioViewTopConstraint).For(x => x.Active).To(d => d.CommentData.Speak);
            setBinding.Bind(ViewForWrite).For(d => d.Hidden).To(d => d.CommentData.Speak);
            setBinding.Bind(fieldCorrect).To(d => d.CommentData.Write);
            setBinding.Bind(textHowDid).To(d => d.CommentData.Name).WithConversion(new MyMvxConverter.TextHowDidValueConverter(), null);
			setBinding.Apply();

			var ratingConfig = new RatingConfig(UIImage.FromBundle("Stars" + "/grey_star"),
									UIImage.FromBundle("Stars" + "/yellow_star_d"),
									UIImage.FromBundle("Stars" + "/yellow_star_d"));

			ratingConfig.ItemPadding = 1;
			var ratingFrame = new CGRect(CGPoint.Empty, new CGSize(193, 24));

			ratingView = new PDRatingView(ratingFrame, ratingConfig);

            ViewStar.Add(ratingView);
            ViewStar.SendSubviewToBack(ratingView);

            decimal rating = 4;

			ratingView.AverageRating = rating;

			var bbcolor = UIColor.FromRGB(217, 217, 217);

			ViewQuestion.Layer.BorderWidth = 1;
			ViewQuestion.Layer.BorderColor = bbcolor.CGColor;

            ViewAudioPlayer.Layer.BorderWidth = 1;
            ViewAudioPlayer.Layer.BorderColor = bbcolor.CGColor;

			ViewAudioPlayer.Layer.CornerRadius = 2;
			ViewAudioPlayer.Layer.MasksToBounds = true;

            btnAudioPlay.Layer.CornerRadius = btnAudioPlay.Frame.Width / 2;
			btnAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);

			var img = UIImage.FromBundle("play_icon_small");
			SliderSpeak.SetThumbImage(img, UIControlState.Normal);
			SliderSpeak.SetThumbImage(img, UIControlState.Selected);
			SliderSpeak.SetThumbImage(img, UIControlState.Highlighted);

			ViewBossQuestion.Layer.CornerRadius = 2;
			ViewBossQuestion.Layer.MasksToBounds = true;

            btnSay.Layer.CornerRadius = btnSay.Frame.Width / 2;
			btnSay.Layer.MasksToBounds = true;
			btnSay.ImageEdgeInsets = new UIEdgeInsets(26, 30, 26, 30);
			btnSay.Layer.ShadowRadius = 2;
			btnSay.Layer.ShadowOpacity = 0.3f;
			btnSay.Layer.ShadowOffset = new CGSize(2, 2);

            var img2 = UIImage.FromBundle("conversation_speaking_button_red.png");
            btnSay.SetImage(img2, UIControlState.Selected);
            btnSay.SetImage(img2, UIControlState.Highlighted);
        }
	}
}
