using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataAccessInterfaces;

namespace DataAccessLayer
{
    public class DBConnection : IDBConnection
    {
        public SqlConnection GetDBConnection()
        {
            SqlConnection conn = null;
            string connectionString = @"Data Source=localhost;Initial Catalog=dealershipDB;Integrated Security=True";
            conn = new SqlConnection(connectionString);
            return conn;
        }


    }
}
