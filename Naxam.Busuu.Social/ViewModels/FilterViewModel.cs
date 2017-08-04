using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Social.ViewModels
{
    public class FilterViewModel : MvxViewModel
    {
        public bool Write
        {
            get;
            set;
        }

		public IMvxCommand btnDoneCommand
		{
            get { return new MvxCommand(() => Close(this)); }
		}
    }
}
