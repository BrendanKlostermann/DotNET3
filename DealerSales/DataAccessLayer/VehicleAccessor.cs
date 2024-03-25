using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class VehicleAccessor : IVehicleAccessor
    {
        Vehicle vehicle = null;
        List<Vehicle> vehicles = null;
        VehicleFactoryOptions factoryOptions = null;

        public int AddFactoryOptions(VehicleFactoryOptions factoryOptions)
        {
            int rowcount = 0;
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_insert_factory_options";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vehicleID", factoryOptions.VehicleID);
            cmd.Parameters.AddWithValue("@engineSize", factoryOptions.EngineSize);
            cmd.Parameters.AddWithValue("@cylinderCount", factoryOptions.CylinderCount);
            cmd.Parameters.AddWithValue("@transmission", factoryOptions.Transmission);
            cmd.Parameters.AddWithValue("@driveLine", factoryOptions.DriveLine);
            cmd.Parameters.AddWithValue("@interiorMaterial", factoryOptions.InteriorMaterial);
            cmd.Parameters.AddWithValue("@interiorColor", factoryOptions.InteriorColor);
            cmd.Parameters.AddWithValue("@exteriorColor", factoryOptions.ExteriorColor);

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowcount;

        }

        public int AddVehicle(Vehicle vehicle)
        {

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_insert_vehicle";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@make", vehicle.Make);
            cmd.Parameters.AddWithValue("@model", vehicle.Model);
            cmd.Parameters.AddWithValue("@submodel", vehicle.SubModel);
            cmd.Parameters.AddWithValue("@modelYear", vehicle.Year);
            cmd.Parameters.AddWithValue("@cost", vehicle.Cost);
            cmd.Parameters.AddWithValue("@serviceCost", vehicle.ServiceCost);
            cmd.Parameters.AddWithValue("@miles", vehicle.Mileage);
            cmd.Parameters.AddWithValue("@vin", vehicle.Vin);
            cmd.Parameters.AddWithValue("@bodyStyle", vehicle.BodyStyle);
            cmd.Parameters.AddWithValue("@available", vehicle.Available);

            try
            {
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveVehicle(int vehicleID)
        {
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_deactivate_vehicle";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@vehID", SqlDbType.Int);
            cmd.Parameters[0].Value = vehicleID;

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
                throw ex;
            }


            throw new NotImplementedException();
        }

        public VehicleFactoryOptions RetreiveFactoryOptionsByVehicleID(int vehicleID)
        {
            factoryOptions = new VehicleFactoryOptions();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_factory_options_by_vehicleID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@VehID", SqlDbType.Int);
            cmd.Parameters[0].Value = vehicleID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        factoryOptions = new VehicleFactoryOptions();

                        factoryOptions.VehicleID = vehicleID;
                        factoryOptions.EngineSize = reader.GetString(0);
                        factoryOptions.CylinderCount = reader.GetInt32(1);
                        factoryOptions.Transmission = reader.GetString(2);
                        factoryOptions.DriveLine = reader.GetString(3);
                        factoryOptions.InteriorMaterial = reader.GetString(4);
                        factoryOptions.InteriorColor = reader.GetString(5);
                        factoryOptions.ExteriorColor = reader.GetString(6);

                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicle not found", ex);
            }
            finally
            {
                conn.Close();
            }


            return factoryOptions;
        }

        public List<Vehicle> RetrieveAllAvailableVehicles()
        {
            vehicles = new List<Vehicle>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_all_available_vehicles";
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
                        vehicle = new Vehicle();

                        vehicle.VehicleID = reader.GetInt32(0);
                        vehicle.Vin = reader.GetString(1);
                        vehicle.Make = reader.GetString(2);
                        vehicle.Model = reader.GetString(3);
                        vehicle.SubModel = reader.GetString(4);
                        vehicle.Year = reader.GetString(5);
                        vehicle.Mileage = reader.GetInt32(6);
                        vehicle.BodyStyle = reader.GetString(7);
                        vehicle.Cost = reader.GetDecimal(8);
                        vehicle.ServiceCost = reader.GetDecimal(9);
                        vehicle.Available = reader.GetBoolean(10);


                        vehicles.Add(vehicle);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Vehicles not found", ex);
            }
            finally
            {
                conn.Close();
            }

            return vehicles;
        } // End select all vehicles

        public Vehicle RetrieveVehicleByVehicleID(int vehicleID)
        {
            vehicle = new Vehicle();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetDBConnection();
            var cmdText = "sp_select_vehicle_by_vehicleID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@VehID", SqlDbType.Int);
            cmd.Parameters[0].Value = vehicleID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehicle.VehicleID = reader.GetInt32(0);
                        vehicle.Vin = reader.GetString(1);
                        vehicle.Make = reader.GetString(2);
                        vehicle.Model = reader.GetString(3);
                        vehicle.SubModel = reader.GetString(4);
                        vehicle.Year = reader.GetString(5);
                        vehicle.Mileage = reader.GetInt32(6);
                        vehicle.BodyStyle = reader.GetString(7);
                        vehicle.Cost = reader.GetDecimal(8);
                        vehicle.ServiceCost = reader.GetDecimal(9);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Vehicle not found", ex);
            }
            finally
            {
                conn.Close();
            }

            return vehicle;
            
        }
    }
}
