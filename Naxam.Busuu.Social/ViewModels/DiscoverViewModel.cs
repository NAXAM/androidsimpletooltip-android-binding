using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Serveices;

namespace Naxam.Busuu.Social.ViewModels
{
    public class DiscoverViewModel : MvxViewModel
    {
		readonly IDataDiscover _datadiscover;

		private List<DiscoverModel> _discover;

        public DiscoverViewModel(IDataDiscover datadiscover)
        {
            _datadiscover = datadiscover;
        }

		public List<DiscoverModel> DiscoverData
		{
			get => _discover;
			set => SetProperty(ref _discover, value);
		}

        public async override void Start()
		{
            DiscoverData = await _datadiscover.GetAllDiscover();
			base.Start();
		}
    }
}
