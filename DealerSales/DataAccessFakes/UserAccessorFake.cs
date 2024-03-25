using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class UserAccessorFake : IUserAccessor
    {
        public List<User> fakeUsers = new List<User>();
        public List<string> passwordFakes = new List<string>();

        public UserAccessorFake()
        {
            fakeUsers.Add(new User()
            {
                EmployeeID = 10000,
                FirstName = "Brendan",
                LastName = "Klostermann",
                PhoneNumber = "3197205011",
                LocationID = 1000,
                EmailAddress = "brendan@company.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeUsers.Add(new User()
            {
                EmployeeID = 10001,
                FirstName = "Marika",
                LastName = "Bjornson",
                PhoneNumber = "3195551234",
                LocationID = 1001,
                EmailAddress = "marika@company.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeUsers.Add(new User()
            {
                EmployeeID = 1002,
                FirstName = "Joe",
                LastName = "Smith",
                PhoneNumber = "3197774321",
                LocationID = 1000,
                EmailAddress = "joe@company.com",
                Active = false,
                Roles = new List<string>()
            });

            passwordFakes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            passwordFakes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            passwordFakes.Add("Invalid");

            fakeUsers[0].Roles.Add("Manager");
            fakeUsers[0].Roles.Add("Director");
            fakeUsers[1].Roles.Add("Salesman");
        }

        public int AuthenticateUser(string email, string passwordHash)
        {
            int result = 0;
            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].EmailAddress == email && passwordFakes[i] == passwordHash)
                {
                    result++;
                }
            }

            return result; //should return 1 meaning one email password combo was found
        }

        public int InsertEmployee(User user)
        {
            throw new NotImplementedException();
        }

        public List<string> SelectAllRoles()
        {
            throw new NotImplementedException();
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            throw new NotImplementedException();    
        }

        public User SelectUserByEmail(string email)
        {
            User user = null;
            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.EmailAddress == email)
                {
                    user = fakeUser;
                }
            }
            if (user == null)
            {
                throw new ApplicationException("User Not found.");
            }
            return user;
        }
    }
}
