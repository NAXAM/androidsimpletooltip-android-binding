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

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "LoginPageActivity")]
    public class LoginPageActivity : Activity
    {
        private bool isClickLoginBtn;
        Button btnFB, btnGoogle, btnLogin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginPage);
            InitActivity();

        }

        private void InitActivity()
        {
            isClickLoginBtn = false;
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if(isClickLoginBtn){
                btnLogin.SetBackgroundResource(Resource.Drawable.circle_drawable_login_click);
                isClickLoginBtn = !isClickLoginBtn;
            }
            else
            {
                btnLogin.SetBackgroundResource(Resource.Drawable.circle_drablelogin);
                isClickLoginBtn = !isClickLoginBtn;
            }
        }
    }
}