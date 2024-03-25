using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using DataAccessLayer;
using System.Security.Cryptography;
using LogicLayerInterfaces;
using DataAccessFakes;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor userAccessor = null;
        public UserManager()
        {
            userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor ua)
        {
            userAccessor = ua;
        }

        public List<string> GetAllRoles()
        {
            try
            {
                userAccessor = new UserAccessor();
                return userAccessor.SelectAllRoles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string HashSha256(string source)
        {
            if (source == "" || source == null)
            {
                throw new ArgumentException("Source can not be empty");
            }
            string result = "";

            byte[] data;

            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString();

            return result.ToLower();
        }



        public User LoginUser(string email, string password)
        {
            User user = null;

            try
            {
                password = HashSha256(password);
                if (1 == userAccessor.AuthenticateUser(email, password))
                {
                    user = userAccessor.SelectUserByEmail(email);
                    user.Roles = userAccessor.SelectRolesByEmployeeID(user.EmployeeID);
                }
                else
                {
                    throw new ApplicationException("Bad username or password.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Login Failed", ex);
            }

            return user;
        }

        public bool FindUser(string email)
        {
            try
            {
                return userAccessor.SelectUserByEmail(email) != null;
            }
            catch(ApplicationException ax)
            {
                if(ax.Message == "User not found")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }

        public int RetrieveUserIdByEmail(string email)
        {
            try
            {
                return userAccessor.SelectUserByEmail(email).EmployeeID;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }

        public bool AddUser(User employee)
        {
            try
            {
                if(1 == userAccessor.InsertEmployee(employee))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Could not add employee", ex);
            }
        }
    }
}
