using System;
using System.Globalization;
using AVFoundation;
using CoreAnimation;
using CoreGraphics;
using FFImageLoading;
using FFImageLoading.Work;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Platform.Converters;
using Naxam.Busuu.iOS.Core.Views;
using Naxam.Busuu.Review.Models;
using UIKit;


namespace Naxam.Busuu.iOS.Review.Views
{
    public partial class ReviewTableViewCell : MvxTableViewCell
    {
        private ReviewModel _item;
        public ReviewModel Item { get => _item; set => _item = value; }
        private AVAudioPlayer _ringtoneAudioPlayer;
        static bool isPlaying;

        RippleLayer StarShape, PlayShape, CellShape;

        public void SetupCell()
        {
            lbTitle.Text = Item.Title;
            lbSubtitle.Text = Item.SubTitle;
            switch (Item.StrengthLevel)
            {
                case 0: imgStrength.Image = UIImage.FromBundle("strength_0"); break;
                case 1: imgStrength.Image = UIImage.FromBundle("strength_1"); break;
                case 2: imgStrength.Image = UIImage.FromBundle("strength_2"); break;
                case 3: imgStrength.Image = UIImage.FromBundle("strength_3"); break;
                case 4: imgStrength.Image = UIImage.FromBundle("strength_4"); break;
                default:
                    imgStrength.Image = UIImage.FromBundle("strength_0"); break;
            }
            if (Item.IsFavorite)
            {
                btnStar.SetImage(UIImage.FromBundle("rating_star_gold"), UIControlState.Normal);
            }
            else
            {
                btnStar.SetImage(UIImage.FromBundle("rating_star_grey"), UIControlState.Normal);
            }

            ImageService.Instance.LoadUrl(Item.ImgWord).
                        LoadingPlaceholder("image_placeholder", ImageSource.CompiledResource).
                        Into(imgWord);

            _ringtoneAudioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename(Item.SoundUrl));
            _ringtoneAudioPlayer.NumberOfLoops = -1;
            if (isPlaying)
            {
                PlayRingtone();
            }
            else
            {
                StopRingtone();
            }
        }

        protected ReviewTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic
        }

        partial void btnStar_TouchUpInside(NSObject sender)
        {
            Item.IsFavorite = !Item.IsFavorite;
            if (Item.IsFavorite)
            {
                btnStar.SetImage(UIImage.FromBundle("rating_star_gold"), UIControlState.Normal);
            }
            else
            {
                btnStar.SetImage(UIImage.FromBundle("rating_star_grey"), UIControlState.Normal);
            }
            StarShape.WillAnimate();
        }

        partial void btnPlay_TouchUpInside(NSObject sender)
        {
            isPlaying = !isPlaying;
            if (isPlaying)
            {
                PlayRingtone();
            }
            else
            {
                StopRingtone();
            }
            PlayShape.WillAnimate();
        }

        public override void AwakeFromNib()
        {

            base.AwakeFromNib();
            imgWord.Layer.CornerRadius = 4;
            StarShape = new RippleLayer(ContentView, UIColor.LightGray, UIColor.Clear);
            CellShape = new RippleLayer(ContentView, UIColor.LightGray, UIColor.Clear);
            PlayShape = new RippleLayer(ContentView, UIColor.LightGray, UIColor.Clear);
            StarShape.Frame = new CGRect(btnStar.Center.X - 38, btnStar.Center.Y - 18, 36, 36);
            StarShape.Path = UIBezierPath.FromOval(new CGRect(0, 0, 36, 36)).CGPath;
			PlayShape.Frame = new CGRect(btnPlay.Center.X - 34, btnPlay.Center.Y - 18, 36, 36);
			PlayShape.Path = UIBezierPath.FromOval(new CGRect(0, 0, 36, 36)).CGPath;

            var cellGesture = new UITapGestureRecognizer((UITapGestureRecognizer obj) =>
            {
                var touchLocation = obj.LocationInView(ContentView);
                CellShape.WillAnimateTapGesture(touchLocation);
            });
            ContentView.AddGestureRecognizer(cellGesture);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
        }

        public void PlayRingtone()
        {
            if (_ringtoneAudioPlayer != null)
            {
                _ringtoneAudioPlayer.Stop();
            }
            _ringtoneAudioPlayer.Play();
        }

        public void StopRingtone()
        {
            if (_ringtoneAudioPlayer != null)
            {
                _ringtoneAudioPlayer.Stop();
            }
        }
    }
}       
