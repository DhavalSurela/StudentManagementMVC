using RepositoryPattern.Core.Interfaces;
using RepositoryPattern.Core.Models;
using RepositoryPattern.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Services
{
    public class LoginService : ILoginService
    {
        public IUnitOfWork _unitOfWork;
  
        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Login(Login login, string jwtkey, string jwtexpiredays, string jwtissuer, string jwtaudience)
        {
            var username = login.UserName;
            var password = login.Password;

            if (_unitOfWork.Login.Login(username, password))
            {
                var role = "Admin";
                var jwtToken = Authentication.GenerateJWTAuthetication(login.UserName, role,  jwtkey,  jwtexpiredays,  jwtissuer,  jwtaudience);
            

               return jwtToken;

            }
            return null;
        }
    }
}
