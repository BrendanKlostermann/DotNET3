using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IVehicleAccessor
    {
        List<Vehicle> RetrieveAllAvailableVehicles();

        VehicleFactoryOptions RetreiveFactoryOptionsByVehicleID(int vehicleID);

        Vehicle RetrieveVehicleByVehicleID(int vehicleID);

        bool RemoveVehicle(int vehicleID);

        int AddVehicle(Vehicle vehicle);

        int AddFactoryOptions(VehicleFactoryOptions factoryOptions);
     }

}
