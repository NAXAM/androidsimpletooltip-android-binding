using System;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Social.Models
{
    public class SocialModel : MvxNotifyPropertyChanged
    {
        int _Id;

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _Avatar;

        public string Avatar
        {
            get { return _Avatar; }
            set
            {
                if (_Avatar != value)
                {
                    _Avatar = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _Country;

		public string Country
		{
			get { return _Country; }
			set
			{
				if (_Country != value)
				{
					_Country = value;
					RaisePropertyChanged();
				}
			}
		}
		
        string _ImageSpeakLanguage;

		public string ImageSpeakLanguage
		{
			get { return _ImageSpeakLanguage; }
			set
			{
				if (_ImageSpeakLanguage != value)
				{
					_ImageSpeakLanguage = value;
					RaisePropertyChanged();
				}
			}
		}

        string _ImageLearn;

		public string ImageLearn
		{
			get { return _ImageLearn; }
			set
			{
				if (_ImageLearn != value)
				{
					_ImageLearn = value;
					RaisePropertyChanged();
				}
			}
		}

        string _TextLearn;

		public string TextLearn
		{
			get { return _TextLearn; }
			set
			{
				if (_TextLearn != value)
				{
					_TextLearn = value;
					RaisePropertyChanged();
				}
			}
		}

        bool _Speak;

        public bool Speak
		{
			get { return _Speak; }
			set
			{
				if (_Speak != value)
				{
					_Speak = value;
					RaisePropertyChanged();
				}
			}
		}

        string _Write;

        public string Write
		{
			get { return _Write; }
			set
			{
				if (_Write != value)
				{
					_Write = value;
					RaisePropertyChanged();
				}
			}
		}

        DateTimeOffset _PostedTime;

        public DateTimeOffset PostedTime
		{
            get { return _PostedTime; }
			set
			{
				if (_PostedTime != value)
				{
					_PostedTime = value;
					RaisePropertyChanged();
				}
			}
		}

        double _Star;

        public double Star
		{
			get { return _Star; }
			set
			{
				if (_Star != value)
				{
					_Star = value;
					RaisePropertyChanged();
				}
			}
		}

        bool _Friends;

		public bool Friends
		{
			get { return _Friends; }
			set
			{
				if (_Friends != value)
				{
					_Friends = value;
					RaisePropertyChanged();
				}
			}
		}

        string _ImgQuestion;

		public string ImgQuestion
		{
			get { return _ImgQuestion; }
			set
			{
				if (_ImgQuestion != value)
				{
					_ImgQuestion = value;
					RaisePropertyChanged();
				}
			}
		}

        string _TextQuestion;

		public string TextQuestion
		{
			get { return _TextQuestion; }
			set
			{
				if (_TextQuestion != value)
				{
					_TextQuestion = value;
					RaisePropertyChanged();
				}
			}
		}
    }
}
