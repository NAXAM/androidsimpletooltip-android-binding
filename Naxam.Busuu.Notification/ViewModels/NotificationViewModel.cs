﻿using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Notification.Models;
using Naxam.Busuu.Notification.Serveices;
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
            ShowViewModel<SocialDetailViewModel>(new SocialModel
			{
				Id = item.Id
			});
		}

		public override void Start()
		{
			NotificationData = new MvxObservableCollection<NotificationModel>(_datanotification.GetNotification());
			base.Start();
		}

	}
}
