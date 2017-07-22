// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using MvvmCross.iOS.Views;
using Naxam.Busuu.Profile.ViewModel;
using UIKit;

namespace Naxam.Busuu.iOS.Profile.Views
{
	[MvxFromStoryboard(StoryboardName = "Profile")]
	public partial class RegisterView : MvxViewController<RegisterViewModel>
	{
		public RegisterView (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			this.NavigationController.NavigationBarHidden = false;

			ViewShadow.Layer.ShadowRadius = 2;
			ViewShadow.Layer.ShadowOpacity = 0.25f;
			ViewShadow.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 2);

			btnFacebook.ContentEdgeInsets = new UIEdgeInsets(0, 16, 0, 0);
			btnGoogle.ContentEdgeInsets = new UIEdgeInsets(0, 16, 0, 0);

			btnFacebook.Layer.CornerRadius = btnFacebook.Frame.Height / 2;
			btnGoogle.Layer.CornerRadius = btnGoogle.Frame.Height / 2;

			viewbtnFacebook.Layer.CornerRadius = viewbtnFacebook.Frame.Height / 2;
			viewbtnGoogle.Layer.CornerRadius = viewbtnGoogle.Frame.Height / 2;

			viewbtnFacebook.Layer.ShadowRadius = 1;
			viewbtnFacebook.Layer.ShadowOpacity = 0.25f;
			viewbtnFacebook.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 1);

			viewbtnGoogle.Layer.ShadowRadius = 1;
			viewbtnGoogle.Layer.ShadowOpacity = 0.25f;
			viewbtnGoogle.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 1);

			btnLogin.Layer.CornerRadius = btnLogin.Frame.Height / 2;
        }
	}
}
