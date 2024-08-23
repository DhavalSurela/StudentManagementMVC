using RepositoryPattern.Core.Interfaces;
using RepositoryPattern.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Infrastructure.Repositories
{
    public class LoginRepository : GenericRepository<Login>, ILoginRepositorry
    {
   

        public LoginRepository(DbContextClass dbContext) : base(dbContext) { }
        public bool Login(string username, string password)
        {
            var user = from s in base._dbContext.Login
                       where s.UserName == username && s.Password == password
                       select s;
            if (user.Any())
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
