# Changelog
All notable changes to this project will be documented in this file.

## [1.1.1.0] - 2021-10-28

### Changed

- Route4MeSDKLibrary is Switched from .NET Core 3.1 to .NET Standard 2.0.
- Threading is optimized for Route4MeManager and Route4MeManagerV5
- 3rd party dependencies are updated and consolidated
- Code style is adjusted
- Code is cleaned up

## [1.1.0.0] - 2021-10-20

### Added

The file [BarcodeDataRequest.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/AddressBarcodes/BarcodeDataRequest.cs) 
– **Class**: BarcodeDataRequest - for saving bar codes. 

The file [BarcodeDataResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/AddressBarcodes/BarcodeDataResponse.cs) 
– **Class**: BarcodeDataResponse - for reading bar codes from response. 

The file [GetAddressBarcodesResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/AddressBarcodes/GetAddressBarcodesResponse.cs) 
– **Class**: GetAddressBarcodesResponse - response for get bar codes request. 

The file [GetAddressBarcodesParameters.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/AddressBarcodes/GetAddressBarcodesResponse.cs) 
– **Class**: GetAddressBarcodesParameters - Query parameters for get bar codes request. 

The file [SaveAddressBarcodesParameters.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/AddressBarcodes/SaveAddressBarcodesParameters.cs) 
– **Class**: SaveAddressBarcodesParameters - Query parameters for save bar codes request. 

## [1.0.2.0] - 2021-10-07

### Added

The file [HttpClientHolderManager.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/HttpClientHolderManager.cs) 
– **Class**: HttpClientHolderManager - for correct usage of HttpClient means re-use to avoid TCP handshake overhead and exhaust the available socket pool (also supports GC for not-in-use HttpClient instances). 

The file [HttpClientHolder.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/HttpClientHolder.cs) 
– **Class**: HttpClientHolder - holds the instance of HttpClient provided by HttpClientHolderManager.

The file [SequentialTimer.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/SequentialTimer.cs) 
– **Class**: SequentialTimer - wraps System.Threading.Timer and guarantees FIFO for timer callbacks (without race conditions).

### Changed

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs)  
– Switch from HttpClient per HTTP Request to usage of HttpClientHolderManager

The file [Route4MeManagerV5.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManagerV5.cs)  
– Switch from HttpClient per HTTP Request to usage of HttpClientHolderManager


## [1.0.1.11] - 2021-09-24

### Changed

The file [RouteParametersQuery.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/RouteParametersQuery.cs)  
– Added the property: DuplicateRoutesId.

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs)  
– Changed the class DuplicateRouteResponse;
- Changed the method DuplicateRoute;
- Fixed error notification (was non-informative) issue in the generic methods GetJsonObjectFromAPIAsync, GetJsonObjectFromAPI, GetXmlObjectFromAPI.


## [1.0.1.10] - 2021-09-19

### Changed

The file [OrderFilterParameters.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/OrderFilterParameters.cs)  
– Added the properties: Query, TrackingNumbers.
- Renamed the property Scheduled_for_YYMMDD to Scheduled_for_YYYYMMDD.

The file [Order.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Order.cs)
– Added the properties: AddressStopType, TrackingNumber.

The file [OptimizationParameters.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/OptimizationParameters.cs)  
– Added the properties: OrderTerritories, IdOnly.

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs)  
– Added the method RunOptimizationByOrderTerritories.


### Added

The file [OrderTerritories.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/OrderTerritories.cs) 
– **Class**: OrderTerritories - for optimization payload. 


## [1.0.1.9] - 2021-09-07

### Changed

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs)  
– Changed the WinHttpHandler reference and appropriate code with SocketsHttpHandler

The file [Route4MeManagerV5cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManagerV5.cs)  
– Changed the WinHttpHandler reference and appropriate code with SocketsHttpHandler

## [1.0.1.8] - 2021-09-01

### Changed

The file [Order.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Order.cs)
– Changed the property DayScheduledFor_YYMMDD to DayScheduledFor_YYYYMMDD. 

The file [FastBulkGeocoding.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/FastProcessing/FastBulkGeocoding.cs)
– Removed non-used variables. 
- Fixed typos Chank to Chunk. 

The file [FastFileReading.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/FastProcessing/FastFileReading.cs)
– Removed non-used variables. 

The file [FastValidateData.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/FastProcessing/FastValidateData.cs)
– Removed non-used variables. 
- Fixed typos Chank to Chunk. 

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs)  
– In the class UpdateRouteDestinationRequest was sat as shadowed properties: RouteId, OptimizationProblemId, RouteDestinationId. 


## [1.0.1.7] - 2021-08-18

### Changed

The file [FastFileReading.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/FastProcessing/FastFileReading.cs)
– **Class**: FastFileReading - added custom data field reading feature.  

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs)  
– Added the method: GetAddressBookContactsByCustomField;
- Changed the method: GetAddressBookContactsByGroup, BatchGeocodingAsync.  

The file [RouteAdvancedConstraints.cs: ](https://github.com/route4me/route4me-csharp-sdk/blob/master/Route4MeSDKLibrary/DataTypes/V5/RouteAdvancedConstraints.cs)  
– **Class (API 5 version)**: RouteAdvancedConstraints  - added the property LocationSequencePattern.  

### Added

The file [AddressBookSearchResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/AddressBookSearchResponse.cs)  
– **Class**: AddressBookSearchResponse  

The file [FastBulkRemoveContacts.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/FastProcessing/FastBulkRemoveContacts.cs)
– **Class**: FastBulkRemoveContacts   

## [1.0.1.6] - 2021-08-11

### Changed

The file [Route4MeManagerV5.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManagerV5.cs)
– Added the methods: GetAddressBookContacts, GetAddressBookContactById, GetAddressBookContactsByIds, AddAddressBookContact, BatchCreateAdressBookContacts.  
- Added the classes: AddressBookContactsRequest, BatchCreatingAddressBookContactsReque.  

The file [DataContractResolver.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataContractResolver.cs)  
– **Class**: DataContractResolver  

The file [FastBulkGeocoding.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/FastProcessing/FastBulkGeocoding.cs)
– Added large CSV file uploading function (to be finished in next versions)

The file [FastFileReading.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/FastProcessing/FastFileReading.cs)
– Added large CSV file uploading function (to be finished in next versions)  

The file [PropertyValidation.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/PropertyValidation.cs)
– **Class**: PropertyValidation  

The file [Address.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Address.cs)
– Changed the type of the property Joint form int? to bool?  
The file [Address.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Address/Address.cs)
– Changed the type of the property Joint form int? to bool?  

### Added

The file [AddressBookContact.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/AddressBookContact/AddressBookContact.cs)  
– **Class (API 5 version)**: AddressBookContact  

The file [AddressBookContactsResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/AddressBookContact/AddressBookContactsResponse.cs)  
– **Class (API 5 version)**: AddressBookContactsResponse  

The file [AddressBookParameters.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/AddressBookContact/AddressBookParameters.cs)  
– **Class (API 5 version)**: AddressBookParameters  

The file [FastValidateData.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/FastProcessing/FastValidateData.cs)
– **Class**: FastValidateData   

## [1.0.1.5] - 2021-06-04

### Changed

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs)
– The method GetVehicles: Changed response type from VehiclesPaginated to DataTypes.V5.Vehicle[].


## [1.0.1.3] - 2021-03-29

### Added

The file [Consts.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Consts.cs)  
– **Endpoints**: /vehicles, /vehicles/assign, /vehicles/execute, /vehicles/location, /vehicle-profiles, /vehicles/license, /vehicles/search, TelematicsVendorsHost, TelematicsRegisterHost, TelematicsConnection, TelematicsVendorsInfo

The file [Geocoding.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Address/Geocoding.cs)  
– **Class (API 5 version)**: Geocoding  

The file [Enums.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Enum.cs)  
– **Enumerations (API 5 version)**: FuelTypes, FuelConsumptionUnits, VehicleSizeUnits, VehicleWeightUnits, TelematicsVendorType.  

The file [Vehicle.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Vehicles/Vehicle.cs)  
– **Class (API 5 version)**: Vehicle  

The file [VehicleLocationResponse.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Vehicles/VehicleLocationResponse.cs)  
– **Class (API 5 version)**: VehicleLocationResponse  

The file [VehicleOrderResponse.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Vehicles/VehicleOrderResponse.cs)  
– **Class (API 5 version)**: VehicleOrderResponse  

The file [VehicleProfile.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Vehicles/VehicleProfile.cs)  
– **Class (API 5 version)**: VehicleProfile  

The file [VehicleProfilesResponse.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Vehicles/VehicleProfilesResponse.cs)  
– **Class (API 5 version)**: VehicleProfilesResponse  

The file [VehicleResponse.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Vehicles/VehicleResponse.cs)  
– **Class (API 5 version)**: VehicleResponse  

The file [VehicleTemporary.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Vehicles/VehicleTemporary.cs)  
– **Class (API 5 version)**: VehicleTemporary  

The file [VehicleTrackResponse.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Vehicles/VehicleTrackResponse.cs)  
– **Class (API 5 version)**: VehicleTrackResponse  

The file [VehicleOrderParameters.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/Vehicles/VehicleOrderParameters.cs)  
– **Class (API 5 version)**: VehicleOrderParameters  

The file [VehicleParameters.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/Vehicles/VehicleParameters.cs)  
– **Class (API 5 version)**: VehicleParameters  

The file [VehicleProfileParameters.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/Vehicles/VehicleProfileParameters.cs)  
– **Class (API 5 version)**: VehicleProfileParameters  

The file [VehicleSearchParameters.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/Vehicles/VehicleSearchParameters.cs)  
– **Class (API 5 version)**: VehicleSearchParameters  

The file [Route4MeManagerV5.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManagerV5.cs)  
– **Methods (API 5 version)**: GetRouteDataTableWithElasticSearch, CreateVehicle, GetPaginatedVehiclesList, DeleteVehicle, CreateTemporaryVehicle, ExecuteVehicleOrder, GetVehicleLocations, GetVehicleById, GetVehicleTrack, GetVehicleProfiles, CreateVehicleProfile, DeleteVehicleProfile, GetVehicleProfileById, GetVehicleByLicensePlate, SearchVehicles, UpdateVehicle, UpdateVehicleProfile.  

The file [AccountProfile.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/AccountProfile.cs)  
– **Class (API 5 version)**: AccountProfile  

The file [TelematicsConnection.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/TelematicsConnection.cs)  
– **Class**: TelematicsConnection  

The file [TelematicsRegisterMemberResponse.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/TelematicsRegisterMemberResponse.cs)  
– **Class**: TelematicsRegisterMemberResponse  

The file [TelematicsConnectionParameters.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/TelematicsConnectionParameters.cs)  
– **Class**: TelematicsConnectionParameters  

Added the unit tests for the 'Telematics GateWay API' endpoints (TelematicsGateWayAPI).

Added the unit tests for the 'Route4Me Vehicles API 5' endpoints (VehiclesApiTests).  


### Changed

The file [Consts.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/DriverReview.cs)
– Changed property types of of the class DriverReview: **Rating** (double? to int?), **Previous** (int? to string), **Next** (int? to string).  

The file [TelematicsVendors.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/TelematicsVendors.cs)  
– Added the properties: Description, WebsiteURL, ApiDocsURL.

The file [TelematicsVendorParameters.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/TelematicsVendorParameters.cs)  
– Added the properties: MemberID, isVirtual, ApiKey, ValidateRemoteCredentials, ApiToken.

The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs)  
– Added the methods: RegisterTelematicsMember, GetTelematicsConnections, CreateTelematicsConnection, DeleteTelematicsConnection, UpdateTelematicsConnection, GetTelematicsConnection.

The file [Route4MeManagerV5.cs: ](https://github.com/route4me/route4me-net-core/tree/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManagerV5.cs)  
– Added the methods: GetAccountProfile, GetAccountPreferedUnit, GetTelematicsConnections, GetTelematicsConnectionByToken, RegisterTelematicsConnection, DeleteTelematicsConnection, UpdateTelematicsConnection.


## [1.0.1.2] - 2021-02-06

### Added

Added the unit test project for the API 5 endpoints done in the Xunit 2.4.1.

The file [Consts.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Consts.cs)  
– **Endpoints**: MainHostWeb, Routes, RoutesDuplicate, RoutesMerge, RoutesPaginate, RoutesFallbackPaginate, RoutesFallbackDatatable, RoutesFallback, RoutesReindexCallback, RoutesDatatable, RoutesDatatableConfig, RoutesDatatableConfigFallback  

The file [Enums.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Enums.cs)  
– **Enumeration**: AddressStopType
	
The file [Address.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Address/Address.cs)  
– **Class (API 5 version)**: Address 

The file [*.*: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/DataObject.cs)
– **Class (API 5 version)**: DataObject 
	
The file [DataObjectBase.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/DataObjectBase.cs)  
– **Class (API 5 version)**: DataObjectBase 

The file [Enums.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Enum.cs)  
– **Enumeration (API 5 version)**: AddressStopType
	
The file [Route.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Routes/Route.cs)  
– **Class (API 5 version)**: Route 
	
The file [RouteDataTableConfigResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Routes/RouteDataTableConfigResponse.cs)  
– **Class (API 5 version)**: RouteDataTableConfigResponse 
	
The file [RouteDuplicateResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Routes/RouteDuplicateResponse.cs)  
– **Class (API 5 version)**: RouteDuplicateResponse
	
The file [RouteParameters.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Routes/RouteParameters.cs)  
– **Class (API 5 version)**: RouteParameters

The file [RoutesDeleteResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Routes/RoutesDeleteResponse.cs)  
– **Class (API 5 version)**: RoutesDeleteResponse
	
The file [StatusResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/StatusResponse.cs)  
– **Class (API 5 version)**: StatusResponse

The file [OptimizationParameters.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/OptimizationParameters.cs)  
– **Class (API 5 version)**: OptimizationParameters

The file [RouteFilterParameters.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/RouteFilterParameters.cs)  
– **Class (API 5 version)**: RouteFilterParameters

The file [RouteParametersQuery.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/RouteParametersQuery.cs)  
– **Class (API 5 version)**: RouteParametersQuery

The file [Route4MeManagerV5.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManagerV5.cs)  
– **Methods(API 5 version)**: RemoveAddressBookContacts, GetRoutes, GetAllRoutesWithPagination, GetPaginatedRouteListWithoutElasticSearch, GetRouteDataTableWithElasticSearch, GetRouteDatatableWithElasticSearch, GetRouteListWithoutElasticSearch, DuplicateRoute, DeleteRoutes, GetRouteDataTableConfig, GetRouteDataTableFallbackConfig, UpdateRoute, RunOptimization, RemoveOptimization, 
– **Classes (API 5 version)**: RemoveAddressBookContactsRequest, RemoveOptimizationResponse, RemoveOptimizationRequest 


## [1.0.1.1] - 2021-01-21

### Added

The file [Utils.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs)   
– **Methods**: ReadSetting, DDHHMM2Seconds
	
The file [RouteParameters.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/RouteParameters.cs)  
 **Properties**: Slowdowns (type SlowdownParams), IgnoreTw(bool?), is_dynamic_start_time(bool), Bundling(AddressBundling), AdvancedConstraints(V5.RouteAdvancedConstraints[]), AddressStopType(string), Group(string) <br>
 **Class**: SlowdownParams 

	
The file [Consts.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Consts.cs)  
– **Endpoints**: MemberCapabilities, ScheduleCalendar.   
– **Class**: R4MEInfrastructureSettingsV5
	
The file [Address.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Address.cs)  
– **Properties**: BundleCount(int), BundleItems(type BundledItemResponse[]), Tags(string[]), Pickup(string), Dropoff(string), Joint(int?), OrderInventory(V5.OrderInventory[])

The file [AddressBundling.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/AddressBundling.cs)  
– **Classes**: AddressBundling, BundledItemResponse
	
The file [Enums.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Enums.cs)  
– **Enumerations**: AddressBundlingMode, AddressBundlingMergeMode, AddressBundlingFirstItemMode, AddressBundlingAdditionalItemsMode
	
The file [Enums.cs (API 5 version): ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Enum.cs)  
– **Enumerations**: AlgorithmType, TravelMode, DistanceUnit, Avoid, Optimize, Metric, DeviceType, Format, OptimizationState, RoutePathOutput, StatusUpdateType, TerritoryType, AddressBundlingMode, AddressBundlingMergeMode, AddressBundlingFirstItemMode, AddressBundlingAdditionalItemsMode, MemberTypes

The file [MemberCapabilities.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/MemberCapabilities.cs)  
– **Class**: MemberCapabilities
	
The file [Route.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Route.cs)  
– **Properties**: BundleItems(type BundledItemResponse[]), RouteWeight(double?), RouteCube(double?), RoutePieces(int?)

The file [RouteParametersQuery.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/RouteParametersQuery.cs)  
– **Properties**: BundlingItem(bool?), UnlinkFromMasterOptimization(bool), OrderInventory(bool?), Remaining(bool?)
	 
The file [Route4MeManager.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs)  
– **Methods**: GetMemberCapabilities, GetScheduleCalendar, MemberHasCommercialCapability  
– **Class**: SearchOrdersResponse
	
The file [ScheduleCalendarResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/ScheduleCalendarResponse.cs)  
– **Class**: ScheduleCalendarResponse
	
The file [ScheduleCalendarQuery.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/ScheduleCalendarQuery.cs)  
– **Class**: ScheduleCalendarQuery
	
The file [MergeRoutesQuery.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/MergeRoutesQuery.cs)  
– **Properties**: ToRouteId(string), RouteDestinationId(string), RouteDestinationIDs(string)

The file [DriverReview.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/DriverReview.cs)  
– **Classes (API 5 version)**: DriverReview, DriverReviewsResponse, SimplePaginationData, TypeQuantity

The file [ResultResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/ResultResponse.cs)  
– **Class (API 5 version)**: ResultResponse
	
The file [TeamResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/TeamResponse.cs)  
– **Class (API 5 version)**: TeamResponse
	
The file [HttpClientExtensions.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/HttpClientExtensions.cs)  
– **Class**: HttpClientExtensions
	
The file [DriverReviewParameters.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/DriverReviewParameters.cs)  
– **Class (API 5 version)**: DriverReviewParameters
	
The file [TeamRequest.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/V5/TeamRequest.cs)  
– **Class (API 5 version)**: TeamRequest
	
The file [Route4MeManagerV5.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManagerV5.cs)  
– **Class (API 5 version)**: Route4MeManagerV5  
– **Method (API 5 version)**: AddSkillsToDriver, 	
	
The file [RouteAdvancedConstraints.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/RouteAdvancedConstraints.cs)  
– **Class (API 5 version)**: RouteAdvancedConstraints

The file [Activity.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Activity.cs)  
– **Properties**: RouteNam(string), NoteId(string), NoteType(string), NoteContents(string), NoteFile(string), Member(ActivityMember), DestinationName(string), DestinationAlias(string)  
– **Class**: ActivityMember
	
The file [AddressBookContact.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/AddressBookContact.cs)  
– **Property**: IsAssigned(string)
	
The file [AddressBookGroup.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/AddressBookGroup.cs)  
– **Property**: Valid(bool?)
	
The file [DataObject.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/DataObject.cs)  
– **Property**: SmartOptimizationId(string)
	
The file [FindAssetResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/FindAssetResponse.cs)  
– **Properties**: LargeLogoUri2x(string), MobileLogoUri2x(string), HideCovid19Warning(string)
	
The file [MemberResponseV4.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/MemberResponseV4.cs)  
– **Property**: ApiKey(string)
	
The file [Order.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Order.cs)  
– **Property**: VisitedCount(int)
	
The file [TrackingHistory.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/TrackingHistory.cs)  
– **Property**: SpeedUnit(string)
	
The file [UserLocation.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/UserLocation.cs)  
– **Properties**: CalculatedSpeed(string), SpeedAccuracy(string), SpeedUnit(string), Bearing(int?), BearingAccuracy(string), Accuracy(string), RootMemberId(int?)
	
The file [OrderInventory.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/OrderInventory.cs)  
– **Class (API 5 version)**: OrderInventory


### Changed

The file [DeviceLocationLeg.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/DeviceLocationLeg.cs)  
– Type decimal? replaced with double? for the properties: Distance, Duration, Weight.
	
The file [DeviceLocationMatching.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/DeviceLocationMatching.cs)  
– Type decimal? replaced with double? for the properties: Distance, Duration, Weight.
	
The file [DeviceLocationTracePoint.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/DeviceLocationTracePoint.cs)  
– Type decimal? replaced with double? for the property: Distance.
	
The file [DirectionStep.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/DirectionStep.cs)  
– Type decimal? replaced with double? for the property: UduDistance.
	
The file [Route.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Route.cs)  
– Type decimal? replaced with double? for the properties: UduActualTravelDistance, ActualTravelDistance, PayingMiles.
	
The file [RouteParameters.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/RouteParameters.cs)  
– Type decimal? replaced with double? for the properties: RouteTimeMultiplier, RouteServiceTimeMultiplier.  
– Type string replaced with bool? for the property: IsUpload.  
– Type double? replaced with int? for the property: RouteTimeMultiplier.  
– Type double? replaced with int? for the property: RouteServiceTimeMultiplier.
	
The file [RouteResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/RouteResponse.cs)  
– Type decimal? replaced with double? for the property: UduActualTravelDistance.
	
The file [UserLocation.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/UserLocation.cs)  
– Type decimal? replaced with double? for the property: Speed.
	
The file [VehicleResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/VehicleResponse.cs)  
– Type string replaced with bool? for the property: IsOperational.
	
The file [Address.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Address.cs)  
– Type string replaced with int? for the property: MemberId.

The file [AddressBookGroupRule.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/AddressBookGroupRule.cs)  
– Type string replaced with object for the property: Value.
	
The file [Types.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Types.cs)  
– Enumeration HttpMethodType - added Patch.

The file [RouteAdvancedConstraints.cs (API 5): ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/RouteAdvancedConstraints.cs)  
– Type int? replaced with int[] for the property: Route4meMembersId.  
– Type Tuple<int,int>[] replaced with List<int[]> for the property: AvailableTimeWindows.
	
The file [MemberResponse.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/AddressBookGroupRule.cs)  
– Type int? replaced with string for the property: SessionId.
	
The file [Enums.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Enums.cs)  
– Enumeration: AlgorithmType - added: OPTIMIZATION_STATE_IN_QUEUE = 7, ADVANCED_CVRP_TW = 9
	
The file [Enums.cs (API 5): ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/V5/Enum.cs)  
– Enumeration: AlgorithmType - added: OPTIMIZATION_STATE_IN_QUEUE = 7, ADVANCED_CVRP_TW = 9  
– Enumeration: OptimizationState - added: InQueue = 7
	
The file [GPSParameters.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/GPSParameters.cs)  
– Type int replaced with int? for the property: MemberId.  
– Type int replaced with string for the property: VehicleId.  
– Type int replaced with int? for the property: Course.  
– Type double replaced with double? for the property: Speed.  
– Type double replaced with double? for the property: Latitude.  
– Type double replaced with double? for the property: Longitude.  
– Type bool replaced with bool? for the property: LastPosition.  
– Type long replaced with long? for the property: StartDate.  
– Type long replaced with long? for the property: EndDate.  
– Type double replaced with double? for the property: Altitude.

The file [AddressBookContact.cs: ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/AddressBookContact.cs)  
– Type string replaced with bool? for the property: IsAssigned.


## [1.0.0.5] - 2020-05-14

### Added

- The class [Utils](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs). Added the methods:
	1. The method [ConvertObjectToType<T>(ref object value)](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L515) - for converting a value to the specified type.
	2. The method [ConvertObjectToPropertyType(object value, PropertyInfo targetProperty)](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L614) - for converting a value to the specified type.
	

### Changed

- in the The class [Route4MeDynamicClass](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs) changed:
	1. The method [SearchAddressBookLocation(AddressBookParameters addressBookParameters, out List<AddressBookContact> contactsFromObjects, out string errorString)](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L2378) - searhing the locations with showing specified fields.
	2. The method [SearchAddressBookLocation(AddressBookParameters addressBookParameters, out List<AddressBookContact> contactsFromObjects, out string errorString)](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L2378) - added algorithm of managing wrong data.
	3. The method [UpdateRoute(DataObjectRoute route, DataObjectRoute initialRoute, out string errorString)](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L393) - handled issues: ApprovedForExecution, depots, sequencing.
	4. Added the method [AddAddressNote(NoteParameters noteParameters, out string errorString)](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L1591) - for adding complex address note (send text content, custom note, file at once) to a route address.
	
- The class [Utils](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs). Modified the methods:
    1. The method [TValue ToObject<TValue>(object obj, out string errorString)](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L488) - remade using try-catch.
- The class [NoteParameters](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/NoteParameters.cs)- added properties for form data.


## [1.0.0.4] - 2020-03-30

### Added

- The method [UpdateAddressBookContact](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L2308) - for updating the Address Book Contact with data with containg null values.
- The method [UpdateAddressBookContact](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L2326) - for updating the Address Book Contact by sending modified address book contact object.
- The class [ReadOnlyAttribute](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/ReadOnlyAttribute.cs) - for creating custom attribute ReadOnlyAttribute to distinguish the object properties while updating.
- The response class [VehicleV4CreateResponse](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/VehicleV4CreateResponse.cs)) for new vehicle creating process using the endpoint /api.v4/vehicle.php.
- The helper class [DataContractResolver](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataContractResolver.cs) - for assigning the null values to the properties.
- The class [Route4MeDynamicClass](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Route4MeDynamicClass.cs)) - for creating a dynamic class from an existing class by copying only specified properties.
- The method [UpdateRoute(DataObjectRoute route, DataObjectRoute initialRoute, out string errorString)](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L376) - for updating a route by sending the initial route and cloned-modified route. The algorithm compares the routes, finds modified properties and updates them in the Route4Me database.
- The method [UpdateOptimizationDestination](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L973) to the Route4MeManager class for updating an optimization destination by sending changed Address object.
- The method [AddOptimizationDestinations](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L1839) to the Route4MeManager class for adding the optimization destinations by sending array of the Address objectS.
- The class [Utils](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs)). Modified the methods:
    1. The method [ObjectDeepClone<T>(T obj)](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L232)) - for clonning a Route4Me object.
	2. The method [GetPropertiesWithDifferentValues(object modifiedObject, object initialObject, out string errorString, bool excludeReadonly = true)](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L266)) - compares initial and modified Route4Me objects and returns a list of the modified properties.
	3. The method [IsPropertyDictionary](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L384)) - checks if a Route4Me object property is a dictionary type.
	4. The method [IsPropertyObject](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L399)) - checks if a Route4Me object property is a nested object type.
	5. The method [IsPropertyArray](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L421)) - checks if a Route4Me object property is an array type.
	6. The method [IsDictionariesEqual](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L434)) - checks if the dictionaries are equal.
	7. The method [CheckIfPropertyHasIgnoreAttribute](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L480)) - checks if a Route4Me object property has the attribute IgnoreDataMemberAttribute.
	8. The method [CheckIfPropertyHasReadOnlyAttribute](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L492)) - checks if a Route4Me object property has the attribute ReadOnllyAttribute.
	9. The method [ReadObjectNew<T>](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L59)) - for reading Route4Me object from a JSON string.
	10. The method [GetPropertyPositions<T>](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L510)) - for getting positions of the Route4Me object properties.
	11. The method [OrderPropertiesByPosition<T>](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L529)) - for the ordering of a property names list by the positions in the object.
	12. The method [ToObject<TValue>](](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L562)) - for converting anonymous type object to a specified Route4Me object type.


### Changed

- The class [Address](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Address.cs) - added the custom attribute ReadOnlyAttribute to some properties.
- The enumaration types [Enum](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Enums.cs): the enumeration types cahnged according to current state of the web application.
- In the class [VehicleV4Response](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/VehicleV4Response.cs) :
   1. Removed the property [IsDeleted].
   2. Removed the property [CreatedTime] - there is the property [TimestampAdded](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/VehicleV4Response.cs#L72) instead.
   3. Removed the property [TimestampRemoved].
   3. The class [VehicleV4Response](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/VehicleV4Response.cs) - all the properties type changed to the string.
- In the class AddressBookContact.cs changed the property [AddressCustomData](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/AddressBookContact.cs#L165) - filters wrong data type for this property.
- In the AddressBookContact.cs changed type of the properties from object type to:
	1. AddressCube -> double?
	2. AddressPieces -> int?
	3. AddressReferenceNo -> string
	4. AddressRevenue -> double?
	5. AddressWeight -> double?
	6. AddressCustomerPo -> string
	
- The method [GetUserLocations](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Route4MeManager.cs#L1099) - changed returning object type from Dictionary<string,UserLocation> to UserLocation[].
- In the Utils.cs changed the method [SerializeObjectToJson](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/Utils.cs#L94) - for asuring to get correct serialization: if serialization with the DataContractJsonSerializer is failing, serialization with the SerializeObjectToJson will be done.
- The class [Route](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/DataTypes/Route.cs) - added the custom attribute ReadOnlyAttribute to some properties.
- The class [OptimizationParameters](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/OptimizationParameters.cs) - added he properties:
    1. The property [StartDate ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/OptimizationParameters.cs#L80);
	2. The property [EndDate](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/OptimizationParameters.cs#L84).
- The class [RouteParametersQuery](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/RouteParametersQuery.cs) - added he properties:
    1. The property [StartDate ](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/RouteParametersQuery.cs#L66);
	2. The property [EndDate](https://github.com/route4me/route4me-net-core/blob/master/route4me-csharp-sdk/Route4MeSDKLibrary/QueryTypes/RouteParametersQuery.cs#L70).





