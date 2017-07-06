// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Naxam.Busuu.iOS.Social.Cells
{
    [Register ("FriendsCell")]
    partial class FriendsCell
    {
        [Outlet]
        UIKit.NSLayoutConstraint audioViewBottomConstraint { get; set; }

        [Outlet]
        UIKit.NSLayoutConstraint audioViewTopConstraint { get; set; }

        [Outlet]
        UIKit.UIButton ButtonAudioPlay { get; set; }

        [Outlet]
        UIKit.UIImageView imgLan { get; set; }

        [Outlet]
        UIKit.UIImageView imgUserAvatar { get; set; }

        [Outlet]
        UIKit.UILabel lblCountry { get; set; }

        [Outlet]
        UIKit.UILabel lblTime { get; set; }

        [Outlet]
        UIKit.UILabel lblTimePublic { get; set; }

        [Outlet]
        UIKit.UILabel lblUserName { get; set; }

        [Outlet]
        UIKit.UISlider SliderSpeak { get; set; }

        [Outlet]
        UIKit.UILabel textLan { get; set; }

        [Outlet]
        UIKit.UIView ViewAudioPlayer { get; set; }

        [Outlet]
        UIKit.UILabel WriteText { get; set; }
       
        [Action ("ButtonAudioPlay_TouchUpInside:")]
        partial void ButtonAudioPlay_TouchUpInside (Foundation.NSObject sender);

        [Action ("ButtonRate_TouchUpInside:")]
        partial void ButtonRate_TouchUpInside (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (imgLan != null) {
                imgLan.Dispose ();
                imgLan = null;
            }

            if (imgUserAvatar != null) {
                imgUserAvatar.Dispose ();
                imgUserAvatar = null;
            }

            if (lblCountry != null) {
                lblCountry.Dispose ();
                lblCountry = null;
            }

            if (lblUserName != null) {
                lblUserName.Dispose ();
                lblUserName = null;
            }

            if (textLan != null) {
                textLan.Dispose ();
                textLan = null;
            }

            if (lblTimePublic != null) {
                lblTimePublic.Dispose ();
                lblTimePublic = null;
            }

            if (ButtonAudioPlay != null) {
                ButtonAudioPlay.Dispose ();
                ButtonAudioPlay = null;
            }

            if (SliderSpeak != null) {
                SliderSpeak.Dispose ();
                SliderSpeak = null;
            }

            if (lblTime != null) {
                lblTime.Dispose ();
                lblTime = null;
            }

            if (WriteText != null) {
                WriteText.Dispose ();
                WriteText = null;
            }

            if (audioViewTopConstraint != null) {
                audioViewTopConstraint.Dispose ();
                audioViewTopConstraint = null;
            }

            if (audioViewBottomConstraint != null) {
                audioViewBottomConstraint.Dispose ();
                audioViewBottomConstraint = null;
            }

            if (ViewAudioPlayer != null) {
                ViewAudioPlayer.Dispose ();
                ViewAudioPlayer = null;
            }
        }
    }
}
