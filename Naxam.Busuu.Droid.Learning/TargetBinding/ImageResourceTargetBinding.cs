using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Target;
using Java.Lang;
using MvvmCross.Binding;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load.Engine;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class ImageResourceTargetBinding : MvxAndroidTargetBinding
    {
        public ImageResourceTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(ImageView);

        protected override void SetValueImpl(object target, object value)
        {
            ImageView view = (ImageView)target;
            Glide.With(view.Context)
                .Load(Integer.ParseInt(value.ToString()))
                .DiskCacheStrategy(DiskCacheStrategy.All)
                .Into(view);
        }
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

    }
}