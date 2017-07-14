using System;
using System.Collections.Generic;
using Naxam.Busuu.Notification.Models;

namespace Naxam.Busuu.Notification.Serveices
{
    public class DataNotification : IDataNotification
    {
        int[] idd = { 0, 1, 9, 10, 11, 12, 20, 21 };

        public NotificationModel[] GetNotification()
        {
            Random rnd = new Random();
            var list = new List<NotificationModel>();

            for (int i = 0; i < 22; i++)
            {
                bool bbb = false;
				int x = rnd.Next(0, 8);
				int y = rnd.Next(0, 2);
                if (y == 0)
                {
                    bbb = true;
                }

				list.Add(new NotificationModel()
                {
                    Id = idd[x],
                    ImgUser = "user_avatar_placeholder.png",
                    NameUser = "Nguyen Nhu Son",
                    Details = "Nguyen Nhu Son has asked you to correct their exercise",
                    Time = "14/7/2017",
                    Check = bbb
                });
            }

            return list.ToArray();
        }

    }
}
