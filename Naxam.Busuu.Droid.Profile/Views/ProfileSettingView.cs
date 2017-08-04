﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load.Engine.Bitmap_recycle;
using Com.Bumptech.Glide.Load.Resource.Bitmap;
using Android.Support.V7.App;
using Com.Orhanobut.Dialogplus;
using Java.IO;
using System.IO;
using System.Diagnostics;
using Com.Nguyenhoanglam.Imagepicker.Activity;
using Java.Nio;
using Com.Nguyenhoanglam.Imagepicker.Model;
using Naxam.Busuu.Profile.Models;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "Profile Setting")]
    public class ProfileSettingView : MvxAppCompatActivity
    {
        private LinearLayout layoutPersonalName;
        private FrameLayout layoutPersonalAvatar;
        private LinearLayout layoutAboutMe;
        private LinearLayout layoutPersonalEmail;
        private LinearLayout layoutCountry;
        private LinearLayout layoutGender;
        private LinearLayout layoutISpeak;
        private LinearLayout layoutNotificationSetting;
        private LinearLayout layoutInterfaceLanguage;
        private LinearLayout layoutClearData;
        private LinearLayout layoutItWork;
        private LinearLayout layoutContactUs;
        private LinearLayout layoutLogOut;
        private LinearLayout layoutRedeemVoucher;

        private TextView txtPersonalName;
        private ImageView imPersonalAvatar;
        private TextView txtAboutMe;
        private TextView txtPersonalEmail;
        private TextView txtCountry;
        private TextView txtGender;
        private TextView txtISpeak;
        private TextView txtInterfaceLanguage;
        private TextView txtClearData;

        int REQUEST_CODE_PICKER = 2000;
        int REQUEST_CODE_CAMERA = 0;
        int REQUEST_CODE_CHANGE_DATA = 1;
        private List<Image> images = new List<Image>();
        UserModel model;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.profiles_setting);

            model = new UserModel()
            {
                username = "nghianahit",
                password = "deobiet",
                avatarImage = "https://scontent.fhan2-1.fna.fbcdn.net/v/t1.0-9/19554344_1170448823067074_3677184999917790335_n.jpg?oh=2882b6b2b9c7bfcba934fa3f4e9876cb&oe=5A01D016",
                fullName = "Ha Minh Nghia",
                gender = 1,
                email = "nghiaanhit@gmail.com",
                phoneNumber = "0984228297",
                Country = new CountryModel()
                {
                    Country = "Viet Nam",
                    PhoneCode = "+84"
                },
                speakLanguages = new List<LanguageModel>()
                {
                    new LanguageModel()
                    {
                        Language = "Viet Nam"
                    },
                    new LanguageModel()
                    {
                        Language = "English"
                    },
                    new LanguageModel()
                    {
                        Language = "French"
                    }
                },
                interfaceLanguage = new LanguageModel()
                {
                    Language = "Viet Nam"
                }
            };

            InitInterface();
        }

        public void InitInterface()
        {
            layoutPersonalName = FindViewById<LinearLayout>(Resource.Id.layout_personal_name);
            layoutPersonalAvatar = FindViewById<FrameLayout>(Resource.Id.layout_personal_avatar);
            layoutPersonalEmail = FindViewById<LinearLayout>(Resource.Id.layout_personal_email);
            layoutAboutMe = FindViewById<LinearLayout>(Resource.Id.layout_personal_about_me);
            layoutCountry = FindViewById<LinearLayout>(Resource.Id.layout_personal_country);
            layoutGender = FindViewById<LinearLayout>(Resource.Id.layout_personal_gender);
            layoutISpeak = FindViewById<LinearLayout>(Resource.Id.layout_personal_ispeak);
            layoutNotificationSetting = FindViewById<LinearLayout>(Resource.Id.layout_notifications_setting);
            layoutInterfaceLanguage = FindViewById<LinearLayout>(Resource.Id.layout_interface_language);
            layoutClearData = FindViewById<LinearLayout>(Resource.Id.layout_clear_data);
            layoutItWork = FindViewById<LinearLayout>(Resource.Id.layout_it_work);
            layoutContactUs = FindViewById<LinearLayout>(Resource.Id.layout_contact_us);
            layoutLogOut = FindViewById<LinearLayout>(Resource.Id.layout_log_out);
            layoutRedeemVoucher = FindViewById<LinearLayout>(Resource.Id.layout_redeem_voucher);

            txtPersonalName = FindViewById<TextView>(Resource.Id.txt_personal_name);
            imPersonalAvatar = FindViewById<ImageView>(Resource.Id.im_avatar);
            txtAboutMe = FindViewById<TextView>(Resource.Id.txt_personal_about_me);
            txtPersonalEmail = FindViewById<TextView>(Resource.Id.txt_personal_email);
            txtCountry = FindViewById<TextView>(Resource.Id.txt_personal_country);
            txtGender = FindViewById<TextView>(Resource.Id.txt_personal_gender);
            txtISpeak = FindViewById<TextView>(Resource.Id.txt_personal_ispeak);
            txtInterfaceLanguage = FindViewById<TextView>(Resource.Id.txt_interface_language);
            txtClearData = FindViewById<TextView>(Resource.Id.txt_clear_data);

            txtPersonalName.Text = model.fullName != null ? model.fullName : "";
            if (model.avatarImage != null) Glide.With(this).Load("https://scontent.fhan2-1.fna.fbcdn.net/v/t1.0-9/19554344_1170448823067074_3677184999917790335_n.jpg?oh=2882b6b2b9c7bfcba934fa3f4e9876cb&oe=5A01D016").BitmapTransform(new CircleTransform(this)).Into(imPersonalAvatar);
            txtAboutMe.Text = model.selfDescription != null ? model.selfDescription : "Write a bit about yourself";
            txtPersonalEmail.Text = model.email != null ? model.email : "";
            txtCountry.Text = model.Country != null ? model.Country.Country : "";
            txtGender.Text = model.gender != null ? model.gender != 0 ? model.gender != 1 ? "Undisclosed" : "Female" : "Male" : "";
            if (model.speakLanguages != null)
            {
                for (int i = 0; i < model.speakLanguages.Count; i++)
                {
                    txtISpeak.Text += model.speakLanguages[i].Language + ", ";
                }
            }
            txtInterfaceLanguage.Text = model.interfaceLanguage != null ? model.interfaceLanguage.Language : "";

            Intent intentChangeInput = new Intent(this, typeof(ProfileInputView));

            layoutPersonalAvatar.Click += (s, e) =>
            {
                DialogPlus dialog = DialogPlus.NewDialog(this)
                .SetContentHolder(new ViewHolder(Resource.Layout.dialog_image_picker))
                .SetGravity((int)GravityFlags.Bottom)
                .SetAdapter(new DialogImagePickerAdapter())
                .SetOnClickListener(new OnClickListener()
                {
                    ClickAction = (d, v) =>
                    {
                        if (v.Id == Resource.Id.btn_take_camera)
                        {
                            Intent cameraIntent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
                            StartActivityForResult(cameraIntent, REQUEST_CODE_CAMERA);
                        }

                        if (v.Id == Resource.Id.btn_take_gallery)
                        {
                            Intent intent = new Intent(this, typeof(ImagePickerActivity));
                            intent.PutExtra(ImagePickerActivity.IntentExtraFolderMode, true);
                            intent.PutExtra(ImagePickerActivity.IntentExtraMode, ImagePickerActivity.ModeSingle);
                            intent.PutExtra(ImagePickerActivity.IntentExtraLimit, 10);
                            intent.PutExtra(ImagePickerActivity.IntentExtraShowCamera, true);
                            intent.PutExtra(ImagePickerActivity.IntentExtraSelectedImages, images.ToArray());
                            intent.PutExtra(ImagePickerActivity.IntentExtraFolderTitle, "Album");
                            intent.PutExtra(ImagePickerActivity.IntentExtraImageTitle, "Tap to select images");
                            intent.PutExtra(ImagePickerActivity.IntentExtraImageDirectory, "Camera");
                            StartActivityForResult(intent, REQUEST_CODE_PICKER);
                        }

                        d.Dismiss();
                    }
                })
                .SetOnItemClickListener(new OnItemClickListener()
                {
                    ItemClick = (p0, p1, p2, p3) =>
                    {
                    }
                }).SetExpanded(false)
                .SetCancelable(true)
                .Create();
                dialog.Show();
            };
            layoutPersonalName.Click += (s, e) =>
            {
                intentChangeInput.PutExtra("ProfileInputData", model.fullName != null ? model.fullName : "");
                intentChangeInput.PutExtra("ProfileInputType", "name");
                StartActivityForResult(intentChangeInput, REQUEST_CODE_CHANGE_DATA);
            };
            layoutAboutMe.Click += (s, e) =>
            {
                intentChangeInput.PutExtra("ProfileInputData", model.selfDescription != null ? model.selfDescription : "");
                intentChangeInput.PutExtra("ProfileInputType", "aboutme");
                StartActivityForResult(intentChangeInput, REQUEST_CODE_CHANGE_DATA);
            };
            layoutCountry.Click += (s, e) =>
            {
                intentChangeInput.PutExtra("ProfileInputData", model.Country.Country);
                intentChangeInput.PutExtra("ProfileInputType", "country");
                StartActivityForResult(intentChangeInput, REQUEST_CODE_CHANGE_DATA);
            };
            layoutISpeak.Click += (s, e) =>
            {

            };
            layoutInterfaceLanguage.Click += (s, e) =>
            {

            };
            layoutClearData.Click += (s, e) =>
            {

            };
            layoutItWork.Click += (s, e) =>
            {

            };
            layoutContactUs.Click += (s, e) =>
            {

            };
            layoutLogOut.Click += (s, e) =>
            {
                Toast.MakeText(this, "Log out complete!", ToastLength.Short).Show();
            };
            layoutRedeemVoucher.Click += (s, e) =>
            {
                Toast.MakeText(this, model.voucher != null ? "You used a voucher!" : "You don't have a voucher at all!", ToastLength.Short).Show();
            };
            layoutGender.Click += (s, e) =>
            {
                intentChangeInput.PutExtra("ProfileInputData", model.gender != null ? model.gender : 0);
                intentChangeInput.PutExtra("ProfileInputType", "gender");
                StartActivityForResult(intentChangeInput, REQUEST_CODE_CHANGE_DATA);
            };
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == 0 && resultCode == Result.Ok)
            {
                Bitmap photo = (Bitmap)data.Extras.Get("data");
                MemoryStream stream = new MemoryStream();
                photo.Compress(Bitmap.CompressFormat.Png, 0, stream);
                byte[] bitmapData = stream.ToArray();

                Glide.With(this).Load(bitmapData).BitmapTransform(new CircleTransform(this)).Into(imPersonalAvatar);
                imPersonalAvatar.SetImageBitmap(photo);
            }
            else if (resultCode == Result.Ok && requestCode == REQUEST_CODE_PICKER)
            {
                var imagexx = data.GetParcelableArrayListExtra(ImagePickerActivity.IntentExtraSelectedImages);
                images.Clear();
                foreach (var item in imagexx)
                {
                    images.Add((Image)item);
                }

                Glide.With(this).Load(new Java.IO.File(images[0].Path)).BitmapTransform(new CircleTransform(this)).Into(imPersonalAvatar);
            }
            else if (resultCode == Result.Ok && requestCode == REQUEST_CODE_CHANGE_DATA)
            {
                if (data.HasExtra("name") && data.GetStringExtra("name") != null)
                {
                    txtPersonalName.Text = data.GetStringExtra("name");
                }
                else if (data.HasExtra("aboutme") && data.GetStringExtra("aboutme") != null)
                {
                    txtAboutMe.Text = data.GetStringExtra("aboutme").Length != 0 ? data.GetStringExtra("aboutme") : "Write a bit about yourselft";
                }
                else if (data.HasExtra("country") && data.GetStringExtra("country") != null)
                {
                    txtCountry.Text = data.GetStringExtra("country");
                }
                else if (data.HasExtra("gender") && data.GetIntExtra("gender", 0) != null)
                {
                    txtGender.Text = data.GetIntExtra("gender", 0) != 0 ? data.GetIntExtra("gender", 0) != 1 ? "Undisclosed" : "Female" : "Male";
                }
                else if (data.HasExtra("ispeak") && data.GetStringExtra("ispeak") != null)
                {

                }
                else if (data.HasExtra("interfacelanguage") && data.GetStringExtra("interfacelanguage") != null)
                {

                }
            }
        }
        class OnClickListener : Java.Lang.Object, IOnClickListener
        {
            public Action<DialogPlus, View> ClickAction;
            public void OnClick(DialogPlus p0, View p1)
            {
                ClickAction?.Invoke(p0, p1);
            }
        }
        class OnItemClickListener : Java.Lang.Object, IOnItemClickListener
        {
            public Action<DialogPlus, Java.Lang.Object, View, int> ItemClick { get; set; }
            public void OnItemClick(DialogPlus p0, Java.Lang.Object p1, View p2, int p3)
            {
                ItemClick?.Invoke(p0, p1, p2, p3);
            }
        }
        class CircleTransform : BitmapTransformation
        {
            public CircleTransform(Context context) : base(context)
            {
            }
            public override string Id
            {
                get { return nameof(CircleTransform); }
            }

            protected override Bitmap Transform(IBitmapPool p0, Bitmap p1, int p2, int p3)
            {
                return CircleCrop(p0, p1);
            }

            private Bitmap CircleCrop(IBitmapPool pool, Bitmap source)
            {
                if (source == null) return null;

                int size = Math.Min(source.Width, source.Height);
                int x = (source.Width - size) / 2;
                int y = (source.Height - size) / 2;

                Bitmap squared = Bitmap.CreateBitmap(source, x, y, size, size);

                Bitmap result = pool.Get(size, size, Bitmap.Config.Argb8888);
                if (result == null)
                {
                    result = Bitmap.CreateBitmap(size, size, Bitmap.Config.Argb8888);
                }

                Canvas canvas = new Canvas(result);
                Paint paint = new Paint();
                paint.SetShader(new BitmapShader(squared, BitmapShader.TileMode.Clamp, BitmapShader.TileMode.Clamp));
                paint.AntiAlias = true;
                float r = size / 2f;
                canvas.DrawCircle(r, r, r, paint);
                return result;
            }
        }
    }
}