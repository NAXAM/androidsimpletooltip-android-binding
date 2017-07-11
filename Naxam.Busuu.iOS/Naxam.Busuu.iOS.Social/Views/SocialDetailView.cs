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
        PDRatingView ratingView;
        readonly MvxImageViewLoader _loaderImageUser;
        readonly MvxImageViewLoader _loaderImgQuestion;

        public SocialDetailView(IntPtr handle) : base(handle)
        {
            _loaderImageUser = new MvxImageViewLoader(() => this.imgUserAvatar);
            _loaderImgQuestion = new MvxImageViewLoader(() => this.imgQuestion);
        }

        public override void ViewDidLoad()
        {
            this.Request = new MvxViewModelRequest<SocialDetailViewModel>(null, null);

            base.ViewDidLoad();

			ViewShadow.Layer.ShadowRadius = 2;
			ViewShadow.Layer.ShadowOpacity = 0.3f;
			ViewShadow.Layer.ShadowOffset = new CGSize(2, 2);

            var setBinding = this.CreateBindingSet<SocialDetailView, SocialDetailViewModel>();
            setBinding.Bind(_loaderImageUser).To(d => d.SocialDetailData.Avatar).WithConversion(new MyMvxConverter.ImageUriValueConverter(), null);
            setBinding.Bind(lblUserName).To(d => d.SocialDetailData.Name);
            setBinding.Bind(lblCountry).To(d => d.SocialDetailData.Country);
            setBinding.Bind(_loaderImgQuestion).To(d => d.SocialDetailData.ImgQuestion).WithConversion(new MyMvxConverter.ImageUriValueConverter(), null);
            setBinding.Bind(textQuestion).To(d => d.SocialDetailData.TextQuestion);
            setBinding.Bind(ViewAudioPlayer).For(d => d.Hidden).To(d => d.SocialDetailData.Speak).WithConversion(new MyMvxConverter.InverseValueConverter(), null);
            setBinding.Bind(audioViewBottomConstraint).For(x => x.Active).To(d => d.SocialDetailData.Speak);
            setBinding.Bind(audioViewTopConstraint).For(x => x.Active).To(d => d.SocialDetailData.Speak);
            setBinding.Bind(WriteText).For(d => d.Hidden).To(d => d.SocialDetailData.Speak);
            setBinding.Bind(WriteText).To(d => d.SocialDetailData.Write);
            setBinding.Bind(lblTimePublic).To(d => d.SocialDetailData.PublicTime);
            setBinding.Bind(lblRate).To(d => d.SocialDetailData.Star).WithConversion(new MyMvxConverter.TextRateValueConverter(), null);
            setBinding.Bind(btnFeedBack).To(d => d.CommentViewCommand);
            setBinding.Apply();

            var imgstar = UIImage.FromBundle("play_icon_small");
            SliderSpeak.SetThumbImage(imgstar, UIControlState.Normal);
            SliderSpeak.SetThumbImage(imgstar, UIControlState.Selected);
            SliderSpeak.SetThumbImage(imgstar, UIControlState.Highlighted);

            var ratingConfig = new RatingConfig(UIImage.FromBundle("Stars" + "/grey_star2"),
                                    UIImage.FromBundle("Stars" + "/yellow_star_d"),
                                    UIImage.FromBundle("Stars" + "/yellow_star_d"));

            ratingConfig.ItemPadding = 1;
            var ratingFrame = new CGRect(CGPoint.Empty, new CGSize(95, 20));

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

            ViewQuestion.Layer.BorderWidth = 1;
            ViewQuestion.Layer.BorderColor = bbcolor.CGColor;
            ViewQuestion.Layer.CornerRadius = 2;
            ViewQuestion.Layer.MasksToBounds = true;

            btnAddFriends.Layer.BorderWidth = 0.5f;
            btnAddFriends.Layer.BorderColor = bbcolor.CGColor;
			btnAddFriends.ImageEdgeInsets = new UIEdgeInsets(4, 8, 4, 8);

            ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);

            var img = UIImage.FromBundle("play_icon_small");
            SliderSpeak.SetThumbImage(img, UIControlState.Normal);
            SliderSpeak.SetThumbImage(img, UIControlState.Selected);
            SliderSpeak.SetThumbImage(img, UIControlState.Highlighted);

            ViewBackGroud.Layer.ShadowRadius = 1;
            ViewBackGroud.Layer.ShadowOpacity = 0.3f;
            ViewBackGroud.Layer.ShadowOffset = new CGSize(1, 1);
        }
    }
}
