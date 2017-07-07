using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using Naxam.Busuu.Social.Models;
using CoreGraphics;
using MvvmCross.Platform.Converters;
using System.Globalization;
using AVFoundation;
using Naxam.Busuu.Social.ViewModels;

namespace Naxam.Busuu.iOS.Social.Cells
{
    public partial class DiscoverCell : MvxCollectionViewCell
    {      
        private readonly MvxImageViewLoader _loaderImageUser;
        private readonly MvxImageViewLoader _loaderImgSpeak;
        private readonly MvxImageViewLoader _loaderImgLearn;

        private AVAudioPlayer SpeakMusicPlayer;
        private NSTimer update_timer;

		//UIImage playBtnBg, pauseBtnBg;

        public DiscoverCell(IntPtr handle) : base(handle)
        {
            _loaderImageUser = new MvxImageViewLoader(() => this.ImageUser); 
            _loaderImgSpeak = new MvxImageViewLoader(() => this.ImgSpeak);
            _loaderImgLearn = new MvxImageViewLoader(() => this.ImageLan);

            this.DelayBind(() =>
            {
                var setBinding = this.CreateBindingSet<DiscoverCell, DiscoverModel>();
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

                //var setBinding2 = this.CreateBindingSet<DiscoverCell, DiscoverViewModel>();
                //setBinding2.Bind(ButtonVIEW).To(vm => vm.PopModalCommand);
                //setBinding2.Bind(ButtonVIEW).For("Clicked").To(vm => vm.PopModalCommand);
                //setBinding2.Apply();
			});
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

			Layer.ShadowRadius = 2;
			Layer.ShadowOpacity = 0.3f;
			Layer.ShadowOffset = new CGSize(2, 2);

			ViewCell.Layer.CornerRadius = 2;
			ViewCell.Layer.MasksToBounds = true;

			var bbcolor = UIColor.FromRGB(224, 230, 235);

            ViewLan.Layer.BorderWidth = 0.5f;
			ViewLan.Layer.BorderColor = bbcolor.CGColor;

			ViewHome.Layer.BorderWidth = 0.5f;
			ViewHome.Layer.BorderColor = bbcolor.CGColor;
			ViewHome.Layer.CornerRadius = 2;
			ViewHome.Layer.MasksToBounds = true;

            ImageUser.Layer.CornerRadius = ImageUser.Frame.Width / 2;
            ImgSpeak.Layer.CornerRadius = ImgSpeak.Frame.Width / 2;
            ImageLan.Layer.CornerRadius = ImageLan.Frame.Width / 2;
            ButtonPlay.Layer.CornerRadius = ButtonPlay.Frame.Width / 2;

            ButtonPlay.ImageEdgeInsets = new UIEdgeInsets(9, 11, 9, 9);
                    
            var img = UIImage.FromBundle("play_icon_small");
            SliderSpeak.SetThumbImage(img, UIControlState.Normal);
            SliderSpeak.SetThumbImage(img, UIControlState.Selected);
            SliderSpeak.SetThumbImage(img, UIControlState.Highlighted);

			//playBtnBg = UIImage.FromFile("play_btn.png");
            //pauseBtnBg = UIImage.FromFile("pause_btn.png");			
		}

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            string[] arrPathfile = WriteLabel.Text.Split('.');
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

        private void UpdateCurrentTime()
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

        private void UpdateViewForPlayerState()
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

        private void UpdateViewForPlayerInfo()
		{
			SliderSpeak.Value = 0;
			SliderSpeak.MaxValue = (float)SpeakMusicPlayer.Duration;
            lblTime.Text = String.Format("{0:00}:{1:00}", (int)SpeakMusicPlayer.Duration / 60, (int)SpeakMusicPlayer.Duration % 60);
		}

        private void PausePlayback()
		{          
            SpeakMusicPlayer.Pause();
			UpdateViewForPlayerState();
		}

        private void StartPlayback()
		{           
            SpeakMusicPlayer.Play();
            UpdateViewForPlayerState();
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