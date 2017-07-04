using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Naxam.Busuu.Social.Models;

namespace Naxam.Busuu.Social.Serveices
{
    public interface IDataDiscover
    {
         Task<List<Discover>> GetAllDiscover();
    }
}