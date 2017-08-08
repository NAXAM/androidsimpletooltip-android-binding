using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Naxam.Busuu.Droid.Social.Views
{
    [Activity(Label = "Social", Theme = "@style/AppTheme", MainLauncher = true)]

    public class SocialView : MvxAppCompatActivity
    {
        public static int ResultCodeFilter = 100;
        public static string ShowWriting = "ShowWriting";
        public static string ShowSpeaking = "ShowSpeaking";
        public static string FilterLanguage = "FilterLanguage";
        bool showWriting = true;
        bool showSpeaking = true;
        bool filterLanguage = true;
        private TabLayout tabs;
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.activity_social);
            tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            tabs.AddTab(tabs.NewTab().SetText("Discover"));
            tabs.AddTab(tabs.NewTab().SetText("Friends"));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_social, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menu_filter)
            {
                Intent intent = new Intent(this, typeof(FilterView));
                intent.PutExtra(ShowWriting, showWriting);
                intent.PutExtra(ShowSpeaking, showSpeaking);
                intent.PutExtra(FilterLanguage, filterLanguage);
                StartActivityForResult(intent, ResultCodeFilter);
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == ResultCodeFilter && resultCode == Result.Ok)
            {
                showWriting = data.GetBooleanExtra(ShowWriting, true);
                showSpeaking = data.GetBooleanExtra(ShowSpeaking, true);
                filterLanguage = data.GetBooleanExtra(FilterLanguage, true);
            }
        }
    }
}