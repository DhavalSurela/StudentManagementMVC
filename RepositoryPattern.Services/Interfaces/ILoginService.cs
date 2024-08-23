using RepositoryPattern.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Services.Interfaces
{
    public interface ILoginService
    {
        public string Login(Login login, string jwtkey, string jwtexpiredays, string jwtissuer, string jwtaudience);
    }
}
