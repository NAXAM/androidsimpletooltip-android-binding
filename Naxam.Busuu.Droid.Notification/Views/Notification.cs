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
using Naxam.Busuu.Droid.Notification.Models;

namespace Naxam.Busuu.Droid.Notification.Views
{
    [Activity(Label = "Notification")]
    public class Notification : Activity
    {
        private RecyclerView recyclerView;
        private AdapterNotification adapterNotification;
        private List<NotificationModel> notificationModels;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_notification);
            init();
        }
        private void init()
        {
            recyclerView = (RecyclerView)FindViewById(Resource.Id.myRecyclerView);
            notificationModels = new List<NotificationModel>();

            for (int i = 0; i < 10; i++)
            {
                notificationModels.Add(new NotificationModel());
            }
            adapterNotification = new AdapterNotification(notificationModels);
            LinearLayoutManager layoutManager = new LinearLayoutManager(ApplicationContext);
            layoutManager.Orientation=LinearLayoutManager.Vertical;
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetAdapter(adapterNotification);

        }
    }
}