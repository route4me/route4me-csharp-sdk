using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class FindAssetResponse
    {
        [DataMember(Name = "tracking_number")]
        public string TrackingNumber
        {
            get { return m_TrackingNumber; }
            set { m_TrackingNumber = value; }
        }
        private string m_TrackingNumber;

        [DataMember(Name = "status_history")]
        public string[] StatusHistory
        {
            get { return m_StatusHistory; }
            set { m_StatusHistory = value; }
        }
        private string[] m_StatusHistory;

        [DataMember(Name = "locations")]
        public FindAssetResponseLocations[] Locations
        {
            get { return m_Locations; }
            set { m_Locations = value; }
        }
        private FindAssetResponseLocations[] m_Locations;

        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        public Dictionary<string, string> CustomData
        {
            get { return m_CustomData; }
            set { m_CustomData = value; }
        }
        private Dictionary<string, string> m_CustomData;

        [DataMember(Name = "arrival")]
        public FindAssetResponseArrival[] Arrival
        {
            get { return m_Arrival; }
            set { m_Arrival = value; }
        }
        private FindAssetResponseArrival[] m_Arrival;

        [DataMember(Name = "delivered", EmitDefaultValue = false)]
        public System.Nullable<bool> Delivered
        {
            get { return m_Delivered; }
            set { m_Delivered = value; }
        }
        private System.Nullable<bool> m_Delivered;
    }

    [DataContract()]
    public sealed class FindAssetResponseLocations
    {
        [DataMember(Name = "lat")]
        public double Latitude
        {
            get { return m_Latitude; }
            set { m_Latitude = value; }
        }
        private double m_Latitude;

        [DataMember(Name = "lng")]
        public double Longitude
        {
            get { return m_Longitude; }
            set { m_Longitude = value; }
        }
        private double m_Longitude;

        [DataMember(Name = "icon")]
        public string Icon
        {
            get { return m_Icon; }
            set { m_Icon = value; }
        }
        private string m_Icon;
    }

    [DataContract()]
    public sealed class FindAssetResponseArrival
    {
        [DataMember(Name = "from_unix_timestamp")]
        public System.Nullable<int> FromUnixTimestamp
        {
            get { return m_FromUnixTimestamp; }
            set { m_FromUnixTimestamp = value; }
        }
        private System.Nullable<int> m_FromUnixTimestamp;


        [DataMember(Name = "to_unix_timestamp")]
        public System.Nullable<int> ToUnixTimestamp
        {
            get { return m_ToUnixTimestamp; }
            set { m_ToUnixTimestamp = value; }
        }
        private System.Nullable<int> m_ToUnixTimestamp;
    }

  }
