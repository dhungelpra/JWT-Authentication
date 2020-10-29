using JWT_Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Authentication.Services
{
   public interface IJwtAuthenticateService
    {
        User Authenticate(string userName, string password);

    }
}
