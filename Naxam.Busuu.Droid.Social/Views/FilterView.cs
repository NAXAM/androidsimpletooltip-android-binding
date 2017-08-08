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
using Android.Graphics;

namespace Naxam.Busuu.Droid.Social.Views
{
    [Activity(Label = "Filter", Theme = "@style/AppTheme.NoActionBar", ParentActivity = typeof(SocialView))]
    public class FilterView : MvxAppCompatActivity
    {
        bool showWriting = true, showSpeaking = true, filterLanguage = true, visibleDone;
        Android.Support.V7.Widget.Toolbar toolbar;
        Switch swtShowWriting, swtShowSpeaking, swtFilterLanguage;
        TextView txtShowWriting, txtShowSpeaking, txtDone;
        Intent intent = new Intent();

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.activity_filter);
            showWriting = Intent.GetBooleanExtra(SocialView.ShowWriting, true);
            showSpeaking = Intent.GetBooleanExtra(SocialView.ShowSpeaking, true);
            filterLanguage = Intent.GetBooleanExtra(SocialView.FilterLanguage, true);
            swtShowSpeaking = FindViewById<Switch>(Resource.Id.swtShowSpeaking);
            swtShowWriting = FindViewById<Switch>(Resource.Id.swtShowWriting);
            swtFilterLanguage = FindViewById<Switch>(Resource.Id.swtLanguage);
            txtShowWriting = FindViewById<TextView>(Resource.Id.txtShowWriting);
            txtShowSpeaking = FindViewById<TextView>(Resource.Id.txtShowSpeaking);
            txtDone = FindViewById<TextView>(Resource.Id.txtDone);
            ImageView imgFlag = FindViewById<ImageView>(Resource.Id.imgFlag);
            imgFlag.SetImageResource(Resource.Drawable.flag_small_english);
            txtDone.Visibility = ViewStates.Gone;
            txtDone.Click += TxtDone_Click;
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitleTextColor(Color.White);
            toolbar.SetSubtitleTextColor(Color.White);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
         
            swtShowSpeaking.Checked = showSpeaking;
            
            swtShowWriting.Checked = showWriting;
            swtFilterLanguage.Checked = filterLanguage;

            swtShowSpeaking.CheckedChange += SwtShowSpeaking_CheckedChange;
            swtFilterLanguage.CheckedChange += SwtShowSpeaking_CheckedChange;
            swtShowWriting.CheckedChange += SwtShowSpeaking_CheckedChange;
            txtShowWriting.Click += TxtShowWriting_Click;
            txtShowSpeaking.Click += TxtShowSpeaking_Click;
             
        }

        private void TxtDone_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void SwtShowSpeaking_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            txtDone.Visibility = CheckDone() ? ViewStates.Visible : ViewStates.Gone;
            intent.PutExtra(SocialView.ShowSpeaking, swtShowSpeaking.Checked);
            intent.PutExtra(SocialView.ShowWriting, swtShowWriting.Checked);
            intent.PutExtra(SocialView.FilterLanguage, swtFilterLanguage.Checked);
            SetResult(Result.Ok, intent);
        }

         
        private void TxtShowSpeaking_Click(object sender, EventArgs e)
        {
            swtShowSpeaking.Checked = !swtShowSpeaking.Checked;
        }

        private void TxtShowWriting_Click(object sender, EventArgs e)
        {
            swtShowWriting.Checked = !swtShowWriting.Checked; 
        }

        private bool CheckDone()
        { 
            if (!swtFilterLanguage.Checked)
                return false;
            if (!swtShowSpeaking.Checked && !swtShowWriting.Checked)
                return false;
            return true; ;
        }
    }
}