using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class VehicleV4Response : GenericParameters
    {
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public string MemberId { get; set; }

        [DataMember(Name = "is_deleted", EmitDefaultValue = false)]
        public System.Nullable<bool> IsDeleted { get; set; }

        [DataMember(Name = "vehicle_alias", EmitDefaultValue = false)]
        public string VehicleAlias { get; set; }

        [DataMember(Name = "vehicle_vin", EmitDefaultValue = false)]
        public string VehicleVin { get; set; }

        [DataMember(Name = "created_time", EmitDefaultValue = false)]
        public string CreatedTime { get; set; }

        [DataMember(Name = "vehicle_reg_state_id", EmitDefaultValue = false)]
        public string VehicleRegStateId { get; set; }

        [DataMember(Name = "vehicle_reg_country_id", EmitDefaultValue = false)]
        public System.Nullable<int> VehicleRegCountryId { get; set; }

        [DataMember(Name = "vehicle_license_plate", EmitDefaultValue = false)]
        public string VehicleLicensePlate { get; set; }

        [DataMember(Name = "vehicle_type_id", EmitDefaultValue = false)]
        public string VehicleTypeID { get; set; }

        [DataMember(Name = "timestamp_added", EmitDefaultValue = false)]
        public string TimestampAdded { get; set; }

        [DataMember(Name = "vehicle_make", EmitDefaultValue = false)]
        public string VehicleMake { get; set; }

        [DataMember(Name = "vehicle_model_year", EmitDefaultValue = false)]
        public System.Nullable<int> VehicleModelYear { get; set; }

        [DataMember(Name = "vehicle_model", EmitDefaultValue = false)]
        public string VehicleModel { get; set; }

        [DataMember(Name = "vehicle_year_acquired", EmitDefaultValue = false)]
        public System.Nullable<int> VehicleYearAcquired { get; set; }

        [DataMember(Name = "vehicle_cost_new", EmitDefaultValue = false)]
        public System.Nullable<double> VehicleCostNew { get; set; }

        [DataMember(Name = "purchased_new", EmitDefaultValue = false)]
        public System.Nullable<bool> PurchasedNew { get; set; }

        [DataMember(Name = "license_start_date", EmitDefaultValue = false)]
        public string LicenseStartDate { get; set; }

        [DataMember(Name = "license_end_date", EmitDefaultValue = false)]
        public string LicenseEndDate { get; set; }

        [DataMember(Name = "vehicle_axle_count", EmitDefaultValue = false)]
        public System.Nullable<int> VehicleAxleCount { get; set; }

        [DataMember(Name = "mpg_city", EmitDefaultValue = false)]
        public System.Nullable<int> MpgCity { get; set; }

        [DataMember(Name = "mpg_highway", EmitDefaultValue = false)]
        public System.Nullable<int> MpgHighway { get; set; }

        [DataMember(Name = "fuel_type", EmitDefaultValue = false)]
        public string FuelType { get; set; }

        [DataMember(Name = "height_inches", EmitDefaultValue = false)]
        public System.Nullable<int> HeightInches { get; set; }

        [DataMember(Name = "weight_lb", EmitDefaultValue = false)]
        public System.Nullable<int> WeightLb { get; set; }

        [DataMember(Name = "is_operational", EmitDefaultValue = false)]
        public System.Nullable<bool> IsOperational { get; set; }

        [DataMember(Name = "external_telematics_vehicle_id", EmitDefaultValue = false)]
        public string ExternalTelematicsVehicleID { get; set; }

        [DataMember(Name = "has_trailer", EmitDefaultValue = false)]
        public bool HasTrailer { get; set; }

        [DataMember(Name = "heightInInches", EmitDefaultValue = false)]
        public string HeightInInches { get; set; }

        [DataMember(Name = "lengthInInches", EmitDefaultValue = false)]
        public System.Nullable<int> LengthInInches { get; set; }

        [DataMember(Name = "widthInInches", EmitDefaultValue = false)]
        public System.Nullable<int> WidthInInches { get; set; }

        [DataMember(Name = "maxWeightPerAxleGroupInPounds", EmitDefaultValue = false)]
        public System.Nullable<int> MaxWeightPerAxleGroupInPounds { get; set; }

        [DataMember(Name = "numAxles", EmitDefaultValue = false)]
        public System.Nullable<int> NumAxles { get; set; }

        [DataMember(Name = "weightInPounds", EmitDefaultValue = false)]
        public System.Nullable<int> WeightInPounds { get; set; }

        [DataMember(Name = "HazmatType", EmitDefaultValue = false)]
        public string HazmatType { get; set; }

        [DataMember(Name = "LowEmissionZonePref", EmitDefaultValue = false)]
        public string LowEmissionZonePref { get; set; }

        [DataMember(Name = "Use53FootTrailerRouting", EmitDefaultValue = false)]
        public string Use53FootTrailerRouting { get; set; }

        [DataMember(Name = "UseNationalNetwork", EmitDefaultValue = false)]
        public string UseNationalNetwork { get; set; }

        [DataMember(Name = "UseTruckRestrictions", EmitDefaultValue = false)]
        public string UseTruckRestrictions { get; set; }

        [DataMember(Name = "AvoidFerries", EmitDefaultValue = false)]
        public string AvoidFerries { get; set; }

        [DataMember(Name = "DividedHighwayAvoidPreference", EmitDefaultValue = false)]
        public string DividedHighwayAvoidPreference { get; set; }

        [DataMember(Name = "FreewayAvoidPreference", EmitDefaultValue = false)]
        public string FreewayAvoidPreference { get; set; }

        [DataMember(Name = "InternationalBordersOpen", EmitDefaultValue = false)]
        public string InternationalBordersOpen { get; set; }

        [DataMember(Name = "TollRoadUsage", EmitDefaultValue = false)]
        public string TollRoadUsage { get; set; }

        [DataMember(Name = "hwy_only", EmitDefaultValue = false)]
        public bool HwyOnly { get; set; }

        [DataMember(Name = "long_combination_vehicle", EmitDefaultValue = false)]
        public bool LongCombinationVehicle { get; set; }

        [DataMember(Name = "avoid_highways", EmitDefaultValue = false)]
        public bool AvoidHighways { get; set; }

        [DataMember(Name = "side_street_adherence", EmitDefaultValue = false)]
        public string SideStreetAdherence { get; set; }

        [DataMember(Name = "truck_config", EmitDefaultValue = false)]
        public string TruckConfig { get; set; }

        [DataMember(Name = "height_metric", EmitDefaultValue = false)]
        public System.Nullable<float> HeightMetric { get; set; }

        [DataMember(Name = "length_metric", EmitDefaultValue = false)]
        public System.Nullable<float> LengthMetric { get; set; }

        [DataMember(Name = "width_metric", EmitDefaultValue = false)]
        public System.Nullable<float> WidthMetric { get; set; }

        [DataMember(Name = "weight_metric", EmitDefaultValue = false)]
        public System.Nullable<float> WeightMetric { get; set; }

        [DataMember(Name = "max_weight_per_axle_group_metric", EmitDefaultValue = false)]
        public System.Nullable<float> MaxWeightPerAxleGroupMetric { get; set; }

        [DataMember(Name = "timestamp_removed", EmitDefaultValue = false)]
        public string TimestampRemoved { get; set; }
    }
}
