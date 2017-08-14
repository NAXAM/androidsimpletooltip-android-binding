using System;
using System.Collections.Generic;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Services;

namespace Naxam.Busuu.Notification.Services
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
                        TypeView = ViewType.Correct,
                        Id = x,
                        ImgUser = "http://media.phunutoday.vn/files/tho_nguyen/2017/05/31/ngoc-trinh-4-1429-phunutoday.jpg",
                        NameUser = dataScial[x].Name,
                        Details = dataScial[x].Name + "| has asked you to correct their exercise",
                        Time = new DateTime(2017, 7, z--),
                        Check = false,
                    });
                }
                else
                {
					list.Add(new NotificationModel()
					{
                        TypeView = ViewType.Reply,
                        Id = x,
						ImgUser = "http://media.phunutoday.vn/files/tho_nguyen/2017/05/31/ngoc-trinh-4-1429-phunutoday.jpg",
						NameUser = dataScial[x].Name,
						Details = dataScial[x].Name + " has asked you to correct their exercise",
						Time = new DateTime(2017, 7, 1),
						Check = false,
					});
                }
            }
            //
            //list.RemoveAt(0);
            list.Insert(0, new NotificationModel()
            {
                TypeView = ViewType.Request,
                Id = 1,
                ImgUser = "http://anh.24h.com.vn/upload/3-2016/images/2016-08-25/1472094439-147203352566000-1464421146-1464415976-bang-kieu-showbiz247-15.jpg",
                NameUser = "Thao Luu",
                Details = "Thao Luu has asked you to correct their exercise",
                Time = new DateTime(2017, 7, 1),
                Check = false,

            });

            return list.ToArray();
        }

    }
}
