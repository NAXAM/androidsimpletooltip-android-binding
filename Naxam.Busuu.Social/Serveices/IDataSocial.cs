using Naxam.Busuu.Social.Models;

namespace Naxam.Busuu.Social.Serveices
{
	public interface IDataSocial
	{		
        SocialModel[] GetFriendSocial();
        SocialModel[] GetDiscoverSocial();
        SocialModel GetSocialById(int id);
	}
}
