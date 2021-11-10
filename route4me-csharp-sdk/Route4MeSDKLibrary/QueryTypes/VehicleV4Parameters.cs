﻿using System.Runtime.Serialization;

//using System.Collections.Generic;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    ///     Parameters for the vehicle(s) request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class VehicleV4Parameters : GenericParameters
    {
        /// <summary>
        ///     Unique ID of a vehicle.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [IgnoreDataMember] // Don't serialize as JSON
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     Unique ID of a member.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public string MemberId { get; set; }

        /// <summary>
        ///     Specifies if the vehicle is marked as deleted.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "is_deleted", EmitDefaultValue = false)]
        public bool? IsDeleted { get; set; }

        /// <summary>
        ///     Required for a vehicle creating.
        /// </summary>
        [DataMember(Name = "vehicle_name", EmitDefaultValue = false)]
        public string VehicleName { get; set; }

        /// <summary>
        ///     Vehicle alias.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_alias", EmitDefaultValue = false)]
        public string VehicleAlias { get; set; }

        /// <summary>
        ///     Vehicle VIN (vehicle identification number).
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_vin", EmitDefaultValue = false)]
        public string VehicleVin { get; set; }

        /// <summary>
        ///     Vehicle creation timestamp.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "created_time", EmitDefaultValue = false)]
        public string CreatedTime { get; set; }

        /// <summary>
        ///     Vehicle registration state ID.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_reg_state_id", EmitDefaultValue = false)]
        public string VehicleRegStateId { get; set; }

        /// <summary>
        ///     Vehicle registration country ID.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_reg_country_id", EmitDefaultValue = false)]
        public int? VehicleRegCountryId { get; set; }

        /// <summary>
        ///     Vehicle registration country ID.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_license_plate", EmitDefaultValue = false)]
        public string VehicleLicensePlate { get; set; }

        /// <summary>
        ///     Unique ID of vehicle type.
        ///     <para>Available values: </para>
        ///     <value>'sedan', 'suv', 'pickup_truck', 'van', '18wheeler', </value>
        ///     <para>
        ///         <value>'cabin', 'hatchback', 'motorcyle', 'waste_disposal', 'tree_cutting', </value>
        ///     </para>
        ///     <value>'bigrig', 'cement_mixer', 'livestock_carrier', 'dairy','tractor_trailer'</value>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_type_id", EmitDefaultValue = false)]
        public string VehicleTypeID { get; set; }

        /// <summary>
        ///     When the vehicle was added.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "timestamp_added", EmitDefaultValue = false)]
        public string TimestampAdded { get; set; }

        /// <summary>
        ///     Vehicle maker brend.
        ///     <para>Available values: </para>
        ///     <value>'American Coleman', 'BMW', 'Chevrolet', 'Ford', 'Freightliner', </value>
        ///     <para>
        ///         <value>'GMC', 'Hino', 'Honda', 'Isuzu', 'Kenworth', </value>
        ///     </para>
        ///     <value>'Mack', 'Mercedes-Benz', 'Mitsubishi', 'Navistar', 'Nissan', </value>
        ///     <para>
        ///         <value>'Peterbilt', 'Renault', 'Scania', 'Sterling', 'Toyota', </value>
        ///     </para>
        ///     <value>'Volvo', 'Western Star'</value>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_make", EmitDefaultValue = false)]
        public string VehicleMake { get; set; }

        /// <summary>
        ///     A year of the vehicle model.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_model_year", EmitDefaultValue = false)]
        public int? VehicleModelYear { get; set; }

        /// <summary>
        ///     A model of the vehicle.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_model", EmitDefaultValue = false)]
        public string VehicleModel { get; set; }

        /// <summary>
        ///     A year the vehicle was acquired
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_year_acquired", EmitDefaultValue = false)]
        public int? VehicleYearAcquired { get; set; }

        /// <summary>
        ///     A cost of the new vehicle.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "vehicle_cost_new", EmitDefaultValue = false)]
        public double? VehicleCostNew { get; set; }

        /// <summary>
        ///     If true, the vehicle was purchased new.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "purchased_new", EmitDefaultValue = false)]
        public bool? PurchasedNew { get; set; }

        /// <summary>
        ///     Start date of the license.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "license_start_date", EmitDefaultValue = false)]
        public string LicenseStartDate { get; set; }

        /// <summary>
        ///     End date of the license.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "license_end_date", EmitDefaultValue = false)]
        public string LicenseEndDate { get; set; }

        /// <summary>
        ///     A number of the vecile's axles.
        ///     Available values: 2,3,4
        /// </summary>
        [DataMember(Name = "vehicle_axle_count", EmitDefaultValue = false)]
        public int? VehicleAxleCount { get; set; }

        /// <summary>
        ///     Miles per gallon in the city area.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "mpg_city", EmitDefaultValue = false)]
        public int? MpgCity { get; set; }

        /// <summary>
        ///     Miles per gallon in the highway area.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "mpg_highway", EmitDefaultValue = false)]
        public int? MpgHighway { get; set; }

        /// <summary>
        ///     A type of the fuel.
        ///     <para>Available values: </para>
        ///     <value>'unleaded 87', 'unleaded 89', 'unleaded 91', 'unleaded 93', </value>
        ///     <para>
        ///         <value>'diesel', 'electric', 'hybrid'</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "fuel_type", EmitDefaultValue = false)]
        public string FuelType { get; set; }

        /// <summary>
        ///     A height of the vehicle in the inches.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "height_inches", EmitDefaultValue = false)]
        public int? HeightInches { get; set; }

        /// <summary>
        ///     A weight of the vehicle in the pounds.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "weight_lb", EmitDefaultValue = false)]
        public int? WeightLb { get; set; }

        /// <summary>
        ///     If "1", the vehicle is operational.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "is_operational", EmitDefaultValue = false)]
        public string IsOperational { get; set; }

        /// <summary>
        ///     External telematics vehicle ID.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "external_telematics_vehicle_id", EmitDefaultValue = false)]
        public string ExternalTelematicsVehicleID { get; set; }

        /// <summary>
        ///     If "1", the vehicle has trailer.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "has_trailer", EmitDefaultValue = false)]
        public string HasTrailer { get; set; }

        /// <summary>
        ///     Vehicle height in inches.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "heightInInches", EmitDefaultValue = false)]
        public string HeightInInches { get; set; }

        /// <summary>
        ///     Vehicle length in inches.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "lengthInInches", EmitDefaultValue = false)]
        public int? LengthInInches { get; set; }

        /// <summary>
        ///     Vehicle width in inches.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "widthInInches", EmitDefaultValue = false)]
        public int? WidthInInches { get; set; }

        /// <summary>
        ///     Maximum weight per axle group in pounds.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "maxWeightPerAxleGroupInPounds", EmitDefaultValue = false)]
        public int? MaxWeightPerAxleGroupInPounds { get; set; }

        /// <summary>
        ///     Number of the axles.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "numAxles", EmitDefaultValue = false)]
        public int? NumAxles { get; set; }

        /// <summary>
        ///     Weight in pounds.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "weightInPounds", EmitDefaultValue = false)]
        public int? WeightInPounds { get; set; }

        /// <summary>
        ///     Hazardous materials type.
        ///     <para>Available values: </para>
        ///     <value>'INVALID', 'NONE', 'GENERAL', 'EXPLOSIVE', 'INHALANT', </value>
        ///     <para>
        ///         <value>'RADIOACTIVE', 'CAUSTIC', 'FLAMMABLE', 'HARMFUL_TO_WATER'</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "HazmatType", EmitDefaultValue = false)]
        public string HazmatType { get; set; }

        /// <summary>
        ///     Low emission zone preference.
        ///     <para>Available values: </para>
        ///     <value>'AVOID', 'ALLOW', 'WARN'</value>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "LowEmissionZonePref", EmitDefaultValue = false)]
        public string LowEmissionZonePref { get; set; }

        /// <summary>
        ///     Use 53 foot trailer routing.
        ///     <para>Available values:
        ///         <value>'YES', 'NO'</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "Use53FootTrailerRouting", EmitDefaultValue = false)]
        public string Use53FootTrailerRouting { get; set; }

        /// <summary>
        ///     Use national network.
        ///     <para>Available values:
        ///         <value>'YES', 'NO'</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "UseNationalNetwork", EmitDefaultValue = false)]
        public string UseNationalNetwork { get; set; }

        /// <summary>
        ///     Use truck restrictions.
        ///     <para>Available values:
        ///         <value>'YES', 'NO'</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "UseTruckRestrictions", EmitDefaultValue = false)]
        public string UseTruckRestrictions { get; set; }

        /// <summary>
        ///     Avoid Ferries.
        ///     <para>Available values:
        ///         <value>'YES', 'NO'</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "AvoidFerries", EmitDefaultValue = false)]
        public string AvoidFerries { get; set; }

        /// <summary>
        ///     Divided highway avoid preference (e.g. NEUTRAL).
        ///     <para>Available values:
        ///         <value>'STRONG_AVOID', 'AVOID', 'NEUTRAL', 'FAVOR', 'STRONG_FAVOR'.</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "DividedHighwayAvoidPreference", EmitDefaultValue = false)]
        public string DividedHighwayAvoidPreference { get; set; }

        /// <summary>
        ///     Freeway avoid preference.
        ///     <para>Available values:
        ///         <value>'STRONG_AVOID', 'AVOID', 'NEUTRAL', 'FAVOR', 'STRONG_FAVOR'.</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "FreewayAvoidPreference", EmitDefaultValue = false)]
        public string FreewayAvoidPreference { get; set; }

        /// <summary>
        ///     International borders open.
        ///     <para>Available values:
        ///         <value>'YES', 'NO'</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "InternationalBordersOpen", EmitDefaultValue = false)]
        public string InternationalBordersOpen { get; set; }

        /// <summary>
        ///     Toll road usage.
        ///     <para>Available values:
        ///         <value>'ALWAYS_AVOID', 'IF_NECESSARY', 'NO_RESTRICTION'</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "TollRoadUsage", EmitDefaultValue = false)]
        public string TollRoadUsage { get; set; }

        /// <summary>
        ///     If true, the vehicle uses only highway.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "hwy_only", EmitDefaultValue = false)]
        public bool HwyOnly { get; set; }

        /// <summary>
        ///     If true, the vehicle is long combination.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "long_combination_vehicle", EmitDefaultValue = false)]
        public bool LongCombinationVehicle { get; set; }

        /// <summary>
        ///     If true, the vehicle should avoid highways.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "avoid_highways", EmitDefaultValue = false)]
        public bool AvoidHighways { get; set; }

        /// <summary>
        ///     Side street adherence.
        ///     <para>Available values: </para>
        ///     <value>'OFF', 'MINIMAL', 'MODERATE', 'AVERAGE', 'STRICT', 'ADHERE', 'STRONGLYHERE'</value>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "side_street_adherence", EmitDefaultValue = false)]
        public string SideStreetAdherence { get; set; }

        /// <summary>
        ///     Truck configuration.
        ///     <para>Available values: </para>
        ///     <value>'NONE', 'PASSENGER', '28_DOUBLETRAILER', '48_STRAIGHT_TRUCK', '48_SEMI_TRAILER', </value>
        ///     <para>
        ///         <value>'53_SEMI_TRAILER', 'FULLSIZEVAN', '26_STRAIGHT_TRUCK'</value>
        ///     </para>
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "truck_config", EmitDefaultValue = false)]
        public string TruckConfig { get; set; }

        /// <summary>
        ///     Vehicle height in metric unit.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "height_metric", EmitDefaultValue = false)]
        public float? HeightMetric { get; set; }

        /// <summary>
        ///     Vehicle length in metric unit.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "length_metric", EmitDefaultValue = false)]
        public float? LengthMetric { get; set; }

        /// <summary>
        ///     Vehicle width in metric unit.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "width_metric", EmitDefaultValue = false)]
        public float? WidthMetric { get; set; }

        /// <summary>
        ///     Vehicle weight in metric unit.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "weight_metric", EmitDefaultValue = false)]
        public float? WeightMetric { get; set; }

        /// <summary>
        ///     Maximum weight per axle group in metric unit.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "max_weight_per_axle_group_metric", EmitDefaultValue = false)]
        public float? MaxWeightPerAxleGroupMetric { get; set; }

        /// <summary>
        ///     When he vehicle was removed.
        ///     <remarks>
        ///         <para>Data member parameter.</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "timestamp_removed", EmitDefaultValue = false)]
        public string TimestampRemoved { get; set; }
    }
}