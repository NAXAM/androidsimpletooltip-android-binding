using System;
using AVFoundation;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using Naxam.Busuu.iOS.Social.Common;
using Naxam.Busuu.Social.ViewModels;
using PatridgeDev;
using ObjCRuntime;
using UIKit;
using Xamarin.CircularProgress.iOS;

namespace Naxam.Busuu.iOS.Social.Views
{
    [MvxFromStoryboard(StoryboardName = "Social")]
    public partial class CommentView : MvxViewController<CommentViewModel>
	{
		PDRatingView ratingView;
		readonly MvxImageViewLoader _loaderImgQuestion;

		AVAudioPlayer SpeakMusicPlayer;
		NSTimer update_timer;
        NSTimer update_timer2;
		NSTimer update_timer3;

		UIImage playBtnBg, pauseBtnBg;

        UIBarButtonItem btnsend;
        bool IsAnimationSld;
        bool IsAnimationBtnRemove;
        bool IsVoiceRecorde;

        CircularProgress fourColorCircularProgress;

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

            ViewSay.Layer.CornerRadius = ViewSay.Frame.Width / 2;
            ViewSay2.Layer.CornerRadius = ViewSay2.Frame.Width / 2;

            ViewSay3.Layer.CornerRadius = ViewSay3.Frame.Width / 2;
            ViewSay3.Layer.MasksToBounds = false;
            ViewSay3.Layer.ShadowRadius = 1.5f;
			ViewSay3.Layer.ShadowOpacity = 0.25f;
			ViewSay3.Layer.ShadowOffset = new CGSize(0, 1.5f);

            var img2 = UIImage.FromBundle("conversation_speaking_button_red.png");
            btnSay.SetImage(img2, UIControlState.Selected);
            btnSay.SetImage(img2, UIControlState.Highlighted);
            btnSay.Layer.CornerRadius = btnSay.Frame.Width / 2;
            btnSay.Layer.MasksToBounds = false;
            btnSay.ImageEdgeInsets = new UIEdgeInsets(28, 32, 28, 32);  

            btnRemove.Layer.CornerRadius = btnRemove.Frame.Width / 2;
            btnRemove.Layer.MasksToBounds = false;
            btnRemove.Layer.ShadowRadius = 1.5f;
			btnRemove.Layer.ShadowOpacity = 0.25f;
			btnRemove.Layer.ShadowOffset = new CGSize(0, 1.5f);
            btnRemove.ImageEdgeInsets = new UIEdgeInsets(4, 4, 4, 4);

			textViewComment.ShouldBeginEditing += TextViewShouldBeginEditing;
            textViewComment.ShouldEndEditing += TextViewShouldEndEditing;

            btnSay.TouchDown += BtnSay_TouchDown; 
            btnSay.TouchUpInside += BtnSay_TouchUpInside;

			btnRemove.Alpha = 0;
			
            ConfigureFourColorCircularProgress();
		}

		private void ConfigureFourColorCircularProgress()
		{
			var frame = new CGRect(-0.4, -0.4, 85.4, 85.4);

			fourColorCircularProgress = new CircularProgress(frame);
			fourColorCircularProgress.StartAngle = fourColorCircularProgress.EndAngle = -90;
            fourColorCircularProgress.Progress = 0;
            fourColorCircularProgress.LineWidth = 2.5;

            fourColorCircularProgress.Colors = new[]
            {			
				UIColor.Red.CGColor
			};

			ViewSay2.AddSubview(fourColorCircularProgress);
		}

		int DemNguoc = 35;

		void BtnSay_TouchDown(object sender, EventArgs e)
		{
            if (!IsVoiceRecorde)
            {
                ViewSay3.Layer.ShadowRadius = 2.5f;
                ViewSay3.Layer.ShadowOpacity = 0.35f;
                ViewSay3.Layer.ShadowOffset = new CGSize(0, 2.5f);

                if (update_timer2 == null)
                {
                    update_timer3 = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.01), delegate
                        {
                            UpdateCurrentTime3();
                        });
                }
            }
		}

		void BtnSay_TouchUpInside(object sender, EventArgs e)
		{
            if (DemNguoc == 0)
            {
                StopAnimationSld();
            }
            else
            {			
				if (update_timer3 != null)
				{
					update_timer3.Invalidate();
					update_timer3 = null;
				}

				ViewSay3.Layer.ShadowRadius = 1.5f;
				ViewSay3.Layer.ShadowOpacity = 0.25f;
				ViewSay3.Layer.ShadowOffset = new CGSize(0, 1.5f);

				DemNguoc = 25;
			}
		}

		void UpdateCurrentTime3()
		{
			DemNguoc--;
			if (DemNguoc == 0)
			{
				if (update_timer3 != null)
				{
					update_timer3.Invalidate();
					update_timer3 = null;
				}

				StartAnimationSld();				
			}
		}

		double slidervalue;
		//int timeSay = 30;

        void UpdateCurrentTime2()
        {
            ViewSay.Layer.CornerRadius = ViewSay.Frame.Width / 2;
            slidervalue += 0.0003333333;

            if (slidervalue < 1)
            {
                ViewSay.Layer.CornerRadius = ViewSay.Bounds.Width / 2;
                fourColorCircularProgress.Progress = slidervalue;
                //timeSay -= (int)(30 * 0.0003333333);
                //lblTimeSay.Text = String.Format("-{0:D2}:{1:D2}", 0, timeSay);
            }
            else
            {
                StopAnimationSld();
            }
        }

        void StartAnimationSld()
        {
            if (!IsAnimationSld)
			{
                IsAnimationSld = true;
                               
				UIView.BeginAnimations("sliderAnimation");
				UIView.SetAnimationDuration(0.35);
				UIView.SetAnimationCurve(UIViewAnimationCurve.EaseOut);
				UIView.SetAnimationDelegate(this);
				UIView.SetAnimationDidStopSelector(new Selector("animationDidStop:finished:context:"));
				ViewSay.Bounds = new CGRect(0, 0, 130, 130);
				UIView.CommitAnimations();
			}
        }

        void StopAnimationSld()
        {
            if (IsAnimationSld)
			{
                IsAnimationSld = false;

                ViewSay3.Layer.ShadowRadius = 1.5f;
				ViewSay3.Layer.ShadowOpacity = 0.25f;
				ViewSay3.Layer.ShadowOffset = new CGSize(0, 1.5f);				

				UIView.BeginAnimations("sliderAnimation");
				UIView.SetAnimationDuration(0.25);
				UIView.SetAnimationCurve(UIViewAnimationCurve.EaseOut);
				UIView.SetAnimationDelegate(this);
				UIView.SetAnimationDidStopSelector(new Selector("animationDidStop:finished:context:"));
				ViewSay.Bounds = new CGRect(0, 0, 82, 82);
				UIView.CommitAnimations();
			}
		}

		[Export("animationDidStop:finished:context:")]
		void SlideStopped(NSString animationID, NSNumber finished, NSObject context)
		{
            if (IsAnimationSld)
            {
                ViewSay.Layer.Bounds = new CGRect(0, 0, 130, 130);

				update_timer2 = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.01), delegate
				{
					UpdateCurrentTime2();
				});
            }
            else
            {
				ViewSay.Layer.Bounds = new CGRect(0, 0, 82, 82);

				if (update_timer2 != null)
				{
					update_timer2.Invalidate();
					update_timer2 = null;
				}

				slidervalue = 0;
                //timeSay = 30;           

				lblTimeSay.Text = "Review your correction";

				fourColorCircularProgress.Progress = 0;
                btnSay.Transform = CGAffineTransform.MakeRotation((float)Math.PI);
                StartAnimationBtnSay();
            }

            ViewSay.Layer.CornerRadius = ViewSay.Frame.Width / 2;
		}

        void StartAnimationBtnSay()
        {
            if (!IsAnimationBtnRemove)
            {
                IsAnimationBtnRemove = true;
                btnRemove.Transform = CGAffineTransform.MakeRotation((float)Math.PI);

                UIView.BeginAnimations("btnSayAnimation");
                UIView.SetAnimationDuration(0.25);
                UIView.SetAnimationCurve(UIViewAnimationCurve.EaseOut);
                UIView.SetAnimationDelegate(this);
                UIView.SetAnimationDidStopSelector(new Selector("animationBtnSay:finished:context:"));
                btnSay.Transform = CGAffineTransform.MakeRotation((float)Math.PI * 2);
                btnRemove.Center = new CGPoint(btnRemove.Center.X + 68, btnRemove.Center.Y);    
                btnRemove.Transform = CGAffineTransform.MakeRotation(0);
                btnRemove.Alpha = 1;
                UIView.CommitAnimations();
            }
        }

		void StopAnimationBtnSay()
		{
            if (IsAnimationBtnRemove)
            {
                IsAnimationBtnRemove = false;

                UIView.BeginAnimations("btnSayAnimation");
                UIView.SetAnimationDuration(0.25);
                UIView.SetAnimationCurve(UIViewAnimationCurve.EaseOut);
                UIView.SetAnimationDelegate(this);
                UIView.SetAnimationDidStopSelector(new Selector("animationBtnSay:finished:context:"));
                btnSay.Transform = CGAffineTransform.MakeRotation(0);
                btnRemove.Alpha = 0;
                UIView.CommitAnimations();
            }
		}

        [Export("animationBtnSay:finished:context:")]
        void btnSayStopped(NSString animationID, NSNumber finished, NSObject context)
        {
            if (IsAnimationBtnRemove)
            {
                btnSay.Transform = CGAffineTransform.MakeRotation((float)Math.PI * 2);
				btnRemove.Center = new CGPoint(btnRemove.Center.X, btnRemove.Center.Y);
                btnRemove.Transform = CGAffineTransform.MakeRotation(0);
				btnRemove.Alpha = 1;

				var playBtnSay = UIImage.FromFile("conversation_speaking_play_button.png");
				btnSay.SetImage(playBtnSay, UIControlState.Normal);
				btnSay.SetImage(playBtnSay, UIControlState.Selected);
				btnSay.SetImage(playBtnSay, UIControlState.Highlighted);				

                btnSay.ImageEdgeInsets = new UIEdgeInsets(30, 35, 30, 30);

                IsVoiceRecorde = true;
			}
            else
            {
				DemNguoc = 35;

                lblTimeSay.Text = "Hold the button to record your corection";

				btnSay.Transform = CGAffineTransform.MakeRotation(0);
				btnRemove.Center = new CGPoint(btnSay.Center.X, btnSay.Center.Y);
                btnRemove.Alpha = 0;

				var playBtnSay = UIImage.FromFile("conversation_speaking_button_Blue.png");
				btnSay.SetImage(playBtnSay, UIControlState.Normal);

				var img2 = UIImage.FromBundle("conversation_speaking_button_red.png");
				btnSay.SetImage(img2, UIControlState.Selected);
				btnSay.SetImage(img2, UIControlState.Highlighted);
             
                btnSay.ImageEdgeInsets = new UIEdgeInsets(28, 32, 28, 32);

                IsVoiceRecorde = false;
			}
        }

        partial void btnRemove_TouchUpInside(NSObject sender)
        {
            btnSay.Transform = CGAffineTransform.MakeRotation((float)Math.PI);
            StopAnimationBtnSay();
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
