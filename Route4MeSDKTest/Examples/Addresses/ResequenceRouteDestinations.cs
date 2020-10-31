using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void ResequenceRouteDestinations(DataObjectRoute route)
    {
      if (route.Addresses == null && route.Addresses.Length < 3)
      {
        Console.WriteLine("ResequenceRouteDestinations error {0}", "route.Addresses == null && route.Addresses.Length < 3. Number of addresses should be >= 3");
        return;
      }

      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

      // Switch 2 addresses after departure address:

      AddressesOrderInfo addressesOrderInfo = new AddressesOrderInfo();
      addressesOrderInfo.RouteId = route.RouteID;
      addressesOrderInfo.Addresses = new AddressInfo[0];
      for (int i = 0; i < route.Addresses.Length; i++)
      {
        Address address = route.Addresses[i];
        AddressInfo addressInfo = new AddressInfo();
        addressInfo.DestinationId = address.RouteDestinationId.Value;
        addressInfo.SequenceNo = i;
        if (i == 1)
          addressInfo.SequenceNo = 2;
        else if (i == 2)
          addressInfo.SequenceNo = 1;
        addressInfo.IsDepot = (addressInfo.SequenceNo == 0);
        List<AddressInfo> addressesList = new List<AddressInfo>(addressesOrderInfo.Addresses);
        addressesList.Add(addressInfo);
        addressesOrderInfo.Addresses = addressesList.ToArray();
      }

      // Run the query
      string errorString1 = "";
      DataObjectRoute route1 = route4Me.GetJsonObjectFromAPI<DataObjectRoute>(addressesOrderInfo,
                                                                                          R4MEInfrastructureSettings.RouteHost,
                                                                                          HttpMethodType.Put,
                                                                                          out errorString1);

      // Output the result
      PrintExampleOptimizationResult("ResequenceRouteDestinations, switch 2 addresses.", route1, errorString1);
      Console.WriteLine("");
    }

    [DataContract]
    private class AddressInfo : GenericParameters
    {
      [DataMember(Name = "route_destination_id")]
      public int DestinationId { get; set; }

      [DataMember(Name = "sequence_no")]
      public int SequenceNo { get; set; }

      [DataMember(Name = "is_depot")]
      public bool IsDepot { get; set; }
    }

    [DataContract]
    private class AddressesOrderInfo : GenericParameters
    {
      [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
      public string RouteId { get; set; }

      [DataMember(Name = "addresses")]
      public AddressInfo[] Addresses { get; set; }
    }

  }
}
