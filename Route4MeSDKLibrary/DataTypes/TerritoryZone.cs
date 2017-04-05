using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Territory Zone
    /// </summary>
    [DataContract]
    public sealed class TerritoryZone
    {
        ///<summary>
        /// Avoidance zone id
        ///</summary>
        [DataMember(Name = "territory_id")]
        public string TerritoryId
        {
            get { return m_TerritoryId; }
            set { m_TerritoryId = value; }
        }
        private string m_TerritoryId;

        ///<summary>
        /// Territory name
        ///</summary>
        [DataMember(Name = "territory_name")]
        public string TerritoryName
        {
            get { return m_TerritoryName; }
            set { m_TerritoryName = value; }
        }
        private string m_TerritoryName;

        ///<summary>
        /// Territory color 
        ///</summary>
        [DataMember(Name = "territory_color")]
        public string TerritoryColor
        {
            get { return m_TerritoryColor; }
            set { m_TerritoryColor = value; }
        }
        private string m_TerritoryColor;

        ///<summary>
        /// Territory addresses 
        ///</summary>
        [DataMember(Name = "addresses")]
        public int[] addresses
        {
            get { return m_addresses; }
            set { m_addresses = value; }
        }
        private int[] m_addresses;

        ///<summary>
        /// Member Id
        ///</summary>
        [DataMember(Name = "member_id")]
        public string MemberId
        {
            get { return m_MemberId; }
            set { m_MemberId = value; }
        }
        private string m_MemberId;

        ///<summary>
        /// Territory parameters
        ///</summary>
        [DataMember(Name = "territory")]
        public Territory Territory
        {
            get { return m_Territory; }
            set { m_Territory = value; }
        }
        private Territory m_Territory;
    }
}
