using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Paginated response from the vehicle request. See also <seealso cref="VehicleV4Response"/>.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    [DataContract]
    public sealed class VehiclesPaginated : GenericParameters
    {
        /// <summary>
        /// Current page number in the vehicles collection.
        /// </summary>
        [DataMember(Name = "current_page", EmitDefaultValue = false)]
        public int CurrentPage{ get; set; }

        /// <summary>
        /// An array of the VehicleV4Response type objects. See <see cref="VehicleV4Response"/>
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public VehicleV4Response[] Data { get; set; }

        /// <summary>
        /// URL to the first page.
        /// </summary>
        [DataMember(Name = "first_page_url", EmitDefaultValue = false)]
        public string FirstPageUrl { get; set; }

        /// <summary>
        /// From which vehicle is starting the page.
        /// </summary>
        [DataMember(Name = "from", EmitDefaultValue = false)]
        public int From { get; set; }

        /// <summary>
        /// Last page number in the vehicles collection.
        /// </summary>
        [DataMember(Name = "last_page", EmitDefaultValue = false)]
        public int LastPage { get; set; }

        /// <summary>
        /// URL to the last page.
        /// </summary>
        [DataMember(Name = "last_page_url", EmitDefaultValue = false)]
        public string LastPageUrl { get; set; }

        /// <summary>
        /// URL to the next page.
        /// </summary>
        [DataMember(Name = "next_page_url", EmitDefaultValue = false)]
        public string NextPageUrl { get; set; }

        /// <summary>
        /// Path to the API endpoint.
        /// </summary>
        [DataMember(Name = "path", EmitDefaultValue = false)]
        public string Path { get; set; }

        /// <summary>
        /// Vehicles number per page.
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int PerPage { get; set; }

        /// <summary>
        /// URL to the previous page.
        /// </summary>
        [DataMember(Name = "prev_page_url", EmitDefaultValue = false)]
        public string PrevPageUrl { get; set; }

        /// <summary>
        /// To which vehicle is ending the page.
        /// </summary>
        [DataMember(Name = "to", EmitDefaultValue = false)]
        public int To { get; set; }

        /// <summary>
        /// Total number of the vehicles.
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int Total { get; set; }
    }
}
