using Route4MeSDK.QueryTypes;
using System;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response from the vehicle request. See also <seealso cref="VehiclesPaginated"/>.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class VehicleV4Response : GenericParameters
    {
        /// <summary>
        /// The vehicle ID.
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        /// Member ID assigned to the vehicle.
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public string MemberId { get; set; }

        /// <summary>
        /// <c>true</c> if the vehicle is deleted.
        /// </summary>
        [DataMember(Name = "is_deleted", EmitDefaultValue = false)]
        public Nullable<bool> IsDeleted { get; set; }

        /// <summary>
        /// Vehicle alias.
        /// </summary>
        [DataMember(Name = "vehicle_alias", EmitDefaultValue = false)]
        public string VehicleAlias { get; set; }

        /// <summary>
        /// Vehicle VIN.
        /// </summary>
        [DataMember(Name = "vehicle_vin", EmitDefaultValue = false)]
        public string VehicleVin { get; set; }

        /// <summary>
        /// When the vehicle was created.
        /// </summary>
        [DataMember(Name = "created_time", EmitDefaultValue = false)]
        public string CreatedTime { get; set; }

        /// <summary>
        /// Vehicle registration state ID.
        /// </summary>
        [DataMember(Name = "vehicle_reg_state_id", EmitDefaultValue = false)]
        public string VehicleRegStateId { get; set; }

        /// <summary>
        /// Vehicle registration country ID.
        /// </summary>
        [DataMember(Name = "vehicle_reg_country_id", EmitDefaultValue = false)]
        public Nullable<int> VehicleRegCountryId { get; set; }

        /// <summary>
        /// A license plate of the vehicle.
        /// </summary>
        [DataMember(Name = "vehicle_license_plate", EmitDefaultValue = false)]
        public string VehicleLicensePlate { get; set; }

        /// <summary>
        /// Vehicle type.
        /// <para>Availbale values:</para>
        /// <value>
        /// 'sedan', 'suv', 'pickup_truck', 'van', '18wheeler', 'cabin', 'hatchback', 
        /// '<para>motorcyle', 'waste_disposal', 'tree_cutting', 'bigrig', 'cement_mixer', </para>
        /// 'livestock_carrier', 'dairy','tractor_trailer'.
        /// </value>
        /// </summary>
        [DataMember(Name = "vehicle_type_id", EmitDefaultValue = false)]
        public string VehicleTypeID { get; set; }

        /// <summary>
        /// When the vehicle was added.
        /// </summary>
        [DataMember(Name = "timestamp_added", EmitDefaultValue = false)]
        public string TimestampAdded { get; set; }

        /// <summary>
        /// Vehicle maker brend. 
        /// <para>Available values:</para>
        /// <value>
        /// "american coleman", "bmw", "chevrolet", "ford", "freightliner", "gmc", 
        /// <para>"hino", "honda", "isuzu", "kenworth", "mack", "mercedes-benz", "mitsubishi", </para>
        /// "navistar", "nissan", "peterbilt", "renault", "scania", "sterling", "toyota", 
        /// <para>"volvo", "western star" </para>
        /// </value>"
        /// </summary>
        [DataMember(Name = "vehicle_make", EmitDefaultValue = false)]
        public string VehicleMake { get; set; }

        /// <summary>
        /// Vehicle model year.
        /// </summary>
        [DataMember(Name = "vehicle_model_year", EmitDefaultValue = false)]
        public Nullable<int> VehicleModelYear { get; set; }

        /// <summary>
        /// Vehicle model.
        /// </summary>
        [DataMember(Name = "vehicle_model", EmitDefaultValue = false)]
        public string VehicleModel { get; set; }

        /// <summary>
        /// The year, vehicle was acquired.
        /// </summary>
        [DataMember(Name = "vehicle_year_acquired", EmitDefaultValue = false)]
        public Nullable<int> VehicleYearAcquired { get; set; }

        /// <summary>
        /// A cost of the new vehicle.
        /// </summary>
        [DataMember(Name = "vehicle_cost_new", EmitDefaultValue = false)]
        public Nullable<double> VehicleCostNew { get; set; }

        /// <summary>
        /// If true, the vehicle was purchased new.
        /// </summary>
        [DataMember(Name = "purchased_new", EmitDefaultValue = false)]
        public Nullable<bool> PurchasedNew { get; set; }

        /// <summary>
        /// Start date of the license.
        /// </summary>
        [DataMember(Name = "license_start_date", EmitDefaultValue = false)]
        public string LicenseStartDate { get; set; }

        /// <summary>
        /// End date of the license.
        /// </summary>
        [DataMember(Name = "license_end_date", EmitDefaultValue = false)]
        public string LicenseEndDate { get; set; }

        /// <summary>
        /// A number of the vecile's axles.
        /// </summary>
        [DataMember(Name = "vehicle_axle_count", EmitDefaultValue = false)]
        public Nullable<int> VehicleAxleCount { get; set; }

        /// <summary>
        /// Miles per gallon in the city area.
        /// </summary>
        [DataMember(Name = "mpg_city", EmitDefaultValue = false)]
        public Nullable<int> MpgCity { get; set; }

        /// <summary>
        /// Miles per gallon in the highway area.
        /// </summary>
        [DataMember(Name = "mpg_highway", EmitDefaultValue = false)]
        public Nullable<int> MpgHighway { get; set; }

        /// <summary>
        /// A type of the fuel.
        /// <para>Available values:</para>
        /// <value>unleaded 87, unleaded 89, unleaded 91, unleaded 93, diesel, electric, hybrid</value>
        /// </summary>
        [DataMember(Name = "fuel_type", EmitDefaultValue = false)]
        public string FuelType { get; set; }

        /// <summary>
        /// Height of the vehicle in the inches.
        /// </summary>
        [DataMember(Name = "height_inches", EmitDefaultValue = false)]
        public Nullable<int> HeightInches { get; set; }

        /// <summary>
        /// Weight of the vehicle in the pounds.
        /// </summary>
        [DataMember(Name = "weight_lb", EmitDefaultValue = false)]
        public Nullable<int> WeightLb { get; set; }

        /// <summary>
        /// If true, the vehicle is operational.
        /// </summary>
        [DataMember(Name = "is_operational", EmitDefaultValue = false)]
        public Nullable<bool> IsOperational { get; set; }

        /// <summary>
        /// External telematics vehicle ID.
        /// </summary>
        [DataMember(Name = "External telematics vehicle ID", EmitDefaultValue = false)]
        public string ExternalTelematicsVehicleID { get; set; }

        /// <summary>
        /// If true, the vehicle has trailer.
        /// </summary>
        [DataMember(Name = "has_trailer", EmitDefaultValue = false)]
        public bool HasTrailer { get; set; }

        /// <summary>
        /// Vehicle height in inches.
        /// </summary>
        [DataMember(Name = "heightInInches", EmitDefaultValue = false)]
        public string HeightInInches { get; set; }

        /// <summary>
        /// Vehicle length in inches.
        /// </summary>
        [DataMember(Name = "lengthInInches", EmitDefaultValue = false)]
        public Nullable<int> LengthInInches { get; set; }

        /// <summary>
        /// Vehicle width in inches.
        /// </summary>
        [DataMember(Name = "widthInInches", EmitDefaultValue = false)]
        public Nullable<int> WidthInInches { get; set; }

        /// <summary>
        /// Maximum weight per axle group in pounds.
        /// </summary>
        [DataMember(Name = "maxWeightPerAxleGroupInPounds", EmitDefaultValue = false)]
        public Nullable<int> MaxWeightPerAxleGroupInPounds { get; set; }

        /// <summary>
        /// Number of the axles.
        /// </summary>
        [DataMember(Name = "numAxles", EmitDefaultValue = false)]
        public Nullable<int> NumAxles { get; set; }

        /// <summary>
        /// Weight in pounds.
        /// </summary>
        [DataMember(Name = "weightInPounds", EmitDefaultValue = false)]
        public Nullable<int> WeightInPounds { get; set; }

        /// <summary>
        /// Hazardous materials type.
        /// <para>Available values:</para>
        /// <value>'INVALID', 'NONE', 'GENERAL', 'EXPLOSIVE', 
        /// 'INHALANT', 'RADIOACTIVE', 'CAUSTIC', 'FLAMMABLE', 'HARMFUL_TO_WATER'</value>
        /// </summary>
        [DataMember(Name = "HazmatType", EmitDefaultValue = false)]
        public string HazmatType { get; set; }

        /// <summary>
        /// Low emission zone preference.
        /// </summary>
        [DataMember(Name = "LowEmissionZonePref", EmitDefaultValue = false)]
        public string LowEmissionZonePref { get; set; }

        /// <summary>
        /// If equal to 'YES', optimization algorithm will use 53 foot trailer routing.
        /// <para>Available values:</para>
        /// <value>'YES', 'NO'</value>
        /// </summary>
        [DataMember(Name = "Use53FootTrailerRouting", EmitDefaultValue = false)]
        public string Use53FootTrailerRouting { get; set; }

        /// <summary>
        /// If equal to 'YES', optimization algorithm will use national network.
        /// <para>Available values:</para>
        /// <value>'YES', 'NO'</value>
        /// </summary>
        [DataMember(Name = "UseNationalNetwork", EmitDefaultValue = false)]
        public string UseNationalNetwork { get; set; }

        /// <summary>
        /// If equal to 'YES', optimization algorithm will use truck restrictions.
        /// <para>Available values:</para>
        /// <value>'YES', 'NO'</value>
        /// </summary>
        [DataMember(Name = "UseTruckRestrictions", EmitDefaultValue = false)]
        public string UseTruckRestrictions { get; set; }

        /// <summary>
        /// If equal to 'YES', optimization algorithm will avoid ferries.
        /// <para>Available values:</para>
        /// <value>'YES', 'NO'</value>.
        /// </summary>
        [DataMember(Name = "AvoidFerries", EmitDefaultValue = false)]
        public string AvoidFerries { get; set; }

        /// <summary>
        /// Divided highway avoid preference (e.g. NEUTRAL).
        /// </summary>
        [DataMember(Name = "DividedHighwayAvoidPreference", EmitDefaultValue = false)]
        public string DividedHighwayAvoidPreference { get; set; }

        /// <summary>
        /// Freeway avoid preference.
        /// <para>Available values:</para>
        /// <value>'STRONG_AVOID', 'AVOID', 'NEUTRAL', 'FAVOR', 'STRONG_FAVOR'</value>
        /// </summary>
        [DataMember(Name = "FreewayAvoidPreference", EmitDefaultValue = false)]
        public string FreewayAvoidPreference { get; set; }

        /// <summary>
        /// If equal to 'YES', optimization algorithm will use 'International borders open' option.
        /// <para>Available values:</para>
        /// <value>'YES', 'NO'</value>
        /// </summary>
        [DataMember(Name = "InternationalBordersOpen", EmitDefaultValue = false)]
        public string InternationalBordersOpen { get; set; }

        /// <summary>
        /// Toll road usage.
        /// <para>Available values:</para>
        /// <value>'STRONG_AVOID', 'AVOID', 'NEUTRAL', 'FAVOR', 'STRONG_FAVOR'</value>
        /// </summary>
        [DataMember(Name = "TollRoadUsage", EmitDefaultValue = false)]
        public string TollRoadUsage { get; set; }

        /// <summary>
        /// If true, the vehicle uses only highway.
        /// </summary>
        [DataMember(Name = "hwy_only", EmitDefaultValue = false)]
        public bool HwyOnly { get; set; }

        /// <summary>
        /// If true, the vehicle is long combination.
        /// </summary>
        [DataMember(Name = "long_combination_vehicle", EmitDefaultValue = false)]
        public bool LongCombinationVehicle { get; set; }

        /// <summary>
        /// If true, the vehicle should avoid highways
        /// </summary>
        [DataMember(Name = "avoid_highways", EmitDefaultValue = false)]
        public bool AvoidHighways { get; set; }

        /// <summary>
        /// Side street adherence.
        /// <para>Available values:</para>
        /// <value>'OFF', 'MINIMAL', 'MODERATE', 'AVERAGE', 'STRICT', 'ADHERE', 'STRONGLYHERE'</value>
        /// </summary>
        [DataMember(Name = "side_street_adherence", EmitDefaultValue = false)]
        public string SideStreetAdherence { get; set; }

        /// <summary>
        /// Truck configuration.
        /// <para>Available values:</para>
        /// <value>'NONE', 'PASSENGER', '28_DOUBLETRAILER', '48_STRAIGHT_TRUCK', '48_SEMI_TRAILER', 
        /// '53_SEMI_TRAILER', 'FULLSIZEVAN', '26_STRAIGHT_TRUCK'</value>
        /// </summary>
        [DataMember(Name = "truck_config", EmitDefaultValue = false)]
        public string TruckConfig { get; set; }

        /// <summary>
        /// Vehicle height in metric unit.
        /// </summary>
        [DataMember(Name = "height_metric", EmitDefaultValue = false)]
        public Nullable<float> HeightMetric { get; set; }

        /// <summary>
        /// Vehicle length in metric unit.
        /// </summary>
        [DataMember(Name = "length_metric", EmitDefaultValue = false)]
        public Nullable<float> LengthMetric { get; set; }

        /// <summary>
        /// Vehicle width in metric unit.
        /// </summary>
        [DataMember(Name = "width_metric", EmitDefaultValue = false)]
        public Nullable<float> WidthMetric { get; set; }

        /// <summary>
        /// Vehicle weight in metric unit.
        /// </summary>
        [DataMember(Name = "weight_metric", EmitDefaultValue = false)]
        public Nullable<float> WeightMetric { get; set; }

        /// <summary>
        /// Maximum weight per axle group in metric unit.
        /// </summary>
        [DataMember(Name = "max_weight_per_axle_group_metric", EmitDefaultValue = false)]
        public Nullable<float> MaxWeightPerAxleGroupMetric { get; set; }

        /// <summary>
        /// When the vehicle was removed.
        /// </summary>
        [DataMember(Name = "timestamp_removed", EmitDefaultValue = false)]
        public string TimestampRemoved { get; set; }
    }
}
