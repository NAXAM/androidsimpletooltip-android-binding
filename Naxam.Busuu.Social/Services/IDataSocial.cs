using Naxam.Busuu.Social.Models;

namespace Naxam.Busuu.Social.Services
{
	public interface IDataSocial
	{		
        SocialModel[] GetFriendSocial();
        SocialModel[] GetDiscoverSocial();
        SocialModel GetSocialById(int id);
	}
}
