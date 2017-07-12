using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Serveices;

namespace Naxam.Busuu.Social.ViewModels
{
    public class SocialDetailViewModel : MvxViewModel
    {
		readonly IDataSocial _datasocialdetail;

        private SocialModel _discoverdetail;

        public SocialDetailViewModel(IDataSocial datasocialdetail)
		{
			_datasocialdetail = datasocialdetail;
		}

		public SocialModel SocialDetailData
		{
			get { return _discoverdetail; }
			set
			{
				if (_discoverdetail != value)
				{
					_discoverdetail = value;
					RaisePropertyChanged(() => SocialDetailData);
				}
			}
		}
               
        public async void Init(int id){
            SocialDetailData = await _datasocialdetail.GetSocialById(id);
        }		
    }
}
