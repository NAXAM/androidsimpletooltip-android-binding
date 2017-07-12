using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Social.ViewModels
{
    public class SocialViewModel : MvxViewModel
    {
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
