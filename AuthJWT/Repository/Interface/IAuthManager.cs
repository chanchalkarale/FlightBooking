using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthJWT.Repository.Interface
{
   public interface IAuthManager
    {
        string Authenticate(string username, string password);
    }
}
