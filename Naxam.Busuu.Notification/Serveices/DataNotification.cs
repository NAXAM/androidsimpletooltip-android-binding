using System;
using System.Collections.Generic;
using Naxam.Busuu.Notification.Models;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Serveices;

namespace Naxam.Busuu.Notification.Serveices
{
    public class DataNotification : IDataNotification
    {
        int z = DateTime.Now.Day;
        List<SocialModel> dataScial;

        public NotificationModel[] GetNotification()
        {
            DataSocial abcdef = new DataSocial();

            dataScial = new List<SocialModel>(abcdef.GetAllSocial());

            Random rnd = new Random();

            var list = new List<NotificationModel>();

            for (int i = 0; i < 22; i++)
            {
                bool bbb = false;
				int x = rnd.Next(0, 22);
				int y = rnd.Next(0, 2);

                if (y == 0)
                {
                    bbb = true;
                }

                if (z > 0)
                {
                    list.Add(new NotificationModel()
                    {
                        Id = x,
                        ImgUser = "user_avatar_placeholder.png",
                        NameUser = dataScial[x].Name,
                        Details = "*" + dataScial[x].Name + "* has asked you to correct their exercise",
                        Time = new DateTime(2017, 7, z--),
                        Check = bbb
                    });
                }
                else
                {
					list.Add(new NotificationModel()
					{
						Id = x,
						ImgUser = "user_avatar_placeholder.png",
						NameUser = dataScial[x].Name,
						Details = "*" + dataScial[x].Name + "* has asked you to correct their exercise",
						Time = new DateTime(2017, 7, 1),
						Check = bbb
					});
                }
            }

            return list.ToArray();
        }

    }
}
