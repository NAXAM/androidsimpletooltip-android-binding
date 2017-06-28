// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social
{
	[Register ("DiscoverCell")]
	partial class DiscoverCell
	{
		[Outlet]
		public UIKit.UILabel Country { get; private set; }

		[Outlet]
		UIKit.UIImageView ImageLan { get; set; }

		[Outlet]
		public UIKit.UIImageView ImageUser { get; private set; }

		[Outlet]
		public UIKit.UIImageView ImgSpeak { get; private set; }

		[Outlet]
		public UIKit.UILabel NameUser { get; private set; }

		[Outlet]
		UIKit.UISlider SliderSpeak { get; set; }

		[Outlet]
		public UIKit.UILabel TextLan { get; private set; }

		[Outlet]
		public UIKit.UIView ViewCell { get; private set; }

		[Outlet]
		public UIKit.UIView ViewHome { get; private set; }

		[Outlet]
		public UIKit.UIView ViewLan { get; private set; }

		[Outlet]
		public UIKit.UIView ViewSpeak { get; private set; }

		[Outlet]
		public UIKit.UILabel WriteLabel { get; private set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Country != null) {
				Country.Dispose ();
				Country = null;
			}

			if (ImageLan != null) {
				ImageLan.Dispose ();
				ImageLan = null;
			}

			if (ImageUser != null) {
				ImageUser.Dispose ();
				ImageUser = null;
			}

			if (ImgSpeak != null) {
				ImgSpeak.Dispose ();
				ImgSpeak = null;
			}

			if (NameUser != null) {
				NameUser.Dispose ();
				NameUser = null;
			}

			if (TextLan != null) {
				TextLan.Dispose ();
				TextLan = null;
			}

			if (ViewCell != null) {
				ViewCell.Dispose ();
				ViewCell = null;
			}

			if (ViewHome != null) {
				ViewHome.Dispose ();
				ViewHome = null;
			}

			if (ViewLan != null) {
				ViewLan.Dispose ();
				ViewLan = null;
			}

			if (ViewSpeak != null) {
				ViewSpeak.Dispose ();
				ViewSpeak = null;
			}

			if (WriteLabel != null) {
				WriteLabel.Dispose ();
				WriteLabel = null;
			}

			if (SliderSpeak != null) {
				SliderSpeak.Dispose ();
				SliderSpeak = null;
			}
		}
	}
}
