// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social.Views
{
	[Register ("CommentView")]
	partial class CommentView
	{
		[Outlet]
		UIKit.NSLayoutConstraint audioViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint audioViewTopConstraint { get; set; }

		[Outlet]
		UIKit.UIButton btnAudioPlay { get; set; }

		[Outlet]
		UIKit.UIButton btnSay { get; set; }

		[Outlet]
		UIKit.UITextField fieldCorrect { get; set; }

		[Outlet]
		UIKit.UIImageView imgCircle { get; set; }

		[Outlet]
		UIKit.UIImageView imgQuestion { get; set; }

		[Outlet]
		UIKit.UILabel lblTime { get; set; }

		[Outlet]
		UIKit.UISlider SliderSpeak { get; set; }

		[Outlet]
		UIKit.UILabel textHowDid { get; set; }

		[Outlet]
		UIKit.UILabel textQuestion { get; set; }

		[Outlet]
		UIKit.UIView ViewAudioPlayer { get; set; }

		[Outlet]
		UIKit.UIView ViewBossQuestion { get; set; }

		[Outlet]
		UIKit.UIView ViewForSpeak { get; set; }

		[Outlet]
		UIKit.UIView ViewForWrite { get; set; }

		[Outlet]
		UIKit.UIView ViewQuestion { get; set; }

		[Outlet]
		UIKit.UIView ViewShadow { get; set; }

		[Outlet]
		UIKit.UIView ViewStar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (audioViewBottomConstraint != null) {
				audioViewBottomConstraint.Dispose ();
				audioViewBottomConstraint = null;
			}

			if (audioViewTopConstraint != null) {
				audioViewTopConstraint.Dispose ();
				audioViewTopConstraint = null;
			}

			if (btnAudioPlay != null) {
				btnAudioPlay.Dispose ();
				btnAudioPlay = null;
			}

			if (btnSay != null) {
				btnSay.Dispose ();
				btnSay = null;
			}

			if (fieldCorrect != null) {
				fieldCorrect.Dispose ();
				fieldCorrect = null;
			}

			if (imgQuestion != null) {
				imgQuestion.Dispose ();
				imgQuestion = null;
			}

			if (lblTime != null) {
				lblTime.Dispose ();
				lblTime = null;
			}

			if (SliderSpeak != null) {
				SliderSpeak.Dispose ();
				SliderSpeak = null;
			}

			if (textHowDid != null) {
				textHowDid.Dispose ();
				textHowDid = null;
			}

			if (textQuestion != null) {
				textQuestion.Dispose ();
				textQuestion = null;
			}

			if (ViewAudioPlayer != null) {
				ViewAudioPlayer.Dispose ();
				ViewAudioPlayer = null;
			}

			if (ViewBossQuestion != null) {
				ViewBossQuestion.Dispose ();
				ViewBossQuestion = null;
			}

			if (ViewForSpeak != null) {
				ViewForSpeak.Dispose ();
				ViewForSpeak = null;
			}

			if (ViewForWrite != null) {
				ViewForWrite.Dispose ();
				ViewForWrite = null;
			}

			if (ViewQuestion != null) {
				ViewQuestion.Dispose ();
				ViewQuestion = null;
			}

			if (ViewShadow != null) {
				ViewShadow.Dispose ();
				ViewShadow = null;
			}

			if (ViewStar != null) {
				ViewStar.Dispose ();
				ViewStar = null;
			}

			if (imgCircle != null) {
				imgCircle.Dispose ();
				imgCircle = null;
			}
		}
	}
}
