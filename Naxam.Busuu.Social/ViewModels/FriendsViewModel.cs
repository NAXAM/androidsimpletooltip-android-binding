using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Serveices;

namespace Naxam.Busuu.Social.ViewModels
{
    public class FriendsViewModel : MvxViewModel
    {
        readonly IDataSocial _datafriends;

        MvxObservableCollection<SocialModel> _friends;

		public FriendsViewModel(IDataSocial datafriens)
		{
			_datafriends = datafriens;
		}

        public MvxObservableCollection<SocialModel> FriendsData
		{
			get { return _friends; }
			set
			{
				if (_friends != value)
				{
					_friends = value;
					RaisePropertyChanged(() => FriendsData);
				}
			}
		}

        public override void Start()
		{
            FriendsData = new MvxObservableCollection<SocialModel>(_datafriends.GetFriendSocial());		
			base.Start();
		}

		IMvxCommand _ViewFriendsCommand;
		public IMvxCommand ViewFriendsCommand
		{
			get
			{
				return (_ViewFriendsCommand = _ViewFriendsCommand ?? new MvxCommand<SocialModel>(ExecuteViewFriendsCommand));
			}
		}

		void ExecuteViewFriendsCommand(SocialModel item)
		{
            ShowViewModel<SocialDetailViewModel>(new { ID = item.Id });
		}
    }
}
