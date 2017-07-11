using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Serveices;

namespace Naxam.Busuu.Social.ViewModels
{
    public class SocialDetailViewModel : MvxViewModel
    {
		readonly IDataSocial _datasocialdetail;

        public SocialDetailViewModel(IDataSocial datasocialdetail)
		{
			_datasocialdetail = datasocialdetail;
		}

		public IMvxCommand CommentViewCommand
		{
            get { return new MvxCommand(() => ShowViewModel<CommentViewModel>(new { idDetai })); }
		}

		public SocialModel SocialDetailData
		{
			get { return _socialdetail; }
			set
			{
				if (_socialdetail != value)
				{
					_socialdetail = value;
					RaisePropertyChanged(() => SocialDetailData);
				}
			}
		}
               
        public void Init(int ID)
        {
            idDetai = ID;
            SocialDetailData = _datasocialdetail.GetSocialById(ID);
        }

		SocialModel _socialdetail;

        int idDetai;
    }
}
