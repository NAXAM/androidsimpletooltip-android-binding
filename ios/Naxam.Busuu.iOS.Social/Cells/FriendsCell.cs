using System;
using AVFoundation;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.Social.Models;
using PatridgeDev;
using UIKit;

namespace Naxam.Busuu.iOS.Social.Cells
{
    public partial class FriendsCell : MvxTableViewCell
    {
        public event EventHandler<SocialModel> ViewFriendsHandler;

        PDRatingView ratingView;
        readonly MvxImageViewLoader _loaderImageUser;
        readonly MvxImageViewLoader _loaderImgLearn;

		AVAudioPlayer SpeakMusicPlayer;
		NSTimer update_timer;
		string fileUrl;

		UIImage playBtnBg, pauseBtnBg;

		public FriendsCell(IntPtr handle) : base(handle)
        {
			_loaderImageUser = new MvxImageViewLoader(() => this.imgUserAvatar);
            _loaderImgLearn = new MvxImageViewLoader(() => this.imgLan);

            this.DelayBind(() =>
            {
                var setBinding = this.CreateBindingSet<FriendsCell, SocialModel>();
                setBinding.Bind(_loaderImageUser).To(f => f.Avatar).WithConversion("ImageUriValueConverter");
                setBinding.Bind(lblUserName).To(f => f.Name);
                setBinding.Bind(lblCountry).To(f => f.Country);
                setBinding.Bind(_loaderImgLearn).To(f => f.ImageLearn).WithConversion("FriendsImgSpeakValueConverter");
                //setBinding.Bind(lblTimePublic).To(f => f.PublicTime).WithConversion("DatetimeTextConverter");
                setBinding.Bind(lblTimePublic).To(f => f.PostedTime).WithConversion("PostedTimeToStringConverter");
                setBinding.Bind(ViewAudioPlayer).For(f => f.Hidden).To(f => f.Speak).WithConversion("BoolInverseConverter");
                setBinding.Bind(audioViewBottomConstraint).For(f => f.Active).To(f => f.Speak);
                setBinding.Bind(audioViewTopConstraint).For(f => f.Active).To(f => f.Speak);
                setBinding.Bind(WriteText).For(f => f.Hidden).To(f => f.Speak);
                setBinding.Bind(WriteText).To(f => f.Write);
                setBinding.Bind(lblRate).To(f => f.Star).WithConversion("StarTextConverter");
                setBinding.Bind(ratingView).For(vm => vm.AverageRating).To(vm => vm.Star).Apply();
                setBinding.Apply();
            });

			playBtnBg = UIImage.FromFile("play_btn.png");
			pauseBtnBg = UIImage.FromFile("pause_btn.png");
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            imgUserAvatar.Layer.CornerRadius = imgUserAvatar.Frame.Width / 2;

			ViewAudioPlayer.Layer.CornerRadius = 2;

			ButtonAudioPlay.Layer.CornerRadius = ButtonAudioPlay.Frame.Width / 2;          
            ButtonAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);

            var img = UIImage.FromBundle("play_icon_small");
            SliderSpeak.SetThumbImage(img, UIControlState.Normal);
            SliderSpeak.SetThumbImage(img, UIControlState.Selected);
            SliderSpeak.SetThumbImage(img, UIControlState.Highlighted);

			ViewBackGroud.Layer.ShadowRadius = 1;
			ViewBackGroud.Layer.ShadowOpacity = 0.25f;
			ViewBackGroud.Layer.ShadowOffset = new CGSize(0, 1);

			var ratingConfig = new RatingConfig(UIImage.FromBundle("Stars" + "/grey_star2"),
									UIImage.FromBundle("Stars" + "/yellow_star_d"),
									UIImage.FromBundle("Stars" + "/yellow_star_d"));
            
            ratingConfig.ItemPadding = 1;
			var ratingFrame = new CGRect(CGPoint.Empty, new CGSize(95, 20));
           
			ratingView = new PDRatingView(ratingFrame, ratingConfig);

			ViewRate.Add(ratingView);
            ViewRate.SendSubviewToBack(ratingView);		
		}

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

			string[] arrPathfile = WriteText.Text.Split('.');
			if (arrPathfile.Length == 2)
			{
				var fileUrl2 = NSBundle.MainBundle.PathForResource(arrPathfile[0], arrPathfile[1]);
				if ((fileUrl2 != null) && (fileUrl != fileUrl2))
				{
					fileUrl = fileUrl2;
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
			else
			{
				if (SpeakMusicPlayer != null)
					SpeakMusicPlayer.Pause();
			}
        }

		void SpeakMusicPlayer_FinishedPlaying(object sender, AVStatusEventArgs e)
		{
			UpdateViewForPlayerInfo();
			UpdateViewForPlayerState();
		}

		partial void ButtonRate_TouchUpInside(NSObject sender)
		{
			ViewFriendsHandler?.Invoke(this, (SocialModel)DataContext);
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
