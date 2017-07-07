using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Serveices;

namespace Naxam.Busuu.Social.ViewModels
{
    public class FriendsViewModel : MvxViewModel
    {
        readonly IDataFriends _datafriends;

        private MvxObservableCollection<FriendsModel> _friends;

		public IMvxCommand PopModalCommand
		{
            get { return new MvxCommand(() => ShowViewModel<SocialDetailViewModel>()); }
		}

        public FriendsViewModel(IDataFriends datafriends)
		{
            _datafriends = datafriends;
		}

		public MvxObservableCollection<FriendsModel> FriendsData
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

		public async override void Start()
		{
            FriendsData = await _datafriends.GetAllFriends();
			base.Start();
		}
    }
}
