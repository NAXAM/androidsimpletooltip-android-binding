// This file has been autogenerated from a class added in the UI designer.

using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Naxam.Busuu.iOS.Core.Converter;
using Naxam.Busuu.Learning.Model;
using UIKit;

namespace Naxam.Busuu.iOS.Learning
{
    public partial class SubLessonTableViewCell : MvxTableViewCell
    {
        public SubLessonTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
          {
              var bindingSet = this.CreateBindingSet<SubLessonTableViewCell, TopicModel>();
              bindingSet.Bind(lbTime).To(m => m.Time);
              bindingSet.Bind(lbTitle).To(m => m.Toppic);
              bindingSet.Bind(ContentView.Layer).For("LayerBackgroundColor").To(m => m.Color).WithConversion(new HexToBrighterUIColorValueConverter(), null);
              bindingSet.Bind(exerciseView).For("ExcersizeImageView").To(m=>m.Exercises);
               bindingSet.Apply();
          });
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }
    }
}
