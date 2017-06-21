using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Review.ViewModels;
using UIKit;

namespace Naxam.Busuu.iOS.Review.Views
{
    public partial class ReviewAllView : MvxViewController<ReviewAllViewModel>
    {
        public ReviewAllView() : base("ReviewAllView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            this.CreateBinding(SubTotalTextField).To((ReviewAllViewModel vm) => vm.SubTotal).Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

