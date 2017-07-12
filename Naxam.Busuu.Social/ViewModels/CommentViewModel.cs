using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Serveices;

namespace Naxam.Busuu.Social.ViewModels
{
    public class CommentViewModel : MvxViewModel
    {
		readonly IDataSocial _datacomment;

		public CommentViewModel(IDataSocial datacomment)
		{
			_datacomment = datacomment;
		}

		SocialModel _Comment;

		public SocialModel CommentData
		{
			get { return _Comment; }
			set
			{
				if (_Comment != value)
				{
					_Comment = value;
					RaisePropertyChanged(() => CommentData);
				}
			}
		}

        public void Init(SocialModel item)
		{			
            CommentData = _datacomment.GetSocialById(item.Id);
		}
    }
}
