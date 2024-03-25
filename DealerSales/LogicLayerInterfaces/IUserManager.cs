using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {
        User LoginUser(string email, string passwordHash);
        string HashSha256(string source);

        List<string> GetAllRoles();
        bool FindUser(string email);

        int RetrieveUserIdByEmail(string email);
    }
}
