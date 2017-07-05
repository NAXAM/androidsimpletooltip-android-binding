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

        public FriendsViewModel(IDataFriends datafriends)
		{
            _datafriends = datafriends;
		}

		public MvxObservableCollection<FriendsModel> FriendsData
		{
			get => _friends;
			set => SetProperty(ref _friends, value);
		}

		public async override void Start()
		{
            FriendsData = await _datafriends.GetAllFriends();
			base.Start();
		}
    }
}
