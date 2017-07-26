using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Naxam.Busuu.iOS.Core.Views
{
    public class RippleLayer : CAShapeLayer
    {
        struct ScaleType
        {
            public static string Scale = "transform.scale";
            public static string Up = "scaleUp";
            public static string Down = "scaleDown";
        }



        double X, Y, Radius;
        CGPoint touchLocation;
        public RippleLayer() : base()
        {
        }

        public RippleLayer(UIView parentView, UIColor animateColor, UIColor finishColor) : base()
        {
            ParentView = parentView;
            AnimateColor = animateColor;
            FinishColor = finishColor;
            InitShapeLayer();
        }

        public void WillAnimateTapGesture(CGPoint touch)
        {
            touchLocation = touch;
            Radius = 2 * Math.Max(ParentView.Bounds.Size.Width, ParentView.Bounds.Size.Height);
            X = touch.X - Radius;
            Y = touch.Y - Radius;
			Frame = new CGRect(X, Y, Radius * 2, Radius * 2);
			Path = UIBezierPath.FromOval(new CGRect(0, 0, Radius * 2, Radius * 2)).CGPath;
			WillAnimate();
        }

        public double AnimationDuration = 0.25f;

        public Action AnimationDidStartFunc;
        public Action<bool> AnimationDidStopFunc;

        public UIColor FinishColor { get; set; }
        public UIColor AnimateColor { get; set; }

        public SpreadOutAnimationViewDelegate AnimationDelegate { get; set; }

        public UIView ParentView
        {
            get;
            set;
        }

        public void InitShapeLayer()
        {
            ParentView.Layer.InsertSublayer(this, 0);
            AnchorPoint = new CGPoint(0.5, 0.5);
            MasksToBounds = true;
            FillColor = UIColor.Clear.CGColor;

            AnimationDelegate = new SpreadOutAnimationViewDelegate();
            AnimationDelegate.AnimationDidStartFunc = () =>
           {
               AnimationDidStartFunc?.Invoke();
               FillColor = AnimateColor.CGColor;
           };
            AnimationDelegate.AnimationDidStopFunc = (finished) =>
            {
                ParentView.Layer.BackgroundColor = FinishColor.CGColor;
                AnimationDidStopFunc?.Invoke(finished);
            };
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

        public void WillAnimate()
        {
            CABasicAnimation scaleAnimation = AnimateKeyPath("transform.scale",
                                                                   0.0001f,
                                                                   1.0f,
                                                                   CAMediaTimingFunction.EaseIn);

            scaleAnimation.Duration = AnimationDuration;
            AddAnimation(scaleAnimation, "scaleUp");
        }

    }


    public class SpreadOutAnimationViewDelegate : CAAnimationDelegate
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
