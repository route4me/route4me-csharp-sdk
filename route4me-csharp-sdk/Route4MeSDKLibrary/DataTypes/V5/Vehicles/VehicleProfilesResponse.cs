using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response from the process of retrieving the vehicle profiles.
    /// </summary>
    [DataContract]
    public sealed class VehicleProfilesResponse : GenericParameters
    {
        /// <summary>
        ///     Current page of the paginated vehicle profiles list.
        /// </summary>
        [DataMember(Name = "current_page", EmitDefaultValue = false)]
        public int? CurrentPage { get; set; }

        /// <summary>
        ///     An array of the vehicle profiles
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public VehicleProfile[] Data { get; set; }

        /// <summary>
        ///     URL to the first page of the paginated vehicle profiles list.
        /// </summary>
        [DataMember(Name = "first_page_url", EmitDefaultValue = false)]
        public string FirstPageUrl { get; set; }

        /// <summary>
        ///     From which vehicle profile is starting the page
        /// </summary>
        [DataMember(Name = "from", EmitDefaultValue = false)]
        public int? From { get; set; }

        /// <summary>
        ///     Last page
        /// </summary>
        [DataMember(Name = "last_page", EmitDefaultValue = false)]
        public int? LastPage { get; set; }

        /// <summary>
        ///     URL to the last page of the paginated vehicle profiles list.
        /// </summary>
        [DataMember(Name = "last_page_url", EmitDefaultValue = false)]
        public string LastPageUrl { get; set; }

        /// <summary>
        ///     URL to the next page of the paginated vehicle profiles list.
        /// </summary>
        [DataMember(Name = "next_page_url", EmitDefaultValue = false)]
        public string NextPageUrl { get; set; }

        /// <summary>
        ///     Path to the API endpoint
        /// </summary>
        [DataMember(Name = "path", EmitDefaultValue = false)]
        public string Path { get; set; }

        /// <summary>
        ///     Vehicles number per page
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        /// <summary>
        ///     URL to the previous page
        /// </summary>
        [DataMember(Name = "prev_page_url", EmitDefaultValue = false)]
        public string PreviousPageUrl { get; set; }

        /// <summary>
        ///     To which vehicle profile is ending the page
        /// </summary>
        [DataMember(Name = "to", EmitDefaultValue = false)]
        public int? To { get; set; }

        /// <summary>
        ///     Total number of the vehicles.
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }
    }
}