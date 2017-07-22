using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using Naxam.Busuu.Social.Models;
using CoreGraphics;
using AVFoundation;

namespace Naxam.Busuu.iOS.Social.Cells
{
    public partial class DiscoverCell : MvxCollectionViewCell
    {
        public event EventHandler<SocialModel> ViewDiscoverHandler;

        readonly MvxImageViewLoader _loaderImageUser;
        readonly MvxImageViewLoader _loaderImgSpeak;
        readonly MvxImageViewLoader _loaderImgLearn;

        AVAudioPlayer SpeakMusicPlayer;
        NSTimer update_timer;
        string fileUrl;

        UIImage playBtnBg, pauseBtnBg;

        public DiscoverCell(IntPtr handle) : base(handle)
        {
            _loaderImageUser = new MvxImageViewLoader(() => this.ImageUser);
            _loaderImgSpeak = new MvxImageViewLoader(() => this.ImgSpeak);
            _loaderImgLearn = new MvxImageViewLoader(() => this.ImageLan);

            this.DelayBind(() =>
            {
                var setBinding = this.CreateBindingSet<DiscoverCell, SocialModel>();
                setBinding.Bind(_loaderImageUser).To(d => d.Avatar).WithConversion("ImageUriValueConverter");
                setBinding.Bind(NameUser).To(d => d.Name);
                setBinding.Bind(Country).To(d => d.Country);
                setBinding.Bind(_loaderImgSpeak).To(d => d.ImageSpeakLanguage).WithConversion("ImageUriValueConverter");
                setBinding.Bind(ViewSpeak).For(d => d.Hidden).To(d => d.Speak).WithConversion("BoolInverseConverter");
                setBinding.Bind(audioViewBottomConstraint).For(x => x.Active).To(d => d.Speak);
                setBinding.Bind(audioViewTopConstraint).For(x => x.Active).To(d => d.Speak);
                setBinding.Bind(WriteLabel).For(d => d.Hidden).To(d => d.Speak);
                setBinding.Bind(WriteLabel).To(d => d.Write);
                setBinding.Bind(_loaderImgLearn).To(d => d.ImageLearn).WithConversion("ImageUriValueConverter");
                setBinding.Bind(TextLan).To(d => d.TextLearn);
                setBinding.Apply();
            });

			playBtnBg = UIImage.FromFile("play_btn.png");
			pauseBtnBg = UIImage.FromFile("pause_btn.png");
		}

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Layer.ShadowRadius = 2;
            Layer.ShadowOpacity = 0.25f;
            Layer.ShadowOffset = new CGSize(0, 2);

            ViewCell.Layer.CornerRadius = 2;

            ViewSpeak.Layer.CornerRadius = 2;

            var bbcolor = UIColor.FromRGB(224, 230, 235);

            ViewLan.Layer.BorderWidth = 0.5f;
            ViewLan.Layer.BorderColor = bbcolor.CGColor;

            ViewHome.Layer.BorderWidth = 0.5f;
            ViewHome.Layer.BorderColor = bbcolor.CGColor;
            ViewHome.Layer.CornerRadius = 2;

            ImageUser.Layer.CornerRadius = ImageUser.Frame.Width / 2;
            ImgSpeak.Layer.CornerRadius = ImgSpeak.Frame.Width / 2;
            ImageLan.Layer.CornerRadius = ImageLan.Frame.Width / 2;

            ButtonPlay.Layer.CornerRadius = ButtonPlay.Frame.Width / 2;
            ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(9, 11, 9, 9);

            var img = UIImage.FromBundle("play_icon_small");
            SliderSpeak.SetThumbImage(img, UIControlState.Normal);
            SliderSpeak.SetThumbImage(img, UIControlState.Selected);
            SliderSpeak.SetThumbImage(img, UIControlState.Highlighted);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

			string[] arrPathfile = WriteLabel.Text.Split('.');
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

        partial void btnView_TouchUpInside(NSObject sender)
        {
            ViewDiscoverHandler?.Invoke(this, (SocialModel)DataContext);
        }

        partial void ButtonPlay_TouchUpInside(NSObject sender)
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
                ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(9, 9, 9, 9);
                ButtonPlay.SetImage(pauseBtnBg, UIControlState.Normal);
                var min = (int)((SpeakMusicPlayer.Duration - SpeakMusicPlayer.CurrentTime) / 60);
                var sec = (int)((SpeakMusicPlayer.Duration - SpeakMusicPlayer.CurrentTime) % 60);
                lblTime.Text = String.Format("{0:D2}:{1:D2}", min , sec);
                SliderSpeak.Value = (float)SpeakMusicPlayer.CurrentTime;
            }
            else
            {
                ButtonPlay.SetImage(playBtnBg, UIControlState.Normal);
            }
        }

        void UpdateViewForPlayerState()
        {
            if (SpeakMusicPlayer.Playing)
            {
                ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(9, 9, 9, 9);
                ButtonPlay.SetImage(pauseBtnBg, UIControlState.Normal);
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
                ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(9, 11, 9, 9);
                ButtonPlay.SetImage(playBtnBg, UIControlState.Normal);
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