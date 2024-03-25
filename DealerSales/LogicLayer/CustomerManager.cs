using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    public class CustomerManager : ICustomerManager
    {
        List<Customer> _customers = null;

        public bool AddCustomer(string fName, string lName, string phone, string email, int Zip)
        {
            CustomerAccessor customerAccessor = new CustomerAccessor();
            bool completed = customerAccessor.AddCustomer(fName, lName, phone, email, Zip);
            return completed;
        }
        public bool AddCustomer(Customer customer)
        {
            CustomerAccessor customerAccessor = new CustomerAccessor();
            return customerAccessor.AddCustomer(customer);
        }

        public List<Customer> GetAllActiveCustomers()
        {
            _customers = new List<Customer>();
            try
            {
                CustomerAccessor customerAccessor = new CustomerAccessor();
                _customers = customerAccessor.RetreiveAllAvailableCustomers();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Customers could not be found", ex);
            }

            return _customers;
        }

        public Customer GetCustomerByID(int custID)
        {
            CustomerAccessor customerAccessor = new CustomerAccessor();
            try
            {
                return customerAccessor.RetrieveCustomerByCustomerID(custID);
            }
            catch(Exception ex)
            {
                throw new Exception("An error occured finding the customer", ex);
            }
        }

        public bool RemoveCustomer(int custID)
        {
            CustomerAccessor customerAccessor = new CustomerAccessor();
            bool completed = customerAccessor.RemoveCustomer(custID);
            return completed;
        }

        public bool UpdateCustomer(int custID, string fName, string lName, string phone, string email, int Zip)
        {
            try
            {
                CustomerAccessor customerAccessor = new CustomerAccessor();
                return customerAccessor.UpdateCustomer(custID, fName, lName, phone, email, Zip);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Could not find Customer");
            }
            
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                CustomerAccessor customerAccessor = new CustomerAccessor();
                return customerAccessor.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not find Customer");
            }
        }
    }
}
