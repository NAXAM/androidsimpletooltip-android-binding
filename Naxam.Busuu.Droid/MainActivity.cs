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
using Naxam.Busuu.Droid.Profile.Views;
using Android.Support.V7.App;
using Android.Support.V4.Content;
using Android.Graphics;

namespace Naxam.Busuu.Droid
{
    [Activity(Label = "MainActivity", MainLauncher = true, SupportsPictureInPicture = true,AllowEmbedded =true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
           StartActivity(new Intent(this, typeof(LoginView)));


        }
    }
}