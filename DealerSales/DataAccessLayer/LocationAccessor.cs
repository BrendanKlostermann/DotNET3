using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class LocationAccessor : ILocationAccessor
    {
     
        public List<Location> SelectAllLocations()
        {
            List<Location> locations = new List<Location>();
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_customer_by_customerID";
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
                        Location location = new Location();
                        location.LocationID = reader.GetInt32(0);
                        location.ZipCode = reader.GetInt32(1);
                        location.StreetAddress = reader.GetString(2);
                        location.LocationName = reader.GetString(3);
                        location.PhoneNumber = reader.GetString(4);

                        locations.Add(location);

                    }
                }


                return locations;

            }
            catch(SqlException ex)
            {
                throw new Exception("Could not find location data", ex);
            }
            finally
            {
                conn.Close();
            }

        }

    }
}
