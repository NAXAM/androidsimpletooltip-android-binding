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
using Android.Support.V7.Widget;
using Android.Views.Animations;
using Naxam.Busuu.Droid.Notification.Models;
using Com.Bumptech.Glide;
using Android.Animation;
using Naxam.Busuu.Droid.Core.Transform;

namespace Naxam.Busuu.Droid.Notification.Adapters
{
    public class AdapterFriend : RecyclerView.Adapter
    {


        private Context context;
        private List<FriendModel> listFriend = new List<FriendModel>();

        //
        public AdapterFriend(List<FriendModel> listFriend, Context context)
        {
            this.listFriend = listFriend;
            this.context = context;
            //
           
        }

        public override int ItemCount => listFriend.Count;


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
  
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemview = inflater.Inflate(Resource.Layout.item_friend_request, parent, false);
            return new RecyclerViewHolder(itemview);
        }

        public class RecyclerViewHolder : RecyclerView.ViewHolder
        {
            private ScaleAnimation scal = new ScaleAnimation(0, 1f, 0, 1f, Dimension.RelativeToSelf, (float)0.5, Dimension.RelativeToSelf, (float)0.5);
            private ScaleAnimation scalDeleteImage = new ScaleAnimation(0, 1f, 0, 1f, Dimension.RelativeToSelf, (float)0.5, Dimension.RelativeToSelf, (float)0.5);

            private Animation fadeOut = new AlphaAnimation(1, 0);
            public FrameLayout mFrameLayout;
            public TextView txtName;
            public ImageView imgAvatar, imgConfirm, imgDelete, imgBlueConfirm, imgHiddenDelete;

            public RecyclerViewHolder(View itemView) : base(itemView)
            {
                scal.Duration = 300;
                scal.FillAfter = true;
                //
                scalDeleteImage.Duration = 300;
                scalDeleteImage.FillAfter = true;
                //
                fadeOut.FillAfter = true;
                fadeOut.Duration = 500;

                mFrameLayout = (FrameLayout)itemView.FindViewById(Resource.Id.myFrameLayout);
                imgBlueConfirm = (ImageView)itemView.FindViewById(Resource.Id.imgBlueConfirm);
                txtName = (TextView)itemView.FindViewById(Resource.Id.txtName);
                imgAvatar = (ImageView)itemView.FindViewById(Resource.Id.imgAvatar);
                imgConfirm = (ImageView)itemView.FindViewById(Resource.Id.imgConfirm);
                imgDelete = (ImageView)itemView.FindViewById(Resource.Id.imgDelete);
                imgHiddenDelete = itemView.FindViewById<ImageView>(Resource.Id.imgHiddenDelete);
                //
                scalDeleteImage.AnimationEnd += (s, e) =>
                {
                    imgConfirm.Visibility = ViewStates.Gone;
                    imgBlueConfirm.Visibility = ViewStates.Gone;


                };
                scal.AnimationEnd += (s, e) =>
                {
                    ObjectAnimator anim = ObjectAnimator.OfFloat(mFrameLayout, "translationX", 0, 150);
                    anim.SetDuration(300);
                    anim.Start();
                    imgDelete.StartAnimation(fadeOut);

                };
                fadeOut.AnimationEnd += (s, e) =>
                {
                    imgDelete.Visibility = ViewStates.Invisible;

                };
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_grey_tick).Transform(new CircleTransform(itemView.Context)).Into(imgConfirm);
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_white_cross).Transform(new CircleTransform(itemView.Context)).Into(imgHiddenDelete);
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_grey_cross).Transform(new CircleTransform(itemView.Context)).Into(imgDelete);
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_blue_tick).Transform(new CircleTransform(itemView.Context)).Into(imgBlueConfirm);
                Glide.With(itemView.Context).Load("http://media.phunutoday.vn/files/tho_nguyen/2017/05/31/ngoc-trinh-4-1429-phunutoday.jpg").Transform(new CircleTransform(itemView.Context)).Into(imgAvatar);
                imgHiddenDelete.Clickable = true;
                imgConfirm.Clickable = true;
                imgConfirm.Click += (s, e) =>
                {
                    imgHiddenDelete.Visibility = ViewStates.Invisible;
                    imgConfirm.Visibility = ViewStates.Invisible;
                    imgBlueConfirm.StartAnimation(scal);

                };
                imgHiddenDelete.Click += (s, e) =>
                {
                    imgDelete.Visibility = ViewStates.Invisible;
                    imgHiddenDelete.StartAnimation(scalDeleteImage);
                };


            }

        }
    }
}