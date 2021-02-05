using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    public class RouteDataTableConfigResponse
    {
        [DataMember(Name = "api_capabilities", EmitDefaultValue = false)]
        public ApiCapabilitiesCls ApiCapabilities { get; set; }

        [DataMember(Name = "api_preferences", EmitDefaultValue = false)]
        public ApiPreferencesCls ApiPreferences { get; set; }
    }

    public class ApiCapabilitiesCls
    {
        [DataMember(Name = "sortable_fields", EmitDefaultValue = false)]
        public string[] SortableFields { get; set; }

        [DataMember(Name = "sortable_fields_combinations", EmitDefaultValue = false)]
        public List<string[]> SortableFieldsCombinations { get; set; }

        [DataMember(Name = "multi_sorting_enabled", EmitDefaultValue = false)]
        public bool? MultiSortingEnabled { get; set; }

        [DataMember(Name = "filterable_fields", EmitDefaultValue = false)]
        public string[] FilterableFields { get; set; }

        [DataMember(Name = "search", EmitDefaultValue = false)]
        public bool? Search { get; set; }
    }

    public class ApiPreferencesCls
    {
        [DataMember(Name = "force_server_side_search", EmitDefaultValue = false)]
        public bool? ForceServerSideSearch { get; set; }

        [DataMember(Name = "partial_load", EmitDefaultValue = false)]
        public bool? PartialLoad { get; set; }

        [DataMember(Name = "simple_pagination", EmitDefaultValue = false)]
        public bool? SimplePagination { get; set; }
    }
}
