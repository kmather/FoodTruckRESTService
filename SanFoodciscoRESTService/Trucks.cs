using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace FoodTruckRESTService
{
    #region Data Classes

    /// <summary>
    /// Data classes created using http://json2csharp.com/
    /// </summary>
    public class Location
    {
        public bool needs_recoding { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    [DataContract]
    public class Truck
    {
        [DataMember]
        public Location location { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string expirationdate { get; set; }
        [DataMember]
        public string permit { get; set; }
        [DataMember]
        public string block { get; set; }
        [DataMember]
        public string received { get; set; }
        [DataMember]
        public string facilitytype { get; set; }
        [DataMember]
        public string blocklot { get; set; }
        [DataMember]
        public string locationdescription { get; set; }
        [DataMember]
        public string cnn { get; set; }
        [DataMember]
        public string priorpermit { get; set; }
        [DataMember]
        public string approved { get; set; }
        [DataMember]
        public string schedule { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string applicant { get; set; }
        [DataMember]
        public string lot { get; set; }
        [DataMember]
        public string fooditems { get; set; }
        [DataMember]
        public string longitude { get; set; }
        [DataMember]
        public string latitude { get; set; }
        [DataMember]
        public string objectid { get; set; }
        [DataMember]
        public string y { get; set; }
        [DataMember]
        public string x { get; set; }
    }

    #endregion

    public static class Trucks
    {
        // Service specific to SF
        public static string url = "http://data.sfgov.org/resource/rqzj-sfat.json";
        public static double defaultLatitude = 37.7749300;	
        public static double defaultLongitude = -122.4194200;
        public static double defaultRadius = 1; // Miles

        /// <summary>
        /// Get a list of food trucks near a specific point
        /// </summary>
        public static List<Truck> NearbyTrucks(double lat, double lng, double radius)
        {
            var result = new List<Truck>();
            double tLat, tLng;

            // Get a list of all food trucks
            var trucks = GetTrucks();

            // Return food trucks meeting a certain criteria
            // TODO: Currently searches by proximity. Add other filters later?
            foreach (var t in trucks)
            {
                Double.TryParse(t.latitude, out tLat);
                Double.TryParse(t.longitude, out tLng);

                if (GeoCodeCalc.CalcDistance(lat, lng, tLat, tLng) < radius)
                    result.Add(t);
            }
            return result;
        }


        #region Helper Methods

        /// <summary>
        /// Gets all listed food trucks
        /// TODO: In the future, store the truck list in dB and update the list periodically and async
        /// </summary>
        private static List<Truck> GetTrucks()
        {            
            try
            {
                // Consume the SODA API
                var client = new WebClient();
                string response = client.DownloadString(url);

                // Parse the JSON Response
                var serializer = new DataContractJsonSerializer(typeof(List<Truck>));

                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(response)))
                {
                    var truckData = (List<Truck>)serializer.ReadObject(ms);
                    return truckData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetTrucks(): " + ex.Message);
                return new List<Truck>();
            }
        }

        #endregion
    }
}