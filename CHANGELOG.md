# Changelog
All notable changes to this project will be documented in this file.

## [1.0.1.12] - 2021-09-24

### Changed

The file [RouteParametersQuery.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/RouteParametersQuery.cs)  
– Added the property: DuplicateRoutesId.

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs)  
– Changed the class DuplicateRouteResponse;
- Changed the method DuplicateRoute;
- Fixed error notification (was non-informative) issue in the generic methods GetJsonObjectFromAPIAsync, GetJsonObjectFromAPI, GetXmlObjectFromAPI.

## [1.0.1.11] - 2021-09-01

### Changed

The file [Order.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Order.cs)
– Changed the property day_scheduled_for_YYMMDD to day_scheduled_for_YYYYMMDD.

The file [FastBulkGeocoding.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/FastProcessing/FastBulkGeocoding.cs)
– Removed non-used variables. 
- Fixed typos Chank to Chunk. 

The file [FastValidateData.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/FastProcessing/FastValidateData.cs)
– Removed non-used variables.  
- Fixed typos Chank to Chunk. 

The file [FastFileReading.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/FastProcessing/FastFileReading.cs)
– Removed non-used variables. 

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs)
– In the class UpdateRouteDestinationRequest was sat as shadowed properties: RouteId, OptimizationProblemId, RouteDestinationId. 

## [1.0.1.9] - 2021-08-18

### Changed

The file [Address.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataContractResolver.cs)
– Added mandatory field option/feature.  

The file [RouteAdvancedConstraints.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/V5/RouteAdvancedConstraints.cs)  
– **Class (API 5 version)**: RouteAdvancedConstraints  - added the property LocationSequencePattern.  

The file [FastBulkGeocoding.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/FastProcessing/FastBulkGeocoding.cs)
– Added large CSV file reading/update feature.  

The file [FastFileReading.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/FastProcessing/FastFileReading.cs)
– Added large CSV file reading feature.  

The file [PropertyValidation.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/PropertyValidation.cs)
– Enhanced the variable validator.  

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Route4MeManager.cs)
– Changed the method: GetAddressBookContactsByGroup;  
- Added the method: GetAddressBookContactsByCustomField.  

The file [Utils.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs)
– Added the method: SerializeObjectToJson(object obj, string[] mandatoryFields).  


### Added

The file [AddressBookSearchResponse.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/AddressBookSearchResponse.cs)  
– **Class**: AddressBookSearchResponse  

The file [FastBulkRemoveContacts.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/FastProcessing/FastBulkRemoveContacts.cs)
– **Class**: FastBulkRemoveContacts  

The file [FastValidateData.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/FastProcessing/FastValidateData.cs)
– **Class**: FastValidateData  


## [1.0.1.8] - 2021-08-10

### Changed

The file [Address.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/Address.cs)
– Changed the type of the property Joint form int? to bool?  
The file [Address.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/V5/Address/Address.cs)
– Changed the type of the property Joint form int? to bool?  

## [1.0.1.7] - 2021-07-24

### Changed

The file [FastBulkGeocoding.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/FastProcessing/FastBulkGeocoding.cs)
– Added large CSV file uploading function (to be finished in next versions)

The file [FastFileReading.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/FastProcessing/FastFileReading.cs)
– Added large CSV file uploading function (to be finished in next versions) (API 5 version)  

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
    1. The method [TValue ToObject<TValue>(object obj, out string errorString)](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/Utils.cs#L556) - remade using try-catch.
- The class [NoteParameters](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/QueryTypes/NoteParameters.cs)- added properties for form data.

