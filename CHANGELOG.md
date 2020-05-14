# Changelog
All notable changes to this project will be documented in this file.

## [1.0.0.6] - 2020-05-14

### Added

- The class [Utils](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs). Added the methods:
	1. The method [ConvertObjectToType<T>(ref object value)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L584) - for converting a value to the specified type.
	2. The method [ConvertObjectToPropertyType(object value, PropertyInfo targetProperty)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L683) - for converting a value to the specified type.
	

### Changed

- in the The class [Route4MeDynamicClass](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Route4MeDynamicClass.cs) changed:
	1. The method [SearchAddressBookLocation(AddressBookParameters addressBookParameters, out List<AddressBookContact> contactsFromObjects, out string errorString)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L2383) - searhing the locations with showing specified fields.
	2. The method [SearchAddressBookLocation(AddressBookParameters addressBookParameters, out List<AddressBookContact> contactsFromObjects, out string errorString)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L2383) - added algorithm of managing wrong data.
	3. The method [UpdateRoute(DataObjectRoute route, DataObjectRoute initialRoute, out string errorString)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L395) - handled issues: ApprovedForExecution, depots, sequencing.
	4. Added the method [AddAddressNote(NoteParameters noteParameters, out string errorString)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L1593) - for adding complex address note (send text content, custom note, file at once) to a route address.
	
- The class [Utils](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs). Modified the methods:
    1. The method [TValue ToObject<TValue>(object obj, out string errorString)](***) - remade using try-catch.
- The class [NoteParameters](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L556)- added properties for form data.



## [1.0.0.5] - 2020-03-09

### Added

- The helper class [DataContractResolver](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataContractResolver.cs) - for assigning the null values to the properties.
- The class [Route4MeDynamicClass](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Route4MeDynamicClass.cs) - for creating a dynamic class from an existing class by copying only specified properties.
- The method [UpdateAddressBookContact](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L2330) - for updating the Address Book Contact with data with containg null values.
- The method [UpdateAddressBookContact](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L2358) - for updating the Address Book Contact by sending modified address book contact object.
- The method [SerializeObjectToJson](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L125) - for serializing an object with/without null values of the properties.
- The class [ReadOnlyAttribute](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/ReadOnlyAttribute.cs) - for creating custom attribute ReadOnlyAttribute to distinguish the object properties while updating.
- The method [UpdateRoute(DataObjectRoute route, DataObjectRoute initialRoute, out string errorString)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L377) - for updating a route by sending the initial route and cloned-modified route. The algorithm compares the routes, finds modified properties and updates them in the Route4Me database.
- The class [Utils](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs). Modified the methods:
    1. The method [ObjectDeepClone<T>(T obj)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L194) - for clonning a Route4Me object.
	2. The method [GetPropertiesWithDifferentValues(object modifiedObject, object initialObject, out string errorString, bool excludeReadonly = true)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L228) - compares initial and modified Route4Me objects and returns a list of the modified properties.
	3. The method [IsPropertyDictionary](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L346) - checks if a Route4Me object property is a dictionary type.
	4. The method [IsPropertyObject](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L361) - checks if a Route4Me object property is a nested object type.
	5. The method [IsPropertyArray](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L383 - checks if a Route4Me object property is an array type.
	6. The method [IsDictionariesEqual](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L396) - checks if the dictionaries are equal.
	7. The method [CheckIfPropertyHasIgnoreAttribute](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L442) - checks if a Route4Me object property has the attribute IgnoreDataMemberAttribute.
	8. The method [CheckIfPropertyHasReadOnlyAttribute](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L454) - checks if a Route4Me object property has the attribute ReadOnllyAttribute.
	9. The method [ReadObjectNew<T>](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs) - for reading Route4Me object from a JSON string.
	10. The method [GetPropertyPositions<T>](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L472) - for getting positions of the Route4Me object properties.
	11. The method [OrderPropertiesByPosition<T>](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L491) - for the ordering of a property names list by the positions in the object.
	12. The method [ToObject<TValue>](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L524) - for converting anonymous type object to a specified Route4Me object type.

### Changed

- In the Utils.cs changed the method [SerializeObjectToJson](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L89) - for asuring to get correct serialization: if serialization with the DataContractJsonSerializer is failing, serialization with the SerializeObjectToJson will be done.
- in the Route4MeManager changed:
    1. the method [GetJsonObjectFromAPI](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L3774) - modified for filtering the wrong data type for the property address_custom_data.
	2. the method [SearchAddressBookLocation](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L2224) - fixed problem with the consumption of the response with the anonymous type object arrays.
- In the class AddressBookContact.cs changed the property [address_custom_data](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/AddressBookContact.cs#L165) - filters wrong data type for this property.
- In the [Address](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Address.cs) added:
	1. property [RouteName](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Address.cs#L112) - route name for a depot address.
	2. property [PathToNext](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Address.cs#L454) - array of the path points to a next stop.
- In the AddressBookContact.cs changed type of the properties from object type to:
	1. AddressCube -> double?
	2. AddressPieces -> int?
	3. AddressReferenceNo -> string
	4. AddressRevenue -> double?
	5. AddressWeight -> double?
	6. AddressCustomerPo -> string
	
- The method [GetUserLocations](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L1100) - changed returning object type from Dictionary<string,UserLocation> to UserLocation[].
- The method [DuplicateRoute](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L479) - as the returning parameter response.OptimizationProblemId actually is route_id, now the method returns it as route ID.
- The class [Address](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Address.cs) :
    1. added the custom attribute ReadOnlyAttribute to some properties.
	2. added the property [PathToNext](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Address.cs#L454).
- The class [Route](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Route.cs) - added the custom attribute ReadOnlyAttribute to some properties.
- The class [VehicleV4Response](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/VehicleV4Response.cs) - all the properties type changed to the string.


## [1.0.0.4] - 2020-01-20

### Added

- The response class [VehicleV4CreateResponse](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/VehicleV4CreateResponse.cs) for new vehicle creating process using the endpoint /api.v4/vehicle.php.

- The method [UpdateOptimizationDestination](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L974) to the Route4MeManager class for updating an optimization destination by sending changed Address object.

- The method [AddOptimizationDestinations](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L1841) to the Route4MeManager class for adding the optimization destinations by sending array of the Address objectS.

### Changed

- The route address class [Address](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Address.cs): was changed some field types from object type to standard type.

- The enumaration types [Enum](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Enums.cs): the enumeration types cahnged according to current state of the web application.

- the route parameters class [RouteParameters](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/RouteParameters.cs): changed the type of the field RouteTime: from object to int?.

- the route manager class [Route4MeManager](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs): In this class were done important changes in order to be able to update/add the destination(s) to an optimization.

- The method [GetAddressBookLocation](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs#L2152) in the Route4MeManager class - before, it was necessary to specify the AddressId parameter in the form of a list of address identifiers separated by commas, even in the case of extracting one location (for example, "52908503,52908503") - fixed.

- The method [CreateVehicle](LL) in the Route4MeManager class - now it returns a response of type VehicleV4CreateResponse.
