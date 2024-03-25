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
    public class ZipCodeAccessor : IZipCodeAccessor
    {
        List<ZipCode> _zipcodes = null;
        ZipCode _zipCode = null;

        public List<ZipCode> RetrieveAllZipCodes()
        {
            _zipcodes = new List<ZipCode>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_all_ZipCode";
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
                        _zipCode = new ZipCode();

                        _zipCode.Zipcode = reader.GetInt32(0);
                        _zipCode.City = reader.GetString(1);
                        _zipCode.State = reader.GetString(2);

                        _zipcodes.Add(_zipCode);

                    }
                }

            }
            catch(Exception e)
            {
                throw new ApplicationException("Could not find ZipCodes", e);
            }
            finally
            {
                conn.Close();
            }


            return _zipcodes;
        }
    }
}
