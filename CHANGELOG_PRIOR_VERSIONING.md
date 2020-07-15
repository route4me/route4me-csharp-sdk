# Changelog
All notable changes to this project will be documented in this file.


## 2019-12-06

- The class **RouteParameters.cs**:
	1. Changed type of the property **TruckHazardousGoods** from **string[]** to **string**.
	
- The class **RouteParametersQuery.cs**. Added:
	1. The property **Parameters**(RouteParameters);
	2. The property **Addresses**(Address[]);
	3. The property **ApprovedForExecution**(bool).
	
- The class **Route4MeManager.cs**. Added:
	1. The method **UpdateRoute**: for updating a route object by route object directly (without separate parameters). Note: later this changed.


## 2019-11-30

- The class **Activity.cs**:
	1. Changed type of the property **ActivityTimestamp** from **uint?** to **long**.
	
- The class **Address.cs**:
	1. Changed type of the property **TimeframeViolationTime** from **int?** to **long?**;
	2. Changed type of the property **TimestampLastVisited** from **uint?** to **long?**;
	3. Changed type of the property **TimestampLastDeparted** from **uint?** to **long?**;
	4. Changed type of the property **OrderId** from **Nullable<int>** to **int?**;
	5. Changed type of the property **DriveTimeToNextDestination** from **int?** to **long?**;
	6. Changed type of the property **AbnormalTrafficTimeToNextDestination** from **int?** to **long?**;
	7. Changed type of the property **UncongestedTimeToNextDestination** from **int?** to **long?**;
	8. Changed type of the property **TrafficTimeToNextDestination** from **int?** to **long?**;
	9. Changed type of the property **GeneratedTimeWindowStart** from **int?** to **long?**;
	10. Changed type of the property **GeneratedTimeWindowEnd** from **int?** to **long?**;
	11. Changed type of the property **TimeWindowEnd** from **int?** to **long?**;
	12. Changed type of the property **TimeWindowStart2** from **int?** to **long?**;
	13. Changed type of the property **TimeWindowEnd2** from **int?** to **long?**;
	14. Changed type of the property **geofence_detected_visited_timestamp** from **int?** to **long?**;
	15. Changed type of the property **geofence_detected_departed_timestamp** from **int?** to **long?**;
	16. Changed type of the property **geofence_detected_service_time** from **int?** to **long?**;
	17. Changed type of the property **Time** from **int?** to **long?**;
	18. Changed type of the property **WaitTimeToNextDestination** from **int?** to **long?**.

- The class **AddressBookContact.cs**:
	1. Changed type of the property **created_timestamp** from **int** to **long**;
	2. Changed type of the property **local_time_window_start** from **int?** to **long?**;
	3. Changed type of the property **local_time_window_end** from **int?** to **long?**;
	4. Changed type of the property **local_time_window_start_2** from **int?** to **long?**;
	5. Changed type of the property **last_visited_timestamp** from **int?** to **long?**;
	6. Changed type of the property **last_routed_timestamp** from **int?** to **long?**;
	7. Changed type of the property **service_time** from **int?** to **long?**.
	
- The class **AddressGeocoded.cs**:
	1. Changed type of the property **intUserIP** from **uint** to  **long**.
	
- The class **AddressManifest.cs**:
	1. Changed type of the property **RunningServiceTime** from **int?** to **long?**;
	2. Changed type of the property **RunningTravelTime** from **int?** to **long?**;
	3. Changed type of the property **RunningWaitTime** from **int?** to **long?**;
	4. Changed type of the property **ProjectedArrivalTimeTs** from **int?** to **long?**;
	5. Changed type of the property **ProjectedDepartureTimeTs** from **int?** to **long?**;
	6. Changed type of the property **ActualArrivalTimeTs** from **int?** to **long?**;
	7. Changed type of the property **ActualDepartureTimeTs** from **int?** to **long?**;
	8. Changed type of the property **EstimatedArrivalTimeTs** from **int?** to **long?**;
	9. Changed type of the property **EstimatedDepartureTimeTs** from **int?** to **long?**;
	10. Changed type of the property **ScheduledArrivalTimeTs** from **int?** to **long?**;
	11. Changed type of the property **ScheduledDepartureTimeTs** from **int?** to **long?**;

- The class **addressNote.cs**:
	1. Changed type of the property **TimestampAdded** from **uint?** to **long?**.

- The class **DirectionLocation.cs**:
	1. Changed type of the property **Time** from **int** to **long**.

- The class **FindAssetResponse.cs**:
	1. Changed type of the property **TimestampGeofenceVisited** from **int?** to **long?**;
	2. Changed type of the property **TimestampLastVisited** from **int?** to **long?**;
	
- The class **AssetStatusHistory.cs**:
	1. Changed type of the property **UnixTimestamp** from **int?** to **long?**.
	
- The class **MemberResponse.cs**:
	1. Changed type of the property **Status** from **Nullable<bool>** to **bool?**
	2. Changed type of the property **SessionId** from **Nullable<int>** to **int?**
	3. Changed type of the property **MemberId** from **Nullable<int>** to **int?**
	4. Changed type of the property **TrackingTtl** from **Nullable<int>** to **int?**
	5. Changed type of the property **GeofencePolygonSize** from **Nullable<int>** to **int?**
	6. Changed type of the property **GeofenceTimeOnsiteTriggerSes
	7. Changed type of the property **IsSubscriptionPastDue** from **Nullable<int>** to **int?**
	8. Changed type of the property **AccountTypeId** from **Nullable<int>** to **int?**
	9. Changed type of the property **MaxStopsPerRoute** from **Nullable<int>** to **int?**
	10. Changed type of the property **MaxRoutess** from **Nullable<int>** to **int?**
	11. Changed type of the property **RoutesPlanned** from **Nullable<int>** to **int?**
	12. Changed type of the property **AutoLogoutTs**  from **Nullable<int>** to **int?**

- The class **Order.cs**:
	1. Changed type of the property **local_time_window_start** from **int?** to **long?**;
	2. Changed type of the property **local_time_window_end** from **int?** to **long?**;
	3. Changed type of the property **local_time_window_start_2** from **int?** to **long?**;
	4. Changed type of the property **local_time_window_end_2** from **int?** to **long?**;
	5. Changed type of the property **service_time** from **int?** to **long?**.

- The class **Order.cs**:
	1. Changed type of the property **RouteDurationSec** from **int?** to **long?**;
	2. Changed type of the property **lannedTotalRouteDuration** from **int?** to **long?**;
	3. Changed type of the property **TotalWaitTime** from **int?** to **long?**;
	4. Changed type of the property **ActualTravelTime** from **int?** to **long?**;
	5. Changed type of the property **WorkingTime** from **int?** to **long?**;
	6. Changed type of the property **DrivingTime** from **int?** to **long?**;
	7. Changed type of the property **IdlingTime**  from **int?** to **long?**.

- The class **RouteParameters.cs**:
	1. Changed type of the property **RouteMaxDuration** from **int?** to **long?**;
	2. Changed type of the property **Time** from **int?** to **long?**.
	
- The class **RouteResponse.cs**:
	1. Changed type of the property **UserRouteRating** from **Nullable<int>** to **int?**
	2. Changed type of the property **MemberId** from **Nullable<int>** to **int?**
	3. Changed type of the property **TripDistance** from **Nullable<double>** to **double?**
	4. Changed type of the property **RouteCost** from **Nullable<double>** to **double?**
	5. Changed type of the property **RouteRevenue** from **Nullable<double>** to **double?**
	6. Changed type of the property **NetRevenuePerDistanceUnit** from **Nullable<double>** to **double?**
	7. Changed type of the property **CreatedTimestamp** from **Nullable<long>** to **long?**
	8. Changed type of the property **mpg** from **Nullable<double>** to **double?**
	9. Changed type of the property **GasPrice** from **Nullable<double>** to **double?**
	10. Changed type of the property **RouteDurationSec** from **Nullable<long>** to **long?**
	11. Changed type of the property **PlannedTotalRouteDuration** from **Nullable<int>** to **int?**
	12. Changed type of the property **ActualTravelDistance** from **Nullable<double>** to **double?**
	13. Changed type of the property **ActualTravelTime** from **Nullable<int>** to **int?**
	14. Changed type of the property **ActualFootSteps** from **Nullable<int>** to **int?**
	15. Changed type of the property **WorkingTime** from **Nullable<int>** to **int?**
	16. Changed type of the property **DrivingTime** from **Nullable<int>** to **int?**
	17. Changed type of the property **IdlingTime** from **Nullable<int>** to **int?**
	18. Changed type of the property **PayingMiles** from **Nullable<double>** to **double?**
	19. Changed type of the property **GeofencePolygonSize** from **Nullable<int>** to **int?**

- The class **User.cs**:
	1. Fixed wrong parameter name from **MemberLasttName** to **MemberLastName**.
	
- The class **VehicleResponse.cs**:
	1. Changed type of the property **VehicleRegStateId** from **Nullable<int>** to **int?**
	2. Changed type of the property **VehicleRegCountryId** from **Nullable<int>** to **int?**
	3. Changed type of the property **VehicleModelYear** from **Nullable<int>** to **int?**
	4. Changed type of the property **VehicleYearAcquired** from **Nullable<int>** to **int?**
	5. Changed type of the property **VehicleCostNew** from **Nullable<double>** to **double?**
	6. Changed type of the property **VehicleAxleCount** from **Nullable<int>** to **int?**
	7. Changed type of the property **MpgCity** from **Nullable<double>** to **double?**
	8. Changed type of the property **MpgHighway** from **Nullable<double>** to **double?**
	9. Changed type of the property **HeightInches** from **Nullable<double>** to **double?**
	10. Changed type of the property **WeightLb** from **Nullable<double>** to **double?**
	11. Changed type of the property **IsOperational** from **Nullable<bool>** to bool?

- The class **ActivityParameters.cs**:
	1. Changed type of the property **Start** from **uint?** to **long?**
	2. Changed type of the property **End** from **uint?** to **long?**
	
- The class **GPSParameters.cs**:
	1. Changed type of the property **StartDate** from **int**to  **long**
	2. Changed type of the property **EndDate** from **int**to  **long**
	
- The class **MemberParameters.cs**: compacted getters and setters.

- The class **MemberParametersV4.cs**: compacted getters and setters.

- The class **OptimizationParameters.cs**: compacted getters and setters of the property **Redirect.

- The class **RouteParametersQuery.cs**: compacted getters and setters of the property **Redirect.

- The class **TelematicsVendorParameters.cs**:
	1. Changed type of the property **vendorID** from **Nullable<uint>** to **uint?**
	2. Changed type of the property **isIntegrated** from **Nullable<uint>** to **uint?**
	3. Changed type of the property **Page** from **Nullable<uint>** to **uint?**
	4. Changed type of the property **perPage** from **Nullable<uint>** to **uint?**

- The class **VehicleParameters.cs**:
	1. Changed type of the property **Page** from **Nullable<uint>** to **uint?**
	2. Changed type of the property **PerPage** from **Nullable<uint>** to **uint?**
	
- The class **Route4MeManager.cs->UpdateRouteCustomDataRequest**:
	1. Changed type of the property **RouteDestinationId** from **Nullable<int>** to **int?**
	
- The class **Route4MeManager.cs->UpdateRouteDestinationRequest**:
	1. Changed type of the property **RouteDestinationId** from **Nullable<int>** to **int?**
	2. Changed type of the property **TimeWindowStart** from **int?** to **long?**
	3. Changed type of the property **TimeWindowEnd** from **int?** to **long?**
	4. Changed type of the property **Time** from **int?** to **long?**
	5. Changed type of the property **TimeWindowStart2** from **int?** to **long?**
	6. Changed type of the property **TimeWindowEnd2** from **int?** to **long?**
	
- The class **Route4MeManager.cs->ValidateSessionRequest**:	
	1. Changed type of the property **MemberId** from **Nullable<int>** to **int?**
	
- The class **Route4MeManager.cs->MarkAddressDepartedRequest**:
	1. Changed type of the property **MemberId** from **Nullable<int>** to **int?**
	
- The class **Route4MeManager.cs->MarkAddressAsMarkedAsDepartedRequest**:
	1. Changed type of the property **RouteDestinationId** from **Nullable<int>** to **int?**
	
- The class **Route4MeManager.cs->SearchAddressBookLocationRequest**:
	1. Changed type of the property **Offset** from **Nullable<int>** to **int?**
	1. Changed type of the property **Limit** from **Nullable<int>** to **int?**
	
- The class **Route4MeManager.cs->AddOrdersToRouteRequest**:
	1. Changed type of the property **Redirect** from **Nullable<int>** to **int?**
	
- The class **Route4MeManager.cs->AddOrdersToOptimizationRequest**:
	1. Changed type of the property **Redirect** from **Nullable<int>** to **int?**



## 2019-11-28

- The class **AddressCustomNote.cs**:
	1. Changed type of the property **NoteCustomEntryID** from **int** to **string**;
	2. Changed type of the property **NoteCustomTypeID** from **int** to **string**.

- The class **AddressNote.cs**:
	1. Added the property **CustomTypes** with the type **AddressCustomNote[]**.
	
- The class **DataObject.cs**:
	1. Added the property: **OptimizationErrors** with the type **string[]**.
	

## 2019-11-12

- Created the class	**DataObjectBase**, moved into it some properties from the classes: **DataObject** and **DataObjectRoute** - now these classes are inherited from the class **DataObjectBase**.


## 2019-10-19

### Added

- The class **AddressBookContact.cs**. Added:
	1. The property: **member_id** (int?);
	2. The property: **in_route_count** (int?);
	3. The property: **visited_count** (int?);
	4. The property: **last_visited_timestamp** (int?);
	5. The property: **last_routed_timestamp** (int?).



