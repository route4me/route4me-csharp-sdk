using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response for the endpoint /vehicles/license
    /// </summary>
    [DataContract]
    public sealed class VehicleResponse : GenericParameters
    {
        /// <summary>
        ///     Vehicle data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DataVehicle Data { get; set; }
    }


    [DataContract]
    public class DataVehicle
    {
        /// <summary>
        ///     Reduced vehicle object
        /// </summary>
        [DataMember(Name = "vehicle", EmitDefaultValue = false)]
        public VehicleReduced Vehicle { get; set; }
    }

    /// <summary>
    ///     Reduced vehicle structure
    /// </summary>
    [DataContract]
    public class VehicleReduced
    {
        /// <summary>
        ///     The vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     Vehicle alias
        /// </summary>
        [DataMember(Name = "vehicle_alias", EmitDefaultValue = false)]
        public string VehicleAlias { get; set; }

        /// <summary>
        ///     Vehicle VIN
        /// </summary>
        [DataMember(Name = "vehicle_vin", EmitDefaultValue = false)]
        public string VehicleVin { get; set; }

        /// <summary>
        ///     A license plate of the vehicle.
        /// </summary>
        [DataMember(Name = "vehicle_license_plate", EmitDefaultValue = false)]
        public string VehicleLicensePlate { get; set; }

        /// <summary>
        ///     Vehicle maker brend.
        ///     <para>Available values:</para>
        ///     "american coleman", "bmw", "chevrolet", "ford", "freightliner", "gmc",
        ///     <para>"hino", "honda", "isuzu", "kenworth", "mack", "mercedes-benz", "mitsubishi", </para>
        ///     "navistar", "nissan", "peterbilt", "renault", "scania", "sterling", "toyota",
        ///     <para>"volvo", "western star" </para>
        /// </summary>
        [DataMember(Name = "vehicle_make", EmitDefaultValue = false)]
        public string VehicleMake { get; set; }

        /// <summary>
        ///     Vehicle model year
        /// </summary>
        [DataMember(Name = "vehicle_model_year", EmitDefaultValue = false)]
        public int? VehicleModelYear { get; set; }

        /// <summary>
        ///     Vehicle model
        /// </summary>
        [DataMember(Name = "vehicle_model", EmitDefaultValue = false)]
        public string VehicleModel { get; set; }

        /// <summary>
        ///     The year, vehicle was acquired
        /// </summary>
        [DataMember(Name = "vehicle_year_acquired", EmitDefaultValue = false)]
        public int? VehicleYearAcquired { get; set; }

        /// <summary>
        ///     A cost of the new vehicle
        /// </summary>
        [DataMember(Name = "vehicle_cost_new", EmitDefaultValue = false)]
        public double? VehicleCostNew { get; set; }

        /// <summary>
        ///     If true, the vehicle was purchased new.
        /// </summary>
        [DataMember(Name = "purchased_new", EmitDefaultValue = false)]
        public bool? PurchasedNew { get; set; }

        /// <summary>
        ///     Start date of the license
        /// </summary>
        [DataMember(Name = "license_start_date", EmitDefaultValue = false)]
        public string LicenseStartDate { get; set; }

        /// <summary>
        ///     End date of the license
        /// </summary>
        [DataMember(Name = "license_end_date", EmitDefaultValue = false)]
        public string LicenseEndDate { get; set; }

        /// <summary>
        ///     If equal to '1', the vehicle is operational.
        /// </summary>
        [DataMember(Name = "is_operational", EmitDefaultValue = false)]
        public bool? IsOsperational { get; set; }
    }
}