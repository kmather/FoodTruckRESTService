using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FoodTruckRESTService
{
    public class TruckRESTService : ITruckRESTService
    {
        /// <summary>
        /// Gets a list of trucks near lat, lng within radius miles
        /// </summary>
        public List<Truck> GetNearbyTrucks(double lat, double lng, double radius)
        {
            var trucks = Trucks.NearbyTrucks(lat, lng, radius);
            return trucks;
        }
    }
}
