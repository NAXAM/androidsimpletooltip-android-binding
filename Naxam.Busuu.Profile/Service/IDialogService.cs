using Naxam.Busuu.Profile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.Service
{
    public interface IDialogService
    {
        Task<bool> ConfirmChooseLanguage(LanguageModel lang);
    }
}
