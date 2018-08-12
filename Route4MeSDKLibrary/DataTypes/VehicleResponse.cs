using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class VehicleResponse : GenericParameters
    {
        [DataMember(Name = "vehicle_id")]
        public string VehicleId
        {
            get { return m_VehicleId; }
            set { m_VehicleId = value; }
        }
        private string m_VehicleId;

        [DataMember(Name = "created_time")]
        public string CreatedTime
        {
            get { return m_CreatedTime; }
            set { m_CreatedTime = value; }
        }
        private string m_CreatedTime;

        [DataMember(Name = "member_id")]
        public string MemberId
        {
            get { return m_MemberId; }
            set { m_MemberId = value; }
        }
        private string m_MemberId;

        [DataMember(Name = "vehicle_alias")]
        public string VehicleAlias
        {
            get { return m_VehicleAlias; }
            set { m_VehicleAlias = value; }
        }
        private string m_VehicleAlias;

        [DataMember(Name = "vehicle_vin")]
        public string VehicleVin
        {
            get { return m_VehicleVin; }
            set { m_VehicleVin = value; }
        }
        private string m_VehicleVin;

        [DataMember(Name = "vehicle_reg_state")]
        public string VehicleRegState
        {
            get { return m_VehicleRegState; }
            set { m_VehicleRegState = value; }
        }
        private string m_VehicleRegState;

        [DataMember(Name = "vehicle_reg_state_id")]
        public System.Nullable<int> VehicleRegStateId
        {
            get { return m_VehicleRegStateId; }
            set { m_VehicleRegStateId = value; }
        }
        private System.Nullable<int> m_VehicleRegStateId;

        [DataMember(Name = "vehicle_reg_country")]
        public string VehicleRegCountry
        {
            get { return m_VehicleRegCountry; }
            set { m_VehicleRegCountry = value; }
        }
        private string m_VehicleRegCountry;

        [DataMember(Name = "vehicle_reg_country_id")]
        public System.Nullable<int> VehicleRegCountryId
        {
            get { return m_VehicleRegCountryId; }
            set { m_VehicleRegCountryId = value; }
        }
        private System.Nullable<int> m_VehicleRegCountryId;

        [DataMember(Name = "vehicle_license_plate")]
        public string VehicleLicensePlate
        {
            get { return m_VehicleLicensePlate; }
            set { m_VehicleLicensePlate = value; }
        }
        private string m_VehicleLicensePlate;

        [DataMember(Name = "vehicle_make")]
        public string VehicleMake
        {
            get { return m_VehicleMake; }
            set { m_VehicleMake = value; }
        }
        private string m_VehicleMake;

        [DataMember(Name = "vehicle_model_year")]
        public System.Nullable<int> VehicleModelYear
        {
            get { return m_VehicleModelYear; }
            set { m_VehicleModelYear = value; }
        }
        private System.Nullable<int> m_VehicleModelYear;

        [DataMember(Name = "vehicle_model")]
        public string VehicleModel
        {
            get { return m_VehicleModel; }
            set { m_VehicleModel = value; }
        }
        private string m_VehicleModel;

        [DataMember(Name = "vehicle_year_acquired")]
        public System.Nullable<int> VehicleYearAcquired
        {
            get { return m_VehicleYearAcquired; }
            set { m_VehicleYearAcquired = value; }
        }
        private System.Nullable<int> m_VehicleYearAcquired;

        [DataMember(Name = "vehicle_cost_new")]
        public System.Nullable<double> VehicleCostNew
        {
            get { return m_VehicleCostNew; }
            set { m_VehicleCostNew = value; }
        }
        private System.Nullable<double> m_VehicleCostNew;

        [DataMember(Name = "license_start_date")]
        public string LicenseStartDate
        {
            get { return m_LicenseStartDate; }
            set { m_LicenseStartDate = value; }
        }
        private string m_LicenseStartDate;

        [DataMember(Name = "license_end_date")]
        public string LicenseEndDate
        {
            get { return m_LicenseEndDate; }
            set { m_LicenseEndDate = value; }
        }
        private string m_LicenseEndDate;

        [DataMember(Name = "vehicle_axle_count")]
        public System.Nullable<int> VehicleAxleCount
        {
            get { return m_VehicleAxleCount; }
            set { m_VehicleAxleCount = value; }
        }
        private System.Nullable<int> m_VehicleAxleCount;

        [DataMember(Name = "mpg_city")]
        public System.Nullable<double> MpgCity
        {
            get { return m_MpgCity; }
            set { m_MpgCity = value; }
        }
        private System.Nullable<double> m_MpgCity;

        [DataMember(Name = "mpg_highway")]
        public System.Nullable<double> MpgHighway
        {
            get { return m_MpgHighway; }
            set { m_MpgHighway = value; }
        }
        private System.Nullable<double> m_MpgHighway;

        [DataMember(Name = "fuel_type")]
        public string FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }
        private string m_FuelType;

        [DataMember(Name = "height_inches")]
        public System.Nullable<double> HeightInches
        {
            get { return m_HeightInches; }
            set { m_HeightInches = value; }
        }
        private System.Nullable<double> m_HeightInches;

        [DataMember(Name = "weight_lb")]
        public System.Nullable<double> WeightLb
        {
            get { return m_WeightLb; }
            set { m_WeightLb = value; }
        }
        private System.Nullable<double> m_WeightLb;

        [DataMember(Name = "is_operational")]
        public System.Nullable<bool> IsOperational
        {
            get { return m_IsOperational; }
            set { m_IsOperational = value; }
        }
        private System.Nullable<bool> m_IsOperational;
    }
}
