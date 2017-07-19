using System;
using Android.Animation;
using static Android.Animation.ValueAnimator;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class AnimatorUpdateListener : Java.Lang.Object, IAnimatorUpdateListener
    {
        Action<ValueAnimator> AnimationUpdate;
        public AnimatorUpdateListener(Action<ValueAnimator> AnimationUpdate)
        {
            this.AnimationUpdate = AnimationUpdate;
        }
        public void OnAnimationUpdate(ValueAnimator animation)
        {
            AnimationUpdate?.Invoke(animation);
        }
    }
}