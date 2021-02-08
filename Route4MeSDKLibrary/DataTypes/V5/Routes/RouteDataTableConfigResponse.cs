using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Route datatable configuration
    /// </summary>
    public class RouteDataTableConfigResponse
    {
        /// <summary>
        /// API Capabilities
        /// </summary>
        [DataMember(Name = "api_capabilities", EmitDefaultValue = false)]
        public ApiCapabilitiesCls ApiCapabilities { get; set; }

        /// <summary>
        /// API Preferences
        /// </summary>
        [DataMember(Name = "api_preferences", EmitDefaultValue = false)]
        public ApiPreferencesCls ApiPreferences { get; set; }
    }

    public class ApiCapabilitiesCls
    {
        /// <summary>
        /// Sortable Fields
        /// </summary>
        [DataMember(Name = "sortable_fields", EmitDefaultValue = false)]
        public string[] SortableFields { get; set; }

        /// <summary>
        /// Combinations of the sortable fields
        /// </summary>
        [DataMember(Name = "sortable_fields_combinations", EmitDefaultValue = false)]
        public List<string[]> SortableFieldsCombinations { get; set; }

        /// <summary>
        /// If true, multi-sorting enabled.
        /// </summary>
        [DataMember(Name = "multi_sorting_enabled", EmitDefaultValue = false)]
        public bool? MultiSortingEnabled { get; set; }

        /// <summary>
        /// An array of the filterable fields.
        /// </summary>
        [DataMember(Name = "filterable_fields", EmitDefaultValue = false)]
        public string[] FilterableFields { get; set; }

        /// <summary>
        /// If true, search enabled.
        /// </summary>
        [DataMember(Name = "search", EmitDefaultValue = false)]
        public bool? Search { get; set; }
    }

    public class ApiPreferencesCls
    {
        /// <summary>
        /// Force the server side search.
        /// </summary>
        [DataMember(Name = "force_server_side_search", EmitDefaultValue = false)]
        public bool? ForceServerSideSearch { get; set; }

        /// <summary>
        /// If true, the search result loaded partially.
        /// </summary>
        [DataMember(Name = "partial_load", EmitDefaultValue = false)]
        public bool? PartialLoad { get; set; }

        /// <summary>
        /// If true, simple pagination enabled.
        /// </summary>
        [DataMember(Name = "simple_pagination", EmitDefaultValue = false)]
        public bool? SimplePagination { get; set; }
    }
}
