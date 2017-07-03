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
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load.Engine;
using MvvmCross.Binding;
using Android.Support.V7.Widget;

namespace Naxam.Busuu.Droid.Learning.TargetBinding
{
    public class ImageSourceTargetBinding : MvxAndroidTargetBinding
    {
        public ImageSourceTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(ImageView);

        protected override void SetValueImpl(object target, object value)
        {
            ImageView view = (ImageView)target;
            GridLayoutManager grid = new GridLayoutManager(null, 1);
           
            Glide.With(view.Context)
                .Load(value.ToString())
                .Transform(new CircleTransform(view.Context))
                .Into(view);
        }

        private void Lst_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
             
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneTime;
    }
}