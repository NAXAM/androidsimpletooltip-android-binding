using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Social.ViewModels;
using PatridgeDev;
using UIKit;
using Naxam.Busuu.iOS.Social.Common;
using AVFoundation;
using Foundation;

namespace Naxam.Busuu.iOS.Social.Views
{
    [MvxFromStoryboard(StoryboardName = "Social")]
    public partial class SocialDetailView : MvxViewController<SocialDetailViewModel>
    {
        PDRatingView ratingView;
        readonly MvxImageViewLoader _loaderImageUser;
        readonly MvxImageViewLoader _loaderImgQuestion;

		AVAudioPlayer SpeakMusicPlayer;
		NSTimer update_timer;

		UIImage playBtnBg, pauseBtnBg;

		public SocialDetailView(IntPtr handle) : base(handle)
        {
            _loaderImageUser = new MvxImageViewLoader(() => this.imgUserAvatar);
            _loaderImgQuestion = new MvxImageViewLoader(() => this.imgQuestion);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			ViewShadow.Layer.ShadowRadius = 2;
			ViewShadow.Layer.ShadowOpacity = 0.25f;
			ViewShadow.Layer.ShadowOffset = new CGSize(0, 2);

            var setBinding = this.CreateBindingSet<SocialDetailView, SocialDetailViewModel>();
            setBinding.Bind(_loaderImageUser).To(d => d.SocialDetailData.Avatar).WithConversion(new MyMvxConverter.ImageUriValueConverter(), null);
            setBinding.Bind(lblUserName).To(d => d.SocialDetailData.Name);
            setBinding.Bind(lblCountry).To(d => d.SocialDetailData.Country);
            setBinding.Bind(_loaderImgQuestion).To(d => d.SocialDetailData.ImgQuestion).WithConversion(new MyMvxConverter.ImageUriValueConverter(), null);
            setBinding.Bind(textQuestion).To(d => d.SocialDetailData.TextQuestion);
            setBinding.Bind(btnAddFriends).For(d => d.Hidden).To(d => d.SocialDetailData.Friends);
            setBinding.Bind(ViewAudioPlayer).For(d => d.Hidden).To(d => d.SocialDetailData.Speak).WithConversion(new MyMvxConverter.InverseValueConverter(), null);
            setBinding.Bind(audioViewBottomConstraint).For(x => x.Active).To(d => d.SocialDetailData.Speak);
            setBinding.Bind(audioViewTopConstraint).For(x => x.Active).To(d => d.SocialDetailData.Speak);
            setBinding.Bind(WriteText).For(d => d.Hidden).To(d => d.SocialDetailData.Speak);
            setBinding.Bind(WriteText).To(d => d.SocialDetailData.Write);
            setBinding.Bind(lblTimePublic).To(d => d.SocialDetailData.PublicTime).WithConversion(new MyMvxConverter.DatetimeStringValueConverter(), null);
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

            this.CreateBinding(ratingView).For(vm => vm.AverageRating).To<SocialDetailViewModel>(vm => vm.SocialDetailData.Star).Apply();

            ViewRate.Add(ratingView);
            ViewRate.SendSubviewToBack(ratingView);

			playBtnBg = UIImage.FromFile("play_btn.png");
			pauseBtnBg = UIImage.FromFile("pause_btn.png");

            if (WriteText.Hidden)
            {
				string[] arrPathfile = WriteText.Text.Split('.');
				if (arrPathfile.Length == 2)
				{
					var fileUrl = NSBundle.MainBundle.PathForResource(arrPathfile[0], arrPathfile[1]);
					if (fileUrl != null)
					{
						Uri songURL = new NSUrl(fileUrl);
						SpeakMusicPlayer = AVAudioPlayer.FromUrl(songURL);
						SpeakMusicPlayer.Volume = 1;
						SpeakMusicPlayer.NumberOfLoops = 0;
						SpeakMusicPlayer.FinishedPlaying -= SpeakMusicPlayer_FinishedPlaying;
						SpeakMusicPlayer.FinishedPlaying += SpeakMusicPlayer_FinishedPlaying;
						UpdateViewForPlayerInfo();
						UpdateViewForPlayerState();
					}
				}				
            }			
           
            imgUserAvatar.Layer.CornerRadius = imgUserAvatar.Frame.Width / 2;
            ButtonAudioPlay.Layer.CornerRadius = ButtonAudioPlay.Frame.Width / 2;
			ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);

			ViewAudioPlayer.Layer.CornerRadius = 2;        
            imgQuestion.Layer.CornerRadius = 2;

            var bbcolor = UIColor.FromRGB(217, 217, 217);

            ViewQuestion.Layer.BorderWidth = 0.75f;
            ViewQuestion.Layer.BorderColor = bbcolor.CGColor;
            ViewQuestion.Layer.CornerRadius = 2;

            btnAddFriends.Layer.BorderWidth = 0.75f;
            btnAddFriends.Layer.BorderColor = bbcolor.CGColor;
			btnAddFriends.ImageEdgeInsets = new UIEdgeInsets(4, 12, 4, 12);
            btnAddFriends.Layer.CornerRadius = btnAddFriends.Frame.Height / 2;
         
            var img = UIImage.FromBundle("play_icon_small");
            SliderSpeak.SetThumbImage(img, UIControlState.Normal);
            SliderSpeak.SetThumbImage(img, UIControlState.Selected);
            SliderSpeak.SetThumbImage(img, UIControlState.Highlighted);

            ViewBackGroud.Layer.ShadowRadius = 1;
            ViewBackGroud.Layer.ShadowOpacity = 0.25f;
            ViewBackGroud.Layer.ShadowOffset = new CGSize(0, 1);		
        }

        partial void ButtonAudioPlay_TouchUpInside(NSObject sender)
        {
			if (SpeakMusicPlayer.Playing)
			{
				PausePlayback();
			}
			else
			{
				StartPlayback();
			}
        }

		void SpeakMusicPlayer_FinishedPlaying(object sender, AVStatusEventArgs e)
		{
			UpdateViewForPlayerInfo();
			UpdateViewForPlayerState();
		}

		void UpdateCurrentTime()
		{
			if (SpeakMusicPlayer.Playing)
			{
				ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 10, 10, 10);
				ButtonAudioPlay.SetImage(pauseBtnBg, UIControlState.Normal);
				var min = (int)((SpeakMusicPlayer.Duration - SpeakMusicPlayer.CurrentTime) / 60);
				var sec = (int)((SpeakMusicPlayer.Duration - SpeakMusicPlayer.CurrentTime) % 60);
				lblTime.Text = String.Format("{0:D2}:{1:D2}", min, sec);
				SliderSpeak.Value = (float)SpeakMusicPlayer.CurrentTime;
			}
			else
			{
				ButtonAudioPlay.SetImage(playBtnBg, UIControlState.Normal);
			}
		}

		void UpdateViewForPlayerState()
		{
			if (SpeakMusicPlayer.Playing)
			{
				ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 10, 10, 10);
				ButtonAudioPlay.SetImage(pauseBtnBg, UIControlState.Normal);
				InvokeOnMainThread(() =>
				{
					update_timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.01), delegate
			   {
				   UpdateCurrentTime();
			   });
				});
			}
			else
			{
				ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);
				ButtonAudioPlay.SetImage(playBtnBg, UIControlState.Normal);
				if (update_timer != null)
				{
					update_timer.Invalidate();
					update_timer = null;
				}
			}
		}

		void UpdateViewForPlayerInfo()
		{
			SliderSpeak.Value = 0;
			SliderSpeak.MaxValue = (float)SpeakMusicPlayer.Duration;
			lblTime.Text = String.Format("{0:00}:{1:00}", (int)SpeakMusicPlayer.Duration / 60, (int)SpeakMusicPlayer.Duration % 60);
		}

		void PausePlayback()
		{
			SpeakMusicPlayer.Pause();
			UpdateViewForPlayerState();
		}

		void StartPlayback()
		{
			SpeakMusicPlayer.Play();
			UpdateViewForPlayerState();
		}
    }
}
