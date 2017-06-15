using Android.App;
using Android.Widget;
using Android.OS;
using Naxam.Busuu.Droid.Profile.Views;

namespace Naxam.Busuu.Droid
{
    [Activity(Label = "Naxam.Busuu.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            StartActivity(new Android.Content.Intent(this,typeof(BuyPremiumActivity)));
        }
    }
}

