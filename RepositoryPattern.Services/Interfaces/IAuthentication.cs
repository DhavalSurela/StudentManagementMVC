using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Services.Interfaces
{
    public interface IAuthentication
    {
        public static abstract string GenerateJWTAuthetication(string userName, string role, string jwtkey, string jwtexpiredays, string jwtissuer, string jwtaudience);
        public static abstract string ValidateToken(string token, string jwtkey);
    }
}
