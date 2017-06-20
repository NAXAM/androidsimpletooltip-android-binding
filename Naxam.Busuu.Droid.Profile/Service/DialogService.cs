using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Naxam.Busuu.Profile.Model; 
using Naxam.Busuu.Droid.Profile.Views;
using Naxam.Busuu.Profile.Service;

namespace Naxam.Busuu.Droid.Profile.Service
{
    public class DialogService : IDialogService
    {
        public async Task<bool> ConfirmChooseLanguage(LanguageModel lang)
        {
            var dialog = new ConfirmChooseLanguageDialog(Application.Context, lang);
            dialog.Show();
            bool confirm = dialog.Confirm;
            return confirm;
        }
    }
}