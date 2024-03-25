using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IUserAccessor
    {
        int AuthenticateUser(String email, String passwordHash);

        User SelectUserByEmail(string email);

        List<string> SelectRolesByEmployeeID(int employeeID);

        List<string> SelectAllRoles();
        int InsertEmployee(User user);
    }
}
