using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Serveices;

namespace Naxam.Busuu.Social.ViewModels
{
    public class DiscoverViewModel : MvxViewModel
    {
		readonly IDataSocial _datadiscover;

        MvxObservableCollection<SocialModel> _discovers;

		public DiscoverViewModel(IDataSocial datadiscover)
		{
			_datadiscover = datadiscover;
		}

        public MvxObservableCollection<SocialModel> DiscoverData
		{
			get { return _discovers; }
			set
			{
				if (_discovers != value)
				{
					_discovers = value;
					RaisePropertyChanged(() => DiscoverData);
				}
			}
		}

        public override void Start()
		{
            DiscoverData = new MvxObservableCollection<SocialModel>(_datadiscover.GetDiscoverSocial());
            base.Start();
		}	

        IMvxCommand _ViewDisoverCommand;
        public IMvxCommand ViewDisoverCommand
        {
            get {
                return (_ViewDisoverCommand = _ViewDisoverCommand ?? new MvxCommand<SocialModel>(ExecuteViewDiscoverCommand));
            }
        }

        void ExecuteViewDiscoverCommand(SocialModel item)
        {
			ShowViewModel<SocialDetailViewModel>(new SocialModel
			{
                Id = item.Id
			});
        }
    }	
}
