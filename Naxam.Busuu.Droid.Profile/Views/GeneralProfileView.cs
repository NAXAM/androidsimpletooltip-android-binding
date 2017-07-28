using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Graphics;
using Android.Views.Animations;
using Naxam.Busuu.Droid.Profile.Utils;
using Android.Util;
using Android.Support.V4.View;
using Android.Graphics.Drawables;
using Java.Lang;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Droid.Profile.Behavior;
using Naxam.Busuu.Profile.Models;
using Android.Text;
using Android.Text.Style;
using static Android.Widget.TextView;
using Naxam.Busuu.Droid.Core.Utils;
using Com.Bumptech.Glide;
using Naxam.Busuu.Droid.Core.Transform;
using Naxam.Busuu.Droid.Profile.Dialogs;

namespace Naxam.Busuu.Droid.Profile.Views
{

    [Activity(Label = "GeneralProfileView", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class GeneralProfileView : MvxAppCompatActivity
    {
        private TabLayout tabLayout;
        private ViewPager viewPager;
        private TextView txtUserName, txtLocation, txtGender, txtLanguage, txtCorrections, txtLike,txtFriend;
        private ImageView imgLanguage, imgAvatar, imgSetting;
        private PopupMenu popup;
        private UserModel User;
        private LinearLayout layoutFriends;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GeneralProfileActivity);

            User = new UserModel
            {
                Name = "Do Thanh Binh",
                Country = new CountryModel
                {
                    Country = "Japan"
                },
                Gender = Gender.Undisclosed,
                Language = new List<LanguageModel> {
                    new LanguageModel
                    {
                        Language = "Vietnamese"
                    },
                     new LanguageModel
                    {
                        Language = "Laos"
                    },
                },
            };

            FindView();
            BindView();
            CreatFriends();

           
        }

        private void FindView()
        {
            tabLayout = FindViewById<TabLayout>(Resource.Id.tab_layout);
            viewPager = FindViewById<ViewPager>(Resource.Id.view_pager);

            txtUserName = FindViewById<TextView>(Resource.Id.txtUserName);
            txtGender = FindViewById<TextView>(Resource.Id.txtSex);
            txtCorrections = FindViewById<TextView>(Resource.Id.txtBestCorrections);
            txtLanguage = FindViewById<TextView>(Resource.Id.txtSpeak);
            txtLike = FindViewById<TextView>(Resource.Id.txtLikes);
            txtLocation = FindViewById<TextView>(Resource.Id.txtLocation);
            txtFriend = FindViewById<TextView>(Resource.Id.txtFriend);

            imgLanguage = FindViewById<ImageView>(Resource.Id.imgLanguage);
            imgAvatar = FindViewById<ImageView>(Resource.Id.imgAvatar);
            layoutFriends = FindViewById<LinearLayout>(Resource.Id.layoutFriends);
            popup = new PopupMenu(this, imgAvatar);
            popup.MenuInflater.Inflate(Resource.Menu.popup_menu, popup.Menu);
            popup.MenuItemClick += Popup_MenuItemClick;
        }

        private void Popup_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            UserPhotoDialog dialog = new UserPhotoDialog(this, "");
            if (e.Item.ItemId== Resource.Id.menu_show_photo)
            {
                dialog.Show();
            }
            else
            { 
                dialog.Show();
            }
        }

        private void BindView()
        {
            Android.Support.V4.App.FragmentManager manager = SupportFragmentManager;
            MyPagerAdapter adapter = new MyPagerAdapter(manager);
            viewPager.Adapter = adapter;
            tabLayout.SetupWithViewPager(viewPager);
            viewPager.AddOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener(tabLayout));
            tabLayout.SetTabsFromPagerAdapter(adapter);

            Glide.With(this).Load("https://scontent.fhan2-1.fna.fbcdn.net/v/t1.0-9/20246173_1323543431092186_392776523060866838_n.jpg?oh=d1fb3da1a138d710152f283e03c8a21c&oe=59EEA441").Transform(new CircleTransform(this)).Into(imgAvatar);
            Glide.With(this).Load("https://scontent.fhan2-1.fna.fbcdn.net/v/t1.0-9/20246173_1323543431092186_392776523060866838_n.jpg?oh=d1fb3da1a138d710152f283e03c8a21c&oe=59EEA441").Transform(new CircleTransform(this)).Into(imgLanguage);
            imgAvatar.Background = BackgroundUtil.BackgroundRound(this, 1000, Color.White);
            imgLanguage.Background = BackgroundUtil.BackgroundRound(this, 1000, Color.White);

            txtUserName.Text = User.Name;
            txtGender.Text = User.Gender.ToString();
            txtLike.Text = 2 + "LIKES";
            txtLocation.Text = User.Country.Country;
            txtFriend.Text = "Friend("+5+")";
            SpannableStringBuilder ssb = new SpannableStringBuilder("Speaks ");
            for (int i = 0; i < User.Language.Count; i++)
            {
                string lang = User.Language[i].Language;
                SpannableStringBuilder sb = new SpannableStringBuilder(lang);
                sb.SetSpan(new StyleSpan(TypefaceStyle.Bold), 0, lang.Length, SpanTypes.ExclusiveExclusive);
                ssb.Append(i == 0 ? "" : i == User.Language.Count - 1 ? " and " : ", ");
                ssb.Append(sb);
            }
            txtLanguage.SetText(ssb, BufferType.Normal);
            imgAvatar.Click += (s, e) =>
            {
                popup.Show();
            };
        }


        private void CreatFriends()
        {
            int imageWidth = (int)Util.PxFromDp(this, 40);
            for (int i = 0; i < 5; i++)
            {
                LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(imageWidth, imageWidth);
                param.Gravity = GravityFlags.Center;
                param.LeftMargin = imageWidth / 6;
                ImageView avatar = new ImageView(this);
                avatar.Background = BackgroundUtil.BackgroundRound(this, imageWidth, Color.White);
                avatar.SetImageResource(Resource.Drawable.usa_flag);
                Glide.With(this).Load("https://scontent.fhan2-1.fna.fbcdn.net/v/t1.0-9/20246173_1323543431092186_392776523060866838_n.jpg?oh=d1fb3da1a138d710152f283e03c8a21c&oe=59EEA441").Transform(new CircleTransform(this)).Into(avatar);
                layoutFriends.AddView(avatar, param);
            }
        }
        


    }
}