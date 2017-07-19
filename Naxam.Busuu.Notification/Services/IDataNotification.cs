using System;
using Naxam.Busuu.Notification.Models;

namespace Naxam.Busuu.Notification.Services
{
    public interface IDataNotification
    {
        NotificationModel[] GetNotification();
    }
}
