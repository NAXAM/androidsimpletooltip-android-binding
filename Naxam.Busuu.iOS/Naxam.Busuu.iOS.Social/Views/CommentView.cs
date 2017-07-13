using System;
using AVFoundation;
using CoreGraphics;
using Foundation;
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

		AVAudioPlayer SpeakMusicPlayer;
		NSTimer update_timer;

		UIImage playBtnBg, pauseBtnBg;

        UIBarButtonItem btnsend;

		public CommentView (IntPtr handle) : base (handle)
		{
			_loaderImgQuestion = new MvxImageViewLoader(() => this.imgQuestion);
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnsend = new UIBarButtonItem(UIImage.FromBundle("ic_send_light"), UIBarButtonItemStyle.Plain, (sender, args) => { });
            NavigationItem.RightBarButtonItem = btnsend;
            btnsend.Enabled = false;

            ViewShadow.Layer.ShadowRadius = 2;
            ViewShadow.Layer.ShadowOpacity = 0.25f;
            ViewShadow.Layer.ShadowOffset = new CGSize(0, 2);

            var setBinding = this.CreateBindingSet<CommentView, CommentViewModel>();
            setBinding.Bind(_loaderImgQuestion).To(d => d.CommentData.ImgQuestion).WithConversion(new MyMvxConverter.ImageUriValueConverter(), null);
            setBinding.Bind(textQuestion).To(d => d.CommentData.TextQuestion);
            setBinding.Bind(ViewForSpeak).For(d => d.Hidden).To(d => d.CommentData.Speak).WithConversion(new MyMvxConverter.InverseValueConverter(), null);
            setBinding.Bind(audioViewBottomConstraint).For(x => x.Active).To(d => d.CommentData.Speak);
            setBinding.Bind(audioViewTopConstraint).For(x => x.Active).To(d => d.CommentData.Speak);
            setBinding.Bind(ViewForWrite).For(d => d.Hidden).To(d => d.CommentData.Speak);
            setBinding.Bind(textViewCorrect).To(d => d.CommentData.Write);
            setBinding.Bind(textHowDid).To(d => d.CommentData.Name).WithConversion(new MyMvxConverter.TextHowDidValueConverter(), null);
            setBinding.Apply();

            var ratingConfig = new RatingConfig(UIImage.FromBundle("Stars" + "/grey_star"),
                                    UIImage.FromBundle("Stars" + "/yellow_star_d"),
                                    UIImage.FromBundle("Stars" + "/yellow_star_d"));

            ratingConfig.ItemPadding = 1;
            var ratingFrame = new CGRect(CGPoint.Empty, new CGSize(193, 24));

            ratingView = new PDRatingView(ratingFrame, ratingConfig);

            ratingView.RatingChosen += RatingView_RatingChosen;

            decimal rating = 0;
            ratingView.AverageRating = rating;

            ViewStar.Add(ratingView);
            ViewStar.SendSubviewToBack(ratingView);

            playBtnBg = UIImage.FromFile("play_btn.png");
            pauseBtnBg = UIImage.FromFile("pause_btn.png");

            if (ViewForWrite.Hidden)
            {
                string[] arrPathfile = textViewCorrect.Text.Split('.');
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

            ViewBossQuestion.Layer.CornerRadius = 2;

            var bbcolor = UIColor.FromRGB(217, 217, 217);

            ViewQuestion.Layer.BorderWidth = 0.75f;
            ViewQuestion.Layer.BorderColor = bbcolor.CGColor;

            ViewAudioPlayer.Layer.BorderWidth = 0.75f;
            ViewAudioPlayer.Layer.BorderColor = bbcolor.CGColor;
            ViewAudioPlayer.Layer.CornerRadius = 2;

            btnAudioPlay.Layer.CornerRadius = btnAudioPlay.Frame.Width / 2;
            btnAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);

            var img = UIImage.FromBundle("play_icon_small");
            SliderSpeak.SetThumbImage(img, UIControlState.Normal);
            SliderSpeak.SetThumbImage(img, UIControlState.Selected);
            SliderSpeak.SetThumbImage(img, UIControlState.Highlighted);

            var img2 = UIImage.FromBundle("conversation_speaking_button_red.png");
            btnSay.SetImage(img2, UIControlState.Selected);
            btnSay.SetImage(img2, UIControlState.Highlighted);
            btnSay.Layer.CornerRadius = btnSay.Frame.Width / 2;
            btnSay.Layer.MasksToBounds = false;
            btnSay.ImageEdgeInsets = new UIEdgeInsets(26, 30, 26, 30);
            btnSay.Layer.ShadowRadius = 1;
            btnSay.Layer.ShadowOpacity = 0.25f;
            btnSay.Layer.ShadowOffset = new CGSize(0, 1);

            textViewComment.ShouldBeginEditing += TextViewShouldBeginEditing;
            textViewComment.ShouldEndEditing += TextViewShouldEndEditing;
        }

        private bool TextViewShouldEndEditing(UITextView textView)
        {
            if (textView.Text == "") {
                textView.Text = TextViewPlaceHolder;
                textView.TextColor = UIColor.FromRGB(173, 182, 187);
            }
            return true;
        }

        private const string TextViewPlaceHolder = "Leave a comment";

        private bool TextViewShouldBeginEditing(UITextView textView)
        {
            if (textView.Text == TextViewPlaceHolder) {
                textView.Text = "";
                textView.TextColor = UIColor.Black;
            }
            return true;
        }

        void RatingView_RatingChosen(object sender, RatingChosenEventArgs e)
        {
            btnsend.Enabled = true;
        }

        partial void btnAudioPlay_TouchUpInside(NSObject sender)
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
				btnAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 10, 10, 10);
				btnAudioPlay.SetImage(pauseBtnBg, UIControlState.Normal);
				var min = (int)((SpeakMusicPlayer.Duration - SpeakMusicPlayer.CurrentTime) / 60);
				var sec = (int)((SpeakMusicPlayer.Duration - SpeakMusicPlayer.CurrentTime) % 60);
				lblTime.Text = String.Format("{0:D2}:{1:D2}", min, sec);
				SliderSpeak.Value = (float)SpeakMusicPlayer.CurrentTime;
			}
			else
			{
				btnAudioPlay.SetImage(playBtnBg, UIControlState.Normal);
			}
		}

		void UpdateViewForPlayerState()
		{
			if (SpeakMusicPlayer.Playing)
			{
				btnAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 10, 10, 10);
				btnAudioPlay.SetImage(pauseBtnBg, UIControlState.Normal);
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
				btnAudioPlay.ImageEdgeInsets = new UIEdgeInsets(10, 12, 10, 10);
				btnAudioPlay.SetImage(playBtnBg, UIControlState.Normal);
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (textViewComment != null) {
                textViewComment.ShouldEndEditing -= TextViewShouldEndEditing;
                textViewComment.ShouldBeginEditing -= TextViewShouldBeginEditing;
            }
        }
    }
}
