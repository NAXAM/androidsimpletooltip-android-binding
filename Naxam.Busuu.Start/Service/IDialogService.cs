using Naxam.Busuu.Start.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Start.Service
{
    public interface IDialogService
    {
        Task<bool> ConfirmChooseLanguage(LanguageModel lang);
    }
}
