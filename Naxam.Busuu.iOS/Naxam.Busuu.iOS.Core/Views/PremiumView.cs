using Foundation;
using System;
using UIKit;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Learning.ViewModel;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;

namespace Naxam.Busuu.iOS.Learning
{
    public partial class PremiumView : MvxViewController<PremiumViewModel>
    {
        public PremiumView(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
    }
}