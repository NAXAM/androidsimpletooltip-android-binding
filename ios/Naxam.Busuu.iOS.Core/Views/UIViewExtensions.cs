using System;
using CoreGraphics;
using UIKit;

namespace Naxam.Busuu.iOS.Core.Views
{
    public static class UIViewExtensions
    {
        public static void AddRippleLayer(this UIView view, int index = 0) {
            //var shape = ...;
        }

        public static void StartRippleAnimation(this UIView view, CGPoint? startPoint = null) {
            var point = startPoint ?? view.Center;
            //
        }
    }
}
