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
using Android.Graphics;
using Android.Graphics.Drawables;
using static Android.Resource;
using Android.Views.Animations;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "LoginPageActivity")]
    public class LoginView : MvxAppCompatActivity
    {
        private bool isClickLoginBtn; 
        private bool isClickGoogleBtn;
        EditText edtEmail, edtPassword;
        TextView txtForgotPass;
        Button btnFB, btnGoogle, btnLogin; 


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginPage);
            InitActivity();
        }

        private void InitActivity()
        {
            txtForgotPass = FindViewById<TextView>(Resource.Id.txtForgotPass);
            isClickLoginBtn = true;
            edtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            edtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnFB = FindViewById<Button>(Resource.Id.btnFb);
            btnGoogle = FindViewById<Button>(Resource.Id.btnGoogle);
            btnGoogle.Focusable = true;
            btnGoogle.FocusableInTouchMode = true;
            btnGoogle.RequestFocus();
            btnLogin.Focusable = true;
            btnLogin.FocusableInTouchMode = true;
            edtPassword.FocusChange += EditText_FocusChange;
            edtEmail.FocusChange += EditText_FocusChange;
            btnGoogle.Click += BtnGoogle_Click;
            btnLogin.Click += BtnLogin_Click;
        }

        private void EditText_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            LinearLayout parent = (LinearLayout)btnFB.Parent;
            if (edtEmail.IsFocused || edtPassword.IsFocused)
            {
                if (parent.Visibility != ViewStates.Gone)
                {
                    parent.Visibility = ViewStates.Gone;
                }

            }
            if (edtEmail.IsFocused == false && edtPassword.IsFocused == false)
            {
                parent.Visibility = ViewStates.Visible;
                // Check if no view has focus: 
                InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(edtPassword.WindowToken, 0);
            }
        }

        private void BtnGoogle_Click(object sender, EventArgs e)
        {

            if (isClickGoogleBtn)
            {
                //btnGoogle.SetBackgroundResource(Resource.Drawable.circle_drawable_login_click);
                isClickGoogleBtn = !isClickGoogleBtn;
            }
            else
            {
                //btnGoogle.SetBackgroundResource(Resource.Drawable.circle_drablelogin);
                isClickGoogleBtn = !isClickGoogleBtn;
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (isClickLoginBtn)
            {
              //  btnLogin.SetBackgroundResource(Resource.Drawable.circle_drawable_login_click);
                isClickLoginBtn = !isClickLoginBtn;
            }
            else
            {
               // btnLogin.SetBackgroundResource(Resource.Drawable.circle_drablelogin);
                isClickLoginBtn = !isClickLoginBtn;
            }
        }
    }
}