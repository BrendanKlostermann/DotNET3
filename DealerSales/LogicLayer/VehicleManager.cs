using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessFakes;
using DataAccessLayer;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class VehicleManager : IVehicleManager
    {
        List<Vehicle> _vehicles = null;
        VehicleFactoryOptions factoryOptions = null;
        Vehicle _vehicle = null;

        public int AddVehicle(Vehicle vehicle)
        {
            VehicleAccessor vehicleAccessor = new VehicleAccessor();
            try
            {
                return vehicleAccessor.AddVehicle(vehicle);
            }
            catch (Exception ex)
            {
                throw new Exception("Vehicle could not be added.", ex);
            }
        }

        public bool AddVehicleOptions(VehicleFactoryOptions options)
        {
            VehicleAccessor vehicleAccessor = new VehicleAccessor();
            try
            {
                if (1 == vehicleAccessor.AddFactoryOptions(options))
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
                throw new Exception("Vehicle could not be added.", ex);
            }
        }

        public bool DeleteVehicle(int vehicleID)
        {
            try
            {
                VehicleAccessor vehicleAccessor = new VehicleAccessor();
                return vehicleAccessor.RemoveVehicle(vehicleID);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Vehicle could not be deleted", ex);
            }

        }

        public List<Vehicle> GetAllAvailableVehicles()
        {
            _vehicles = new List<Vehicle>();

            try
            {
                VehicleAccessor _vehicleAccessor = new VehicleAccessor();
                _vehicles = _vehicleAccessor.RetrieveAllAvailableVehicles();
            } 
            catch(Exception ex)
            {
                throw new ApplicationException("Vehicles not found", ex);
            }
            

            return _vehicles;
        }

        public Vehicle GetVehicleByVehicleID(int vehicleID)
        {
            try
            {
                _vehicle = new Vehicle();

                VehicleAccessor vehicleAccessor = new VehicleAccessor();
                _vehicle = vehicleAccessor.RetrieveVehicleByVehicleID(vehicleID);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Vehicle ID " + vehicleID + " could not be found", ex);
            }
            

            return _vehicle;
            
        }

        public VehicleFactoryOptions GetVehicleFactoryOptionsByVehicleID(int vehicleID)
        {
            factoryOptions = new VehicleFactoryOptions();

            try
            {
                VehicleAccessor _vehicleAccessor = new VehicleAccessor();
                factoryOptions = _vehicleAccessor.RetreiveFactoryOptionsByVehicleID(vehicleID);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find vehicle options.", ex);
            }
            


            return factoryOptions;
        }
    }
}
