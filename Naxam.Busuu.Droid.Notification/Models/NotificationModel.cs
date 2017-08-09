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

namespace Naxam.Busuu.Droid.Notification.Models
{
    public class NotificationModel
    {
        private String Name;
        private String TimePost;
        private String UrlAvatar;

        public NotificationModel(String name, String timePost, String urlAvatar)
        {
            Name = name;
            TimePost = timePost;
            UrlAvatar = urlAvatar;
        }

        public NotificationModel()
        {
            Name = "Binh Do";
            TimePost = "3 hours ago";
            UrlAvatar = "http://anh.eva.vn/upload/4-2016/images/2016-12-30/truoc-khi-yeu-hoang-kieu-ngoc-trinh-cung-tung-o-trong-nhung-ngoi-nha-the-nay-nu-hoang-noi-y-ngoc-trinh-tu-khai-guong-mat-dao-keo-hinh-4-1483096395-width500height583.jpg";

        }

        public String getName()
        {
            return Name;
        }

        public void setName(String name)
        {
            Name = name;
        }

        public String getTimePost()
        {
            return TimePost;
        }

        public void setTimePost(String timePost)
        {
            TimePost = timePost;
        }

        public String getUrlAvatar()
        {
            return UrlAvatar;
        }

        public void setUrlAvatar(String urlAvatar)
        {
            UrlAvatar = urlAvatar;
        }
    }
}