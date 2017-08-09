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
            ((RecyclerViewHolder)holder).txtName.Text = listFriend[position].getName();
            // Glide.With(context).Load("").
            //Glide.With(context).Load(listFriend[position].getUrlAvatar()).Into(((RecyclerViewHolder)holder).imgAvatar);
            //Glide.With(context).Load("").Transform( new CircleTransform(context))
           // Glide.With(context).Load(Resource.Drawable.ic_grey_tick).Apply(RequestOptions.circleCropTransform()).into(((RecyclerViewHolder)holder).imgConfirm);
            //Glide.With(context).Load(Resource.Drawable.ic_grey_cross).apply(RequestOptions.circleCropTransform()).into(((RecyclerViewHolder)holder).imgDelete);
            //Glide.With(context).Load(Resource.Drawable.ic_blue_tick).apply(RequestOptions.circleCropTransform()).into(((RecyclerViewHolder)holder).imgBlueConfirm);
            //
            Glide.With(context).Load(listFriend[position].getUrlAvatar()).Into(((RecyclerViewHolder)holder).imgAvatar);
            

           

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
            private Animation fadeOut = new AlphaAnimation(1, 0);
            public FrameLayout mFrameLayout;
            public TextView txtName;
            public ImageView imgAvatar, imgConfirm, imgDelete, imgBlueConfirm;

            public RecyclerViewHolder(View itemView) : base(itemView)
            {
                scal.Duration = 300;
                scal.FillAfter = true;
                //
                fadeOut.FillAfter = true;
                fadeOut.Duration = 500;

                mFrameLayout = (FrameLayout)itemView.FindViewById(Resource.Id.myFrameLayout);
                imgBlueConfirm = (ImageView)itemView.FindViewById(Resource.Id.imgBlueConfirm);
                txtName = (TextView)itemView.FindViewById(Resource.Id.txtName);
                imgAvatar = (ImageView)itemView.FindViewById(Resource.Id.imgAvatar);
                imgConfirm = (ImageView)itemView.FindViewById(Resource.Id.imgConfirm);
                imgDelete = (ImageView)itemView.FindViewById(Resource.Id.imgDelete);
                scal.AnimationEnd += (s, e) =>
                {
                    ObjectAnimator anim = ObjectAnimator.OfFloat(mFrameLayout, "translationX", 0, 180);
                    anim.SetDuration(300);
                    anim.Start();
                    imgDelete.StartAnimation(fadeOut);

                };
                fadeOut.AnimationEnd += (s, e) =>
                {
                    imgDelete.Visibility = ViewStates.Invisible;

                };
               // Glide.With(this).Load("https://scontent.fhan2-1.fna.fbcdn.net/v/t1.0-9/20246173_1323543431092186_392776523060866838_n.jpg?oh=d1fb3da1a138d710152f283e03c8a21c&oe=59EEA441").Transform(new CircleTransform(this)).Into(imgAvatar);
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_grey_tick).Into(imgConfirm);
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_grey_cross).Into(imgDelete);
                Glide.With(itemView.Context).Load(Resource.Drawable.ic_blue_tick).Into(imgBlueConfirm);
                imgConfirm.Clickable = true;
                imgConfirm.Click += (s, e) =>
                {
                    imgConfirm.Visibility = ViewStates.Invisible;
                    imgBlueConfirm.StartAnimation(scal);

                };


            }

        }
    }
}