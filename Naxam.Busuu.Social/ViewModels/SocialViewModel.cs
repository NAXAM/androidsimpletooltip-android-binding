using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Social.ViewModels
{
    public class SocialViewModel : MvxViewModel
    {
		public IMvxCommand FilterViewCommand
		{
            get { return new MvxCommand(() => ShowViewModel<FilterViewModel>()); }
		}		
    }
}
