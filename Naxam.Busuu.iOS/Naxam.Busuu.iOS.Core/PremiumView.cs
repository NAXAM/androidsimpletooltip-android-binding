using Foundation;
using System;
using UIKit;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Core.ViewModels;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;

namespace Naxam.Busuu.iOS.Core
{
    [MvxFromStoryboard(StoryboardName = "Core")]
    public partial class PremiumView : MvxViewController<PremiumViewModel>
    {
        public PremiumView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var source = new MvxStandardTableViewSource(FeatureTableView, (NSString)"premiumCell");
            FeatureTableView.Source = source;
            var setBinding = this.CreateBindingSet<PremiumView, PremiumViewModel>();
            setBinding.Bind(source).To(vm => vm.Features);
            setBinding.Apply();
        }
    }
}