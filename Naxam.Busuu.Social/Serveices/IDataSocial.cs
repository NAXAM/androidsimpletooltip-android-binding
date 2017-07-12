using System.Threading.Tasks;
using Naxam.Busuu.Social.Models;

namespace Naxam.Busuu.Social.Serveices
{
	public interface IDataSocial
	{		
        Task<SocialModel[]> GetFriendSocial();
        Task<SocialModel[]> GetDiscoverSocial();
        Task<SocialModel> GetSocialById(int id);
	}
}
