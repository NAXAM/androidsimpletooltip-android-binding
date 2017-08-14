using System;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Notification.Services
{
    public interface IDataNotification
    {
        NotificationModel[] GetNotification();
    }
}
