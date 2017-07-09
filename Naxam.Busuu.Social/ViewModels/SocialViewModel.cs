using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Serveices;

namespace Naxam.Busuu.Social.ViewModels
{
    public class SocialViewModel : MvxViewModel
    {		
        public SocialViewModel()
		{          
		}

		public IMvxCommand GoToFilterViewCommand
		{
            get { return new MvxCommand(() => ShowViewModel<FilterViewModel>()); }
		}

		public IMvxCommand GoToSocialDetailViewCommand
		{
            get { return new MvxCommand(() => ShowViewModel<SocialDetailViewModel>()); }
		}
    }
}
