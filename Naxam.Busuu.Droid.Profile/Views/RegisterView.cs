using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Views.Animations;
using Android.Content.Res;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "Create Account")]
    public class RegisterView : MvxAppCompatActivity
    {
        EditText edtEmail, edtUserName, edtPassword, edtPhone;
        LinearLayout layoutPhone, layoutSocial;
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.RegisterActivity);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            Button btnButtonUsePhoneEmail = FindViewById<Button>(Resource.Id.btnButtonUsePhoneEmail);
            edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            edtUserName = FindViewById<EditText>(Resource.Id.edtUserName);
            edtPassword = FindViewById<EditText>(Resource.Id.edtPassword);
            edtPhone = FindViewById<EditText>(Resource.Id.edtPhone);
            layoutPhone = FindViewById<LinearLayout>(Resource.Id.layoutPhone);
            layoutSocial = FindViewById<LinearLayout>(Resource.Id.layoutSocial);
            layoutPhone.SetBackgroundResource(Resource.Drawable.underline_background_focus);


            //animation
            Animation fadeIn = new AlphaAnimation(0, 1);
            fadeIn.Duration = 200;
            Animation moveLefttoRight = new TranslateAnimation(100, 0, 0, 0);
            moveLefttoRight.Duration = 200;
            moveLefttoRight.FillAfter = true;


            AnimationSet animation = new AnimationSet(false);
            animation.AddAnimation(fadeIn);
            animation.AddAnimation(moveLefttoRight);

            edtPhone.FocusChange += (s, e) =>
            {
                if (e.HasFocus)
                {
                    layoutPhone.SetBackgroundResource(Resource.Drawable.underline_background_focus);
                    layoutSocial.Visibility = ViewStates.Gone;
                }
                else
                {
                    layoutPhone.SetBackgroundResource(Resource.Drawable.underline_background_normal);
                    layoutSocial.Visibility = ViewStates.Visible;
                }
            };

            edtEmail.FocusChange += EdtEmail_FocusChange;
            edtUserName.FocusChange += EdtEmail_FocusChange;
            edtPassword.FocusChange += EdtEmail_FocusChange;
            edtPhone.FocusChange += EdtEmail_FocusChange;
            btnButtonUsePhoneEmail.Click += (s, e) =>
            {
                if (edtEmail.Visibility == ViewStates.Gone)
                {
                    edtEmail.StartAnimation(animation);
                    edtEmail.Visibility = ViewStates.Visible;
                    layoutPhone.Visibility = ViewStates.Gone;
                    btnButtonUsePhoneEmail.Text = "Use Phone";
                    edtEmail.RequestFocus();
                }
                else
                {
                    edtEmail.Visibility = ViewStates.Gone;
                    layoutPhone.StartAnimation(animation);
                    layoutPhone.Visibility = ViewStates.Visible;
                    btnButtonUsePhoneEmail.Text = "Use Email";
                    edtPhone.RequestFocus();
                }

            };

        }

        private void EdtEmail_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (edtUserName.HasFocus || edtPhone.HasFocus || edtPassword.HasFocus || edtEmail.HasFocus)
            {
                layoutSocial.Visibility = ViewStates.Gone;
            }
            else
            {
                layoutSocial.Visibility = ViewStates.Visible;
            }
        }
    }
}