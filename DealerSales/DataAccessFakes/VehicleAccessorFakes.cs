using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class VehicleAccessorFakes : IVehicleAccessor
    {
        List<Vehicle> _Vehicles = new List<Vehicle>();
        VehicleFactoryOptions _factoryOptions = null;

        public VehicleAccessorFakes()
        {
            _Vehicles.Add(new Vehicle
            {
                VehicleID = 100000,
                Make = "Chevy",
                Model = "Camaro",
                SubModel = "SS",
                Year = "2017",
                Cost = 21500.00M,
                ServiceCost = 400.00M,
                Vin = "1csda87fp3la8dk32",
                BodyStyle = "Coupe",
                Available = true
            });

            _Vehicles.Add(new Vehicle
            {
                VehicleID = 100001,
                Make = "Ford",
                Model = "Mustange",
                SubModel = "GT",
                Year = "2015",
                Cost = 19750.00M,
                ServiceCost = 2400.00M,
                Vin = "1lks93na76s9d04j2",
                BodyStyle = "Coupe",
                Available = true
            });

            _factoryOptions = new VehicleFactoryOptions() {
                VehicleID = 100000,
                EngineSize = "6.2L",
                CylinderCount = 8,
                Transmission = "Manual",
                DriveLine = "RWD",
                InteriorMaterial = "Cloth",
                InteriorColor = "Gray",
                ExteriorColor = "Midnight Gray Metallic"
            };

        }

        public int AddFactoryOptions(VehicleFactoryOptions factoryOptions)
        {
            throw new NotImplementedException();
        }

        public int AddVehicle(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public bool RemoveVehicle(int vehicleID)
        {
            throw new NotImplementedException();
        }

        public VehicleFactoryOptions RetreiveFactoryOptionsByVehicleID(int vehicleID)
        {
            VehicleAccessorFakes fakes = new VehicleAccessorFakes();

            return fakes._factoryOptions;
        }



        public List<Vehicle> RetrieveAllAvailableVehicles()
        {

            return _Vehicles;
        }

        public Vehicle RetrieveVehicleByVehicleID(int vehicleID)
        {
            throw new NotImplementedException();
        }
    }
}
