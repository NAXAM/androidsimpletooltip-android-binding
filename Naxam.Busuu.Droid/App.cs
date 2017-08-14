using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Profile.ViewModels;
using MvvmCross.Platform; 
using Naxam.Busuu.Droid.Profile.Service; 
using Naxam.Busuu.Social.Services;
using Naxam.Busuu.Review.Services;
using Naxam.Busuu.Learning.Services;
using Naxam.Busuu.Learning.ViewModel;
using Naxam.Busuu.Notification.Services;

namespace Naxam.Busuu.Droid
{
    public class App : MvxApplication
    {
        public App()
        {
            Mvx.RegisterType<IDataSocial, DataSocial>();
           // Mvx.RegisterType<IDataNotification, DataNotification>();
            Mvx.RegisterType<IReviewService, ReviewService>();
            Mvx.RegisterType<ILearningService, LearningService>();
            Mvx.RegisterType<IDataNotification, DataNotification>();
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());
            RegisterAppStart<MainViewModel>();
          //  Mvx.RegisterType<IDialogService, DialogService>();
        }
        public override void Initialize()
        { 
            RegisterAppStart<MainViewModel>();
        }
    }
}