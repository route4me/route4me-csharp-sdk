using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Subclass of the Activity class.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class ActivityMember : GenericParameters
    {
        /// <summary>
        /// The member ID
        /// </summary>
        [DataMember(Name = "member_id")]
        public string member_id { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        [DataMember(Name = "member_first_name")]
        public string member_first_name { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        [DataMember(Name = "member_last_name")]
        public string member_last_name { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        [DataMember(Name = "member_email")]
        public string member_email { get; set; }
    }
}
