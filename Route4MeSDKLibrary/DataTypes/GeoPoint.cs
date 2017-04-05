using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class GeoPoint
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
    }
}
