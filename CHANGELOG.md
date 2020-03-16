# Changelog
All notable changes to this project will be documented in this file.

## [1.0.0.5] - 2020-03-09

### Added

- The helper class [DataContractResolver](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataContractResolver.cs) - for assigning the null values to the properties.
- The class [Route4MeDynamicClass](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Route4MeDynamicClass.cs) - for creating a dynamic class from an existing class by copying only specified properties.
- The method [UpdateAddressBookContact](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L2186) - for updating the Address Book Contact with data with containg null values.
- The method [SerializeObjectToJson](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L71) - for serializing an object with/without null values of the properties.
- The class [ReadOnlyAttribute](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/ReadOnlyAttribute.cs) - for creating custom attribute ReadOnlyAttribute to distinguish the object properties while updating.
- The method [UpdateRoute(DataObjectRoute route, DataObjectRoute initialRoute, out string errorString)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L377) - for updating a route by sending the initial route and cloned-modified route. The algorithm compares the routes, finds modified properties and updates them in the Route4Me database.
- The class [Utils](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs). Modified the methods:
    1. The method [ObjectDeepClone<T>(T obj)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L182) - for clonning a Route4Me object.
	2. The method [GetPropertiesWithDifferentValues(object modifiedObject, object initialObject, out string errorString, bool excludeReadonly = true)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L216) - compares initial and modified Route4Me objects and returns a list of the modified properties.
	3. The method [IsPropertyDictionary](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L334) - checks if a Route4Me object property is a dictionary type.
	4. The method [IsPropertyObject](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L349) - checks if a Route4Me object property is a nested object type.
	5. The method [IsPropertyArray](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L371) - checks if a Route4Me object property is an array type.
	6. The method [IsDictionariesEqual](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L384) - checks if the dictionaries are equal.
	7. The method [CheckIfPropertyHasIgnoreAttribute](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L430) - checks if a Route4Me object property has the attribute IgnoreDataMemberAttribute.
	8. The method [CheckIfPropertyHasReadOnlyAttribute](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L442) - checks if a Route4Me object property has the attribute ReadOnllyAttribute.

### Changed

- In the Utils.cs changed the method [SerializeObjectToJson](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L65) - for asuring to get correct serialization: if serialization with the DataContractJsonSerializer is failing, serialization with the SerializeObjectToJson will be done.
- In the AddressBookContact.cs changed the property [address_custom_data](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/AddressBookContact.cs#L165) - filters wrong data type for this property.
- in the Route4MeManager changed:
    1. the method [GetJsonObjectFromAPI](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L3423) - modified for filtering the wrong data type for the property address_custom_data.
	2. 
- In the AddressBookContact.cs changed type of the properties from object type to:
	1. AddressCube -> double?
	2. AddressPieces -> int?
	3. AddressReferenceNo -> string
	4. AddressRevenue -> double?
	5. AddressWeight -> double?
	6. AddressCustomerPo -> string
	
- The method [GetUserLocations](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L1041) - changed returning object type from Dictionary<string,UserLocation> to UserLocation[].
- The method [DuplicateRoute](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L415) - as the returning parameter response.OptimizationProblemId actually is route_id, now the method returns it as route ID.
- The class [Address](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Address.cs) - added the custom attribute ReadOnlyAttribute to some properties.
- The class [Route](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Route.cs) - added the custom attribute ReadOnlyAttribute to some properties.
- The class [VehicleV4Response](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/VehicleV4Response.cs) - all the properties type changed to the string.


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
