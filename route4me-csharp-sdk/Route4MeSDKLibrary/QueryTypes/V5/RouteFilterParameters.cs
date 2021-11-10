using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     Route filter parameters.
    /// </summary>
    [DataContract]
    public sealed class RouteFilterParameters : GenericParameters
    {
        [DataMember(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public RouteFilterParametersFilters Filters { get; set; }

        [DataMember(Name = "directions", EmitDefaultValue = false)]
        public bool? Directions { get; set; }

        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public bool? Notes { get; set; }

        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        [DataMember(Name = "order_by", EmitDefaultValue = false)]
        public List<string[]> OrderBy { get; set; }

        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }
    }

    public class RouteFilterParametersFilters : GenericParameters
    {
        /// <summary>
        ///     An array of the scheduled dates.
        /// </summary>
        [DataMember(Name = "schedule_date", EmitDefaultValue = false)]
        public string[] ScheduleDate { get; set; }
    }
}