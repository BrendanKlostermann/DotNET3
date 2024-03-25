using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class CustomerAccessorFake : ICustomerAccessor
    {
        List<Customer> _customers = null;

        public CustomerAccessorFake()
        {
            _customers = new List<Customer>();
            _customers.Add(new Customer
            {
                CustomerID = 10000,
                FirstName = "Hunter",
                LastName = "Kiser",
                PhoneNumber = "3198275948",
                EmailAddress = "hunter@gmail.com",
                ZipCode = 52302
            });
            _customers.Add(new Customer
            {
                CustomerID = 10000,
                FirstName = "Chase",
                LastName = "Bading",
                PhoneNumber = "3192349182",
                EmailAddress = "chase@gmail.com",
                ZipCode = 52402
            });
        }

        public bool AddCustomer(string fName, string lName, string phone, string email, int Zip)
        {
            throw new NotImplementedException();
        }

        public bool AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCustomer(int custID)
        {
            throw new NotImplementedException();
        }

        public List<Customer> RetreiveAllAvailableCustomers()
        {
            return _customers;
        }

        public Customer RetrieveCustomerByCustomerID(int custID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCustomer(int custID, string fName, string lName, string phone, string email, int Zip)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
