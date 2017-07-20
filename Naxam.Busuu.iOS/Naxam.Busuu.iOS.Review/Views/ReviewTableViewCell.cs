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

        public Action AnimationDidStartFunc;
        public Action<bool> AnimationDidStopFunc;
        CAShapeLayer StarShape, PlayShape, CellShape;
        public double AnimationDuration = 5;
        ButtonPressAnimationDelegate AnimationDelegate { get; set; }

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
            StarShape.FillColor = UIColor.LightGray.CGColor;
            if (Item.IsFavorite)
            {
                btnStar.SetImage(UIImage.FromBundle("rating_star_gold"), UIControlState.Normal);
            }
            else
            {
                btnStar.SetImage(UIImage.FromBundle("rating_star_grey"), UIControlState.Normal);
            }
            CABasicAnimation scaleAnimation = AnimateKeyPath("transform.scale",
                                                                   0.0001f,
                                                                   1.0f,
                                                                   CAMediaTimingFunction.EaseIn);

            scaleAnimation.Duration = 0.25f;
            StarShape.AddAnimation(scaleAnimation, "scaleUp");
        }

        partial void btnPlay_TouchUpInside(NSObject sender)
        {
            isPlaying = !isPlaying;
            PlayShape.FillColor = UIColor.LightGray.CGColor;
            if (isPlaying)
            {
                PlayRingtone();
            }
            else
            {
                StopRingtone();
            }
            CABasicAnimation scaleAnimation = AnimateKeyPath("transform.scale",
                                                                   0.0001f,
                                                                   1.0f,
                                                                   CAMediaTimingFunction.EaseIn);

            scaleAnimation.Duration = 0.25f;
            PlayShape.AddAnimation(scaleAnimation, "scaleUp");
        }

        public override void AwakeFromNib()
        {

            base.AwakeFromNib();
            imgWord.Layer.CornerRadius = 4;
            StarShape = new CAShapeLayer();
            PlayShape = new CAShapeLayer();
            CellShape = new CAShapeLayer();

            InitShapeForLayer(new[] { StarShape, PlayShape, CellShape });

            AnimationDelegate = new ButtonPressAnimationDelegate();
            AnimationDelegate.AnimationDidStartFunc = () =>
            {
                AnimationDidStartFunc?.Invoke();
            };
            AnimationDelegate.AnimationDidStopFunc = (finished) =>
            {
                StarShape.FillColor = UIColor.Clear.CGColor;
                PlayShape.FillColor = UIColor.Clear.CGColor;
                CellShape.FillColor = UIColor.Clear.CGColor;
                AnimationDidStopFunc?.Invoke(finished);
            };

            var cellgesture = new UITapGestureRecognizer((UITapGestureRecognizer obj) =>
            {
                var touchLocation = obj.LocationInView(ContentView);
                nfloat x, y, radius;
                if (touchLocation.X < ContentView.Center.X)
                {
                    radius = ContentView.Frame.Size.Width - touchLocation.X;
                    x = -(radius - touchLocation.X);
                    y = -(radius - touchLocation.Y);
                }
                else
                {
                    radius = touchLocation.X;
                    x = 0;
                    y = -(radius - touchLocation.Y);
                }
                var yBound = (nfloat)Math.Max(touchLocation.Y, 60 - touchLocation.Y);
                CellShape.Frame = new CGRect(x, y, radius * 2, radius * 2);
                CellShape.Path = UIBezierPath.FromOval(new CGRect(0, 0, radius * 2, radius * 2)).CGPath;
                CellShape.FillColor = UIColor.LightGray.CGColor;
                CABasicAnimation scaleAnimation = AnimateKeyPath("transform.scale",
                                                                           0.0001f,
                                                                           1.0f,
                                                                           CAMediaTimingFunction.EaseIn);

                scaleAnimation.Duration = 0.25f;
                CellShape.AddAnimation(scaleAnimation, "scaleUp");

            });
            ContentView.AddGestureRecognizer(cellgesture);
        }

        void InitShapeForLayer(CAShapeLayer[] shapes)
        {
            foreach (var shape in shapes)
            {
                ContentView.Layer.InsertSublayer(shape, 0);
                shape.AnchorPoint = new CGPoint(0.5, 0.5);
                shape.MasksToBounds = true;
                shape.FillColor = UIColor.Clear.CGColor;
            }

            StarShape.Frame = new CGRect(btnStar.Center.X - 38, btnStar.Center.Y - 18, 36, 36);
            StarShape.Path = UIBezierPath.FromOval(new CGRect(0, 0, 36, 36)).CGPath;
            PlayShape.Frame = new CGRect(btnPlay.Center.X - 34, btnPlay.Center.Y - 18, 36, 36);
            PlayShape.Path = UIBezierPath.FromOval(new CGRect(0, 0, 36, 36)).CGPath;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
        }

        CABasicAnimation AnimateKeyPath(string keyPath, nfloat from, nfloat to, string timing)
        {
            CABasicAnimation animation = CABasicAnimation.FromKeyPath(keyPath);
            animation.From = NSNumber.FromNFloat(from);
            animation.To = NSNumber.FromNFloat(to);
            animation.RepeatCount = 1;
            animation.TimingFunction = CAMediaTimingFunction.FromName((NSString)timing);
            animation.RemovedOnCompletion = false;
            animation.FillMode = CAFillMode.Forwards;
            animation.Duration = AnimationDuration;
            animation.Delegate = AnimationDelegate;
            return animation;
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



    public class ButtonPressAnimationDelegate : CAAnimationDelegate
    {
        public Action AnimationDidStartFunc;
        public Action<bool> AnimationDidStopFunc;

        [Export("animationDidStart:"),]
        public override void AnimationStarted(CAAnimation anim)
        {
            AnimationDidStartFunc?.Invoke();
        }

        [Export("animationDidStop:finished:"),]
        public override void AnimationStopped(CAAnimation anim, bool finished)
        {
            AnimationDidStopFunc?.Invoke(finished);
        }
    }
}       
