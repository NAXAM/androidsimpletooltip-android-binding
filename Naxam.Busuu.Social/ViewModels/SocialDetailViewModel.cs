using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Serveices;

namespace Naxam.Busuu.Social.ViewModels
{
    public class SocialDetailViewModel : MvxViewModel
    {
		readonly IDataSocialDetail _datasocialdetail;

		private MvxObservableCollection<SocialDetailModel> _socialdetail;        	

        public SocialDetailViewModel(IDataSocialDetail datasocialdetail)
		{
			_datasocialdetail = datasocialdetail;
		}

		public MvxObservableCollection<SocialDetailModel> SocialDetailData
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

		public async override void Start()
		{
            SocialDetailData = await _datasocialdetail.GetAllSocialDetail();
			base.Start();
		}
    }
}
