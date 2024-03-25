using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class LocationManager : ILocationManager
    {
        ILocationManager _locationManager;

        public LocationManager()
        {
            _locationManager = new LocationManager();
        }

        public List<Location> GetAllLocation()
        {
            
            try
            {
                return _locationManager.GetAllLocation();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not load location data", ex);
            }
        }



    }
}
