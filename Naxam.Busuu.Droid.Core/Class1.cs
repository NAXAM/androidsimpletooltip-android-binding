using Android.Widget;
using MvvmCross.Binding.Droid.Target;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmCross.Binding;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load.Resource.Bitmap;
using Android.Content;
using Com.Bumptech.Glide.Load.Engine.Bitmap_recycle;
using Android.Graphics;

namespace Naxam.Busuu.Droid.Core
{
    public class GlideImageViewTartgetBinding : MvxAndroidTargetBinding
    {
        public GlideImageViewTartgetBinding(object target) : base(target)
        {
        }

        public override Type TargetType => typeof(ImageView);

        protected override void SetValueImpl(object target, object value)
        {
            var imageView = (ImageView)target;

            Glide.With(imageView.Context)
                .Load((string)value)
                .Transform(new CircleTransform(imageView.Context))
                .Into(imageView);
        }

        public override MvxBindingMode DefaultMode =>  MvxBindingMode.OneTime;
    }


    public class CircleTransform : BitmapTransformation
    {
        public override string Id
        {
            get { return typeof(CircleTransform).Name; }
        }

        public CircleTransform(Context context) : base(context)
        {
        }

        protected override Bitmap Transform(IBitmapPool p0, Bitmap p1, int p2, int p3)
        {
            return CircleCrop(p0, p1);
        }
        private static Bitmap CircleCrop(IBitmapPool pool, Bitmap source)
        {
            if (source == null) return null;

            int size = Math.Min(source.Width, source.Height);
            int x = (source.Width - size) / 2;
            int y = (source.Height - size) / 2;

            // TODO this could be acquired from the pool too
            Bitmap squared = Bitmap.CreateBitmap(source, x, y, size, size);

            Bitmap result = pool.Get(size, size, Bitmap.Config.Argb8888);
            if (result == null)
            {
                result = Bitmap.CreateBitmap(size, size, Bitmap.Config.Argb8888);
            }

            Canvas canvas = new Canvas(result);
            Paint paint = new Paint();
            paint.SetShader(new BitmapShader(squared, BitmapShader.TileMode.Clamp, BitmapShader.TileMode.Clamp));
            paint.AntiAlias = (true);
            float r = size / 2f;
            canvas.DrawCircle(r, r, r, paint);
            return result;
        }
    }
}
