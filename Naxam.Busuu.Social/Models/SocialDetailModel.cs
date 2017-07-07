using System;
namespace Naxam.Busuu.Social.Models
{
    public class SocialDetailModel : FriendsModel
    {
        public bool Friends { get; set; }
        public string ImgQuestion { get; set; }
        public string TextQuestion { get; set; }

        public SocialDetailModel()
        {
        }
    }
}
