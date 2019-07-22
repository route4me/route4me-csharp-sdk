using Route4MeSDK.QueryTypes;
using System;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Response from the vehicle request.
    /// </summary>
    /// <seealso cref="VehicleV4Response" />
    [Obsolete("New endpoint uses the classes: 'VehicleV4Response' and 'VehiclesPaginated' instead")]
    [DataContract]
    public sealed class VehicleResponse : GenericParameters
    {
        /// <summary>
        /// The vehicle ID.
        /// </summary>
        [DataMember(Name = "vehicle_id")]
        public string VehicleId { get; set; }

        /// <summary>
        /// When the vehicle was created.
        /// </summary>
        [DataMember(Name = "created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// Member ID assigned to the vehicle.
        /// </summary>
        [DataMember(Name = "member_id")]
        public string MemberId { get; set; }

        /// <summary>
        /// Vehicle alias.
        /// </summary>
        [DataMember(Name = "vehicle_alias")]
        public string VehicleAlias { get; set; }

        /// <summary>
        /// Vehicle VIN.
        /// </summary>
        [DataMember(Name = "vehicle_vin")]
        public string VehicleVin { get; set; }

        /// <summary>
        /// Vehicle registration state.
        /// </summary>
        [DataMember(Name = "vehicle_reg_state")]
        public string VehicleRegState { get; set; }

        /// <summary>
        /// Vehicle registration state ID.
        /// </summary>
        [DataMember(Name = "vehicle_reg_state_id")]
        public Nullable<int> VehicleRegStateId { get; set; }

        /// <summary>
        /// Vehicle registration country.
        /// </summary>
        [DataMember(Name = "vehicle_reg_country")]
        public string VehicleRegCountry { get; set; }

        /// <summary>
        /// Vehicle registration country ID.
        /// </summary>
        [DataMember(Name = "vehicle_reg_country_id")]
        public Nullable<int> VehicleRegCountryId { get; set; }

        /// <summary>
        /// A license plate of the vehicle.
        /// </summary>
        [DataMember(Name = "vehicle_license_plate")]
        public string VehicleLicensePlate { get; set; }

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
        [DataMember(Name = "vehicle_make")]
        public string VehicleMake { get; set; }

        /// <summary>
        /// Vehicle model year.
        /// </summary>
        [DataMember(Name = "vehicle_model_year")]
        public Nullable<int> VehicleModelYear { get; set; }

        /// <summary>
        /// Vehicle model.
        /// </summary>
        [DataMember(Name = "vehicle_model")]
        public string VehicleModel { get; set; }

        /// <summary>
        /// The year, vehicle was acquired.
        /// </summary>
        [DataMember(Name = "vehicle_year_acquired")]
        public Nullable<int> VehicleYearAcquired { get; set; }

        /// <summary>
        /// A cost of the new vehicle.
        /// </summary>
        [DataMember(Name = "vehicle_cost_new")]
        public Nullable<double> VehicleCostNew { get; set; }

        /// <summary>
        /// Start date of the license.
        /// </summary>
        [DataMember(Name = "license_start_date")]
        public string LicenseStartDate { get; set; }

        /// <summary>
        /// End date of the license.
        /// </summary>
        [DataMember(Name = "license_end_date")]
        public string LicenseEndDate { get; set; }

        /// <summary>
        /// A number of the vecile's axles.
        /// </summary>
        [DataMember(Name = "vehicle_axle_count")]
        public Nullable<int> VehicleAxleCount { get; set; }

        /// <summary>
        /// Miles per gallon in the city area.
        /// </summary>
        [DataMember(Name = "mpg_city")]
        public Nullable<double> MpgCity { get; set; }

        /// <summary>
        /// Miles per gallon in the highway area.
        /// </summary>
        [DataMember(Name = "mpg_highway")]
        public Nullable<double> MpgHighway { get; set; }

        /// <summary>
        /// A type of the fuel.
        /// <para>Available values:</para>
        /// <value>unleaded 87, unleaded 89, unleaded 91, unleaded 93, diesel, electric, hybrid</value>
        /// </summary>
        [DataMember(Name = "fuel_type")]
        public string FuelType { get; set; }

        /// <summary>
        /// Height of the vehicle in the inches.
        /// </summary>
        [DataMember(Name = "height_inches")]
        public Nullable<double> HeightInches { get; set; }

        /// <summary>
        /// Weight of the vehicle in the pounds.
        /// </summary>
        [DataMember(Name = "weight_lb")]
        public Nullable<double> WeightLb { get; set; }

        /// <summary>
        /// If true, the vehicle is operational.
        /// </summary>
        [DataMember(Name = "is_operational")]
        public Nullable<bool> IsOperational { get; set; }
    }
}
