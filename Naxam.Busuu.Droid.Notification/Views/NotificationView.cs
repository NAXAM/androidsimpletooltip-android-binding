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
using Naxam.Busuu.Droid.Notification.Adapters;
using Naxam.Busuu.Core.Models;
using MvvmCross.Droid.Views;
using Naxam.Busuu.Notification.ViewModels;
using Naxam.Busuu.Droid.Core.Utils;

namespace Naxam.Busuu.Droid.Notification.Views
{
    [Activity(Label = "Notification")]
    public class NotificationView : MvxActivity
    {
        private RecyclerView recyclerView;
        private AdapterNotification adapterNotification;

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.activity_notification);
            recyclerView = (RecyclerView)FindViewById(Resource.Id.people_recycler_view);
            recyclerView.AddItemDecoration(new SpacesItemDecoration(20));
            // init();


        }
  
        private void init()
        {
            adapterNotification = new AdapterNotification((ViewModel as NotificationViewModel).NotificationData);
            LinearLayoutManager layoutManager = new LinearLayoutManager(ApplicationContext);
            layoutManager.Orientation=LinearLayoutManager.Vertical;
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetAdapter(adapterNotification);

        }
    }
}