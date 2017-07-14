using System;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Notification.Models
{
    public class NotificationModel : MvxNotifyPropertyChanged
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

		string _ImgUser;

		public string ImgUser
		{
			get { return _ImgUser; }
			set
			{
				if (_ImgUser != value)
				{
					_ImgUser = value;
					RaisePropertyChanged();
				}
			}
		}

        string _NameUser;

		public string NameUser
		{
			get { return _NameUser; }
			set
			{
				if (_NameUser != value)
				{
					_NameUser = value;
					RaisePropertyChanged();
				}
			}
		}

		string _Details;

		public string Details
		{
			get { return _Details; }
			set
			{
				if (_Details != value)
				{
					_Details = value;
					RaisePropertyChanged();
				}
			}
		}

		string _Time;

		public string Time
		{
			get { return _Time; }
			set
			{
				if (_Time != value)
				{
					_Time = value;
					RaisePropertyChanged();
				}
			}
		}

		bool _Check;

		public bool Check
		{
			get { return _Check; }
			set
			{
				if (_Check != value)
				{
					_Check = value;
					RaisePropertyChanged();
				}
			}
		}
    }
}
