// This file has been autogenerated from a class added in the UI designer.

using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Naxam.Busuu.iOS.Profile.Common;
using Naxam.Busuu.Profile.ViewModel;
using UIKit;

namespace Naxam.Busuu.iOS.Profile.Views
{
    [MvxFromStoryboard(StoryboardName = "Profile")]
    public partial class ChooseLanguageView : MvxViewController<ChooseLanguageViewModel>
	{
		public ChooseLanguageView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationController.NavigationBarHidden = false;

			var lSource = new LanguageTableViewSource(LanguageTableView);

            var setBinding = this.CreateBindingSet<ChooseLanguageView, ChooseLanguageViewModel>();
			setBinding.Bind(lSource).To(vm => vm.Languages);
			//setBinding.Bind(lSource).For(vm => vm.SelectionChangedCommand).To(vm => vm.SelectedLanguage);
			setBinding.Apply();

            LanguageTableView.Source = lSource;

            LanguageTableView.Layer.CornerRadius = 5;
            ViewForTableView.Layer.CornerRadius = 5;
			ViewForTableView.Layer.ShadowOpacity = 0.35f;
			ViewForTableView.Layer.ShadowOffset = new CGSize(0, 2);
            ViewForTableView.Layer.MasksToBounds = false;
		}
	}
}
