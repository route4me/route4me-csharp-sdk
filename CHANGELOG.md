# Changelog
All notable changes to this project will be documented in this file.

## [1.0.0.5] - 2020-03-09

### Added

- The helper class [DataContractResolver](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataContractResolver.cs) - for assigning the null values to the properties.
- The class [Route4MeDynamicClass](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Route4MeDynamicClass.cs) - for creating a dynamic class from an existing class by copying only specified properties.
- The method [UpdateAddressBookContact](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L2186) - for updating the Address Book Contact with data with containg null values.
- The method [SerializeObjectToJson](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L71) - for serializing an object with/without null values of the properties.

### Changed

- In the AddressBookContact.cs changed type of the properties from object type to:
	1. AddressCube -> double?
	2. AddressPieces -> int?
	3. AddressReferenceNo -> string
	4. AddressRevenue -> double?
	5. AddressWeight -> double?
	6. AddressCustomerPo -> string
	
- The method [GetUserLocations](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L1041) - changed returning object type from Dictionary<string,UserLocation> to UserLocation[].
- The method [DuplicateRoute](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L415) - as the returning parameter response.OptimizationProblemId actually is route_id, now the method returns it as route ID.

## [1.0.0.4] - 2020-01-20

### Added

- The response class [VehicleV4CreateResponse](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/VehicleV4CreateResponse.cs) for new vehicle creating process using the endpoint /api.v4/vehicle.php.

- The method [UpdateOptimizationDestination](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L905) to the Route4MeManager class for updating an optimization destination by sending changed Address object.

- The method [AddOptimizationDestinations](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L1756) to the Route4MeManager class for adding the optimization destinations by sending array of the Address objectS.

### Changed

- The route address class [Address](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Address.cs): was changed some field types from object type to standard type.

- The enumaration types [Enum](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Enums.cs): the enumeration types cahnged according to current state of the web application.

- the route parameters class [RouteParameters](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/RouteParameters.cs): changed the type of the field RouteTime: from object to int?.

- the route manager class [Route4MeManager](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs): In this class were done important changes in order to be able to update/add the destination(s) to an optimization.

- The method [GetAddressBookLocation](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L2065) in the Route4MeManager class - before, it was necessary to specify the AddressId parameter in the form of a list of address identifiers separated by commas, even in the case of extracting one location (for example, "52908503,52908503") - fixed.

- The method [CreateVehicle](LL) in the Route4MeManager class - now it returns a response of type VehicleV4CreateResponse.
