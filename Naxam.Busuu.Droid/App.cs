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
using Naxam.Busuu.Profile.ViewModel;
using MvvmCross.Platform; 
using Naxam.Busuu.Droid.Profile.Service;
using Naxam.Busuu.Profile.Service;

namespace Naxam.Busuu.Droid
{
    public class App : MvxApplication
    {
        public App()
        {
            Mvx.RegisterType<IDialogService, DialogService>();
        }
        public override void Initialize()
        {
           // RegisterAppStart<ForgotPasswordViewModel>();
            RegisterAppStart<ChooseLanguageViewModel>();
        }
    }
}