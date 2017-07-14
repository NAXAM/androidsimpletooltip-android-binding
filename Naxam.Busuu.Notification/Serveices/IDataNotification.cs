using System;
using Naxam.Busuu.Notification.Models;

namespace Naxam.Busuu.Notification.Serveices
{
    public interface IDataNotification
    {
        NotificationModel[] GetNotification();
    }
}
