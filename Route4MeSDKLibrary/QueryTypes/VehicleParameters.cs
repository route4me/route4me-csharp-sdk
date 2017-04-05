using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    public sealed class VehicleParameters : GenericParameters
    {
        /// <summary>
        /// Limit per page, if you use 0 you will get all records
        /// </summary>
        [HttpQueryMemberAttribute(Name = "limit", EmitDefaultValue = false)]
        public System.Nullable<uint> Limit
        {
            get { return m_Limit; }
            set { m_Limit = value; }
        }
        private System.Nullable<uint> m_Limit;

        /// <summary>
        /// Offset
        /// </summary>
        [HttpQueryMemberAttribute(Name = "offset", EmitDefaultValue = false)]
        public System.Nullable<uint> Offset
        {
            get { return m_Offset; }
            set { m_Offset = value; }
        }
        private System.Nullable<uint> m_Offset;

        /// <summary>
        /// Vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId
        {
            get { return m_VehicleId; }
            set { m_VehicleId = value; }
        }
        private string m_VehicleId;
    }
}

