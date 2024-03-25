using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using System.Data.SqlClient;
using System.Data;
using DataObjects;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public int AuthenticateUser(string email, string passwordHash)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_authenticate_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@passwordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@passwordHash"].Value = passwordHash;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_all_roles";
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
                        string role = reader.GetString(0);
                        roles.Add(role);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not found", ex);
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> _roles = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_roles_by_EmployeeID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters[0].Value = employeeID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _roles.Add(reader.GetString(0));
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            

            return _roles;
        }


        public User SelectUserByEmail(string email)
        {
            User user = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_user_by_email";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            cmd.Parameters[0].Value = email;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User();
                    user.EmployeeID = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.PhoneNumber = reader.GetString(3);

                    if(!reader.IsDBNull(4))
                    {
                        user.LocationID = reader.GetInt32(4);
                    }

                    user.EmailAddress = reader.GetString(5);
                    user.Active = reader.GetBoolean(6);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not found", ex);
            }
            finally
            {
                conn.Close();
            }

            return user;
        } 

        public int InsertEmployee(User user)
        {
            int count = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_insert_employee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@fname", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@lname", SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@location", SqlDbType.Int);
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);

            cmd.Parameters["@fname"].Value = user.FirstName;
            cmd.Parameters["@lname"].Value = user.LastName;
            cmd.Parameters["@phone"].Value = user.PhoneNumber;
            if(user.LocationID > 0)
            {
                cmd.Parameters["@location"].Value = user.LocationID;
            }
            else
            {
                cmd.Parameters["@location"].SourceColumnNullMapping = true;
                cmd.Parameters["@location"].Value = DBNull.Value;

            }
            cmd.Parameters["@email"].Value = user.EmailAddress;

            try
            {
                conn.Open();

                count = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return count;
        }

    }
}
