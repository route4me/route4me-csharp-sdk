using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Vehicle profile data structure.
    /// </summary>
    [DataContract]
    public sealed class VehicleProfile : GenericParameters
    {
        /// <summary>
        ///     Vehicle profile ID
        /// </summary>
        [DataMember(Name = "vehicle_profile_id", EmitDefaultValue = false)]
        public int? VehicleProfileId { get; set; }

        /// <summary>
        ///     Root member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public int? RootMemberId { get; set; }

        /// <summary>
        ///     Vehicle profile name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     Vehicle height
        /// </summary>
        [DataMember(Name = "height", EmitDefaultValue = false)]
        public double? Height { get; set; }

        /// <summary>
        ///     Vehicle width
        /// </summary>
        [DataMember(Name = "width", EmitDefaultValue = false)]
        public double? Width { get; set; }

        /// <summary>
        ///     Vehicle length
        /// </summary>
        [DataMember(Name = "length", EmitDefaultValue = false)]
        public double? Length { get; set; }

        /// <summary>
        ///     Vehicle weight
        /// </summary>
        [DataMember(Name = "weight", EmitDefaultValue = false)]
        public double? Weight { get; set; }

        [DataMember(Name = "max_weight_per_axle", EmitDefaultValue = false)]
        public double? MaxWeightPerAxle { get; set; }

        /// <summary>
        ///     When the profile deleted
        /// </summary>
        [DataMember(Name = "deleted_at", EmitDefaultValue = false)]
        public string DeletedAt { get; set; }

        /// <summary>
        ///     When the profile created
        /// </summary>
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }

        /// <summary>
        ///     When the profile updated
        /// </summary>
        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public string UpdatedAt { get; set; }

        /// <summary>
        ///     A type of the fuel
        ///     enum: ['unleaded 87','unleaded 89','unleaded 91','unleaded 93','diesel','electric','hybrid']
        /// </summary>
        [DataMember(Name = "fuel_type", EmitDefaultValue = false)]
        public string FuelType { get; set; }

        /// <summary>
        ///     Fuel consumption city
        /// </summary>
        [DataMember(Name = "fuel_consumption_city", EmitDefaultValue = false)]
        public double? FuelConsumptionCity { get; set; }

        /// <summary>
        ///     Fuel consumption in the highway area
        /// </summary>
        [DataMember(Name = "fuel_consumption_highway", EmitDefaultValue = false)]
        public double? FuelConsumptionHighway { get; set; }

        /// <summary>
        ///     Type of a hazardous material.
        ///     enum: ['general', 'explosives', 'flammable', 'inhalants', 'caustic', 'radioactive']
        /// </summary>
        [DataMember(Name = "hazmat_type", EmitDefaultValue = false)]
        public string HazmatType { get; set; }

        /// <summary>
        ///     If true, the profile is predefined.
        /// </summary>
        [DataMember(Name = "is_predefined", EmitDefaultValue = false)]
        public bool? IsPredefined { get; set; }

        /// <summary>
        ///     If true, the profile is default.
        /// </summary>
        [DataMember(Name = "is_default", EmitDefaultValue = false)]
        public bool? IsDefault { get; set; }

        /// <summary>
        ///     Height units (e.g. 'ft', 'm')
        /// </summary>
        [DataMember(Name = "height_units", EmitDefaultValue = false)]
        public string HeightUnits { get; set; }

        /// <summary>
        ///     Width units (e.g. 'ft', 'm')
        /// </summary>
        [DataMember(Name = "width_units", EmitDefaultValue = false)]
        public string WidthUnits { get; set; }

        /// <summary>
        ///     Length units (e.g. 'ft', 'm')
        /// </summary>
        [DataMember(Name = "length_units", EmitDefaultValue = false)]
        public string LengthUnits { get; set; }

        /// <summary>
        ///     Weight units (e.g. 'lb', 'kg')
        /// </summary>
        [DataMember(Name = "weight_units", EmitDefaultValue = false)]
        public string WeightUnits { get; set; }

        /// <summary>
        ///     Maximum weight per axle units (e.g. 'lb', 'kg')
        /// </summary>
        [DataMember(Name = "max_weight_per_axle_units", EmitDefaultValue = false)]
        public string MaxWeightPerAxleUnits { get; set; }

        /// <summary>
        ///     Fuel consumption units in the city area (e.g. mpg)
        /// </summary>
        [DataMember(Name = "fuel_consumption_city_unit", EmitDefaultValue = false)]
        public string FuelConsumptionCityUnit { get; set; }

        /// <summary>
        ///     Fuel consumption units in the highway area (e.g. mpg)
        /// </summary>
        [DataMember(Name = "fuel_consumption_highway_unit", EmitDefaultValue = false)]
        public string FuelConsumptionHighwayUnit { get; set; }

        /// <summary>
        ///     Height UF value (e.g. "7'")
        /// </summary>
        [DataMember(Name = "height_uf_value", EmitDefaultValue = false)]
        public string HeightUfValue { get; set; }

        /// <summary>
        ///     Width UF value (e.g. "8'")
        /// </summary>
        [DataMember(Name = "width_uf_value", EmitDefaultValue = false)]
        public string WidthUfValue { get; set; }

        /// <summary>
        ///     Length UF value (e.g. "20'")
        /// </summary>
        [DataMember(Name = "length_uf_value", EmitDefaultValue = false)]
        public string LengthUfValue { get; set; }

        /// <summary>
        ///     Weight UF value (e.g. "8,500lb")
        /// </summary>
        [DataMember(Name = "weight_uf_value", EmitDefaultValue = false)]
        public string WeightUfValue { get; set; }

        /// <summary>
        ///     Maximum weight per axle (UF value, e.g. "8,500lb")
        /// </summary>
        [DataMember(Name = "max_weight_per_axle_uf_value", EmitDefaultValue = false)]
        public string MaxWeightPerAxleUfValue { get; set; }

        /// <summary>
        ///     Fuel consumption city (UF value, e.g. "20.01 mi/l")
        /// </summary>
        [DataMember(Name = "fuel_consumption_city_uf_value", EmitDefaultValue = false)]
        public string FuelConsumptionCityUfValue { get; set; }

        /// <summary>
        ///     Fuel consumption highway (UF value, e.g. "2,000.01 mpg")
        /// </summary>
        [DataMember(Name = "fuel_consumption_highway_uf_value", EmitDefaultValue = false)]
        public string FuelConsumptionHighwayUfValue { get; set; }
    }
}