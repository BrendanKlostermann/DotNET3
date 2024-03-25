using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects; 
using DataAccessInterfaces;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class CustomerAccessor : ICustomerAccessor
    {
        List<Customer> _customers = null;
        Customer _customer = null;

        public bool AddCustomer(string fName, string lName, string phone, string email, int Zip)
        {
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_insert_customer";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@fName", SqlDbType.NVarChar, 30);
            cmd.Parameters[0].Value = fName;

            cmd.Parameters.Add("@lName", SqlDbType.NVarChar, 30);
            cmd.Parameters[1].Value = lName;

            cmd.Parameters.Add("@phone", SqlDbType.NVarChar, 15);
            cmd.Parameters[2].Value = phone;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);
            cmd.Parameters[3].Value = email;

            cmd.Parameters.Add("@zip", SqlDbType.Int);
            cmd.Parameters[4].Value = Zip;


            try
            {
                conn.Open();
                int count = cmd.ExecuteNonQuery();

                if(count == 1)
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
                throw new ApplicationException("Could not Add User", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool AddCustomer(Customer customer)
        {
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_insert_customer";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@fName", SqlDbType.NVarChar, 30);
            cmd.Parameters[0].Value = customer.FirstName;

            cmd.Parameters.Add("@lName", SqlDbType.NVarChar, 30);
            cmd.Parameters[1].Value = customer.LastName;

            cmd.Parameters.Add("@phone", SqlDbType.NVarChar, 15);
            cmd.Parameters[2].Value = customer.PhoneNumber;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);
            cmd.Parameters[3].Value = customer.EmailAddress;

            cmd.Parameters.Add("@zip", SqlDbType.Int);
            cmd.Parameters[4].Value = customer.ZipCode;


            try
            {
                conn.Open();
                int count = cmd.ExecuteNonQuery();

                if (count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not Add User", ex);
            }
            finally
            {
                conn.Close();
            }
        }


        public bool RemoveCustomer(int custID)
        {
            DBConnection connectionFacorty = new DBConnection();
            var conn = connectionFacorty.GetDBConnection();
            var cmdText = "sp_deactivate_customer";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@custID", SqlDbType.Int);
            cmd.Parameters[0].Value = custID;

            try
            {
                conn.Open();
                int count = cmd.ExecuteNonQuery();

                if(count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not remove customer", ex);
            }
        }

        public List<Customer> RetreiveAllAvailableCustomers()
        {
            _customers = new List<Customer>();
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_all_active_customers";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _customer = new Customer();
                        _customer.CustomerID = reader.GetInt32(0);
                        _customer.FirstName = reader.GetString(1);
                        _customer.LastName = reader.GetString(2);
                        _customer.PhoneNumber = reader.GetString(3);
                        _customer.EmailAddress = reader.GetString(4);
                        _customer.ZipCode = reader.GetInt32(5);

                        _customers.Add(_customer);

                    }
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not find any customers", ex);
            }
            finally
            {
                conn.Close();
            }

            return _customers;
        }

        public Customer RetrieveCustomerByCustomerID(int custID)
        {
            Customer customer = new Customer();
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_customer_by_customerID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@customerID", custID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                        customer.CustomerID = reader.GetInt32(0);
                        customer.FirstName = reader.GetString(1);
                        customer.LastName = reader.GetString(2);
                        customer.PhoneNumber = reader.GetString(3);
                        customer.EmailAddress = reader.GetString(4);
                        customer.ZipCode = reader.GetInt32(5);
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not find the Customer with that ID", ex);
            }
            finally
            {
                conn.Close();
            }

            return customer;
        }

        public bool UpdateCustomer(int custID, string fName, string lName, string phone, string email, int Zip)
        {
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_update_customer";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@custID", SqlDbType.Int);
            cmd.Parameters[0].Value = custID;
            
            cmd.Parameters.Add("@fName", SqlDbType.NVarChar, 30);
            cmd.Parameters[1].Value = fName;

            cmd.Parameters.Add("@lName", SqlDbType.NVarChar, 30);
            cmd.Parameters[2].Value = lName;

            cmd.Parameters.Add("@phone", SqlDbType.NVarChar, 15);
            cmd.Parameters[3].Value = phone;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);
            cmd.Parameters[4].Value = email;

            cmd.Parameters.Add("@zip", SqlDbType.Int);
            cmd.Parameters[5].Value = Zip;

            try
            {
                conn.Open();
                int count = cmd.ExecuteNonQuery();
                if(count == 1)
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
                throw new ApplicationException("Could find user to update", ex);
            }
            finally
            {
                conn.Close();
            }

        }

        public bool UpdateCustomer(Customer customer)
        {
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_update_customer";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@custID", SqlDbType.Int);
            cmd.Parameters[0].Value = customer.CustomerID;

            cmd.Parameters.Add("@fName", SqlDbType.NVarChar, 30);
            cmd.Parameters[1].Value = customer.FirstName;

            cmd.Parameters.Add("@lName", SqlDbType.NVarChar, 30);
            cmd.Parameters[2].Value = customer.LastName;

            cmd.Parameters.Add("@phone", SqlDbType.NVarChar, 15);
            cmd.Parameters[3].Value = customer.PhoneNumber;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);
            cmd.Parameters[4].Value = customer.EmailAddress;

            cmd.Parameters.Add("@zip", SqlDbType.Int);
            cmd.Parameters[5].Value = customer.ZipCode;

            try
            {
                conn.Open();
                int count = cmd.ExecuteNonQuery();
                if (count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could find user to update", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
