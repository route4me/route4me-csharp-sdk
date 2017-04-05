using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class TerritoryQuery : GenericParameters
    {
        /// <summary>
        /// Device Id
        /// </summary>
        [HttpQueryMemberAttribute(Name = "device_id", EmitDefaultValue = false)]
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }
        private string m_DeviceID;

        /// <summary>
        /// Territory Id
        /// </summary>
        [HttpQueryMemberAttribute(Name = "territory_id", EmitDefaultValue = false)]
        public string TerritoryId
        {
            get { return m_TerritoryId; }
            set { m_TerritoryId = value; }
        }
        private string m_TerritoryId;

        /// <summary>
        /// Territory Id
        /// </summary>
        [HttpQueryMemberAttribute(Name = "addresses", EmitDefaultValue = false)]
        public System.Nullable<int> addresses
        {
            get { return m_addresses; }
            set { m_addresses = value; }
        }
        private System.Nullable<int> m_addresses;
    }
}
