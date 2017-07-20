using Foundation;
using System;
using UIKit;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Core.ViewModels;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;

namespace Naxam.Busuu.iOS.Core
{
    [MvxFromStoryboard(StoryboardName = "Core")]
    public partial class PremiumView : MvxViewController<PremiumViewModel>
    {
        public PremiumView(IntPtr handle) : base(handle)
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

            UIBarButtonItem backButton = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null, null);
            NavigationItem.BackBarButtonItem = backButton;
            NavigationController.NavigationBar.TopItem.BackBarButtonItem = backButton;

            NavigationItem.Title = "Premium";
            var discount = this.ViewModel.Discount;
            if (discount !=0)
            {
                var uiviewHeader = new UIView();
                var label1 = new UILabel();
                label1.Frame = new CGRect((View.Bounds.Size.Width - 250) / 2, 16, 250, 50);
                label1.Text = this.ViewModel.AdText[0];
                label1.TextAlignment = UITextAlignment.Center;
                label1.Font = UIFont.FromName("HelveticaNeue-Medium", 14);
                label1.Lines = 0;
                label1.SizeToFit();
                label1.Frame = new CGRect((View.Bounds.Size.Width - 250) / 2, 16, 250, label1.Frame.Height);

                var label2 = new UILabel();
                label2.Frame = new CGRect((View.Bounds.Size.Width - 250) / 2, label1.Frame.Height + 32, 250, 50);
				label2.Text = this.ViewModel.AdText[1];
				label2.TextAlignment = UITextAlignment.Center;
                label2.Font = UIFont.FromName("HelveticaNeue-Medium", 14);
                label2.Lines = 0;
                label2.SizeToFit();
                label2.Frame = new CGRect((View.Bounds.Size.Width - 250) / 2, label1.Frame.Height + 32, 250, label2.Frame.Height);

                uiviewHeader.AddSubviews(new[]{label1, label2});
                uiviewHeader.Frame = new CGRect(0,0,View.Bounds.Size.Width, label1.Frame.Height + label2.Frame.Height + 48);
                FeatureTableView.TableHeaderView = uiviewHeader;
            } 
            FeatureTableView.ReloadData();
        }
    }
}