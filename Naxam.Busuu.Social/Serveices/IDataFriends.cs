using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;

namespace Naxam.Busuu.Social.Serveices
{
    public interface IDataFriends
    {
        Task<MvxObservableCollection<FriendsModel>> GetAllFriends();
    }
}