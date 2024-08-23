using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Core.Interfaces
{
    public interface ILoginRepositorry
    {
        public bool Login(string username, string password);
    }
}
