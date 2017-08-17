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
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Naxam.Busuu.Droid.Notification.Adapters;
using Naxam.Busuu.Droid.Notification.Models;
using MvvmCross.Droid.Views;

namespace Naxam.Busuu.Droid.Notification.Views
{
    [Activity(Label = "FriendRequestView")]
    public class FriendRequestView : MvxActivity
    {
        private RecyclerView FriendRecyclerView;
        private AdapterFriend adapterFriend;
        private List<FriendModel> friendModels;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_friend_request);
            bindViews();
        }
        private void bindViews()
        {
            friendModels = new List<FriendModel>();
            for (int i = 0; i < 10; i++)
            {
                friendModels.Add(new FriendModel());
            };
            FriendRecyclerView = (RecyclerView)FindViewById(Resource.Id.recyclerFriend);
            adapterFriend = new AdapterFriend(friendModels, this);
            LinearLayoutManager layoutManager = new LinearLayoutManager(ApplicationContext);
            layoutManager.Orientation=LinearLayoutManager.Vertical;
            FriendRecyclerView.SetLayoutManager(layoutManager);
            FriendRecyclerView.SetAdapter(adapterFriend);
        }
    }

}