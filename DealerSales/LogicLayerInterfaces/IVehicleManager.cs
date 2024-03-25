using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IVehicleManager
    {
        List<Vehicle> GetAllAvailableVehicles();
        VehicleFactoryOptions GetVehicleFactoryOptionsByVehicleID(int vehicleID);

        Vehicle GetVehicleByVehicleID(int vehicleID);

        bool DeleteVehicle(int vehicleID);

        bool AddVehicleOptions(VehicleFactoryOptions options);

        int AddVehicle(Vehicle vehicle);

    }


}
