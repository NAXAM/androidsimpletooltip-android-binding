using System;
using AVFoundation;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.iOS.Social.Common;
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

		//UIImage playBtnBg, pauseBtnBg;

		public FriendsCell(IntPtr handle) : base(handle)
        {
			_loaderImageUser = new MvxImageViewLoader(() => this.imgUserAvatar);
            _loaderImgLearn = new MvxImageViewLoader(() => this.imgLan);

            this.DelayBind(() =>
            {
                var setBinding = this.CreateBindingSet<FriendsCell, SocialModel>();
                setBinding.Bind(_loaderImageUser).To(f => f.Avatar).WithConversion(new MyMvxConverter.ImageUriValueConverter(), null);
                setBinding.Bind(lblUserName).To(f => f.Name);
                setBinding.Bind(lblCountry).To(f => f.Country);
                setBinding.Bind(_loaderImgLearn).To(f => f.ImageLearn).WithConversion(new MyMvxConverter.ImageForFriendsValueConverter(), null);
                setBinding.Bind(lblTimePublic).To(f => f.PublicTime);
                setBinding.Bind(ViewAudioPlayer).For(f => f.Hidden).To(f => f.Speak).WithConversion(new MyMvxConverter.InverseValueConverter(), null);
                setBinding.Bind(audioViewBottomConstraint).For(f => f.Active).To(f => f.Speak);
                setBinding.Bind(audioViewTopConstraint).For(f => f.Active).To(f => f.Speak);
                setBinding.Bind(WriteText).For(f => f.Hidden).To(f => f.Speak);
                setBinding.Bind(WriteText).To(f => f.Write);
                setBinding.Bind(lblRate).To(f => f.Star).WithConversion(new MyMvxConverter.TextRateValueConverter(), null);
                setBinding.Apply();
            });
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

			//playBtnBg = UIImage.FromFile("play_btn.png");
			//pauseBtnBg = UIImage.FromFile("pause_btn.png");
		}

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

			decimal rating = Convert.ToDecimal(lblRate.Text.Replace("(", "").Replace(")", ""));

			ratingView.AverageRating = rating;

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
					SpeakMusicPlayer.FinishedPlaying += delegate
					{
						UpdateViewForPlayerInfo();
						UpdateViewForPlayerState();
					};

					UpdateViewForPlayerInfo();
					UpdateViewForPlayerState();
				}
			}
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
				//lblTime.Text = String.Format("{0:00}:{1:00}", (int)(SpeakMusicPlayer.CurrentTime / 60), (int)(SpeakMusicPlayer.CurrentTime % 60));
				SliderSpeak.Value = (float)SpeakMusicPlayer.CurrentTime;
			}
			else
			{
				//ButtonPlay.SetImage(playBtnBg, UIControlState.Normal);
			}
		}

        void UpdateViewForPlayerState()
		{
			if (SpeakMusicPlayer.Playing)
			{
				update_timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.01), delegate
				{
					UpdateCurrentTime();
				});
			}
			else
			{
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
