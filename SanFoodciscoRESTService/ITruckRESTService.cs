using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FoodTruckRESTService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" 
    //       in both code and config file together.
    [ServiceContract]
    public interface ITruckRESTService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Bare,
                    UriTemplate = "GetNearbyTrucks?lat={lat}&lng={lng}&radius={radius}")]
        List<Truck> GetNearbyTrucks(double lat, double lng, double radius);
    }
}
