using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Notification.Services;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.ViewModels;

namespace Naxam.Busuu.Notification.ViewModels
{
    public class NotificationViewModel : MvxViewModel
    {
        readonly IDataNotification _datanotification;

        MvxObservableCollection<NotificationModel> _notifications;

		public NotificationViewModel(IDataNotification datanotification)
		{

			_datanotification = datanotification;
		}

		public MvxObservableCollection<NotificationModel> NotificationData
		{
			get { return _notifications; }
			set
			{
				if (_notifications != value)
				{
					_notifications = value;
					RaisePropertyChanged(() => NotificationData);
				}
			}
		}

		IMvxCommand _ViewNotificationCommand;
		public IMvxCommand ViewNotificationCommand
		{
			get
			{
                return (_ViewNotificationCommand = _ViewNotificationCommand ?? new MvxCommand<NotificationModel>(ExecuteViewNotificationCommand));
			}
		}

        void ExecuteViewNotificationCommand(NotificationModel item)
		{
            item.Check = true;
            if(item.TypeView== ViewType.Request)
            {
                ShowViewModel<FriendRequestViewModel>();
            }
           
        }

        string _notificationCount;

        public string NotificationCount
        {
			get { return _notificationCount; }
			set
			{
				if (_notificationCount != value)
				{
					_notificationCount = value;
					RaisePropertyChanged(() => NotificationCount);
				}
			}
        }

		public override void Start()
		{
			NotificationData = new MvxObservableCollection<NotificationModel>(_datanotification.GetNotification());

            int noti = 1;

            for (int i = 0; i < NotificationData.Count; i++)
            {
                if (!NotificationData[i].Check)
                {
                    noti++;
                }
            }

            NotificationCount = noti.ToString();

			base.Start();
		}

	}
}
