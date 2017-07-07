using System;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;

namespace Naxam.Busuu.Social.Serveices
{
	public interface IDataSocialDetail
	{
		Task<MvxObservableCollection<SocialDetailModel>> GetAllSocialDetail();
	}
}
