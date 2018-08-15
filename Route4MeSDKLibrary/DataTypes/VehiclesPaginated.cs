using Route4MeSDK.QueryTypes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class VehiclesPaginated : GenericParameters
    {
        [DataMember(Name = "current_page", EmitDefaultValue = false)]
        public int CurrentPage{ get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public VehicleV4Response[] Data { get; set; }

        [DataMember(Name = "first_page_url", EmitDefaultValue = false)]
        public string FirstPageUrl { get; set; }

        [DataMember(Name = "from", EmitDefaultValue = false)]
        public int From { get; set; }

        [DataMember(Name = "last_page", EmitDefaultValue = false)]
        public int LastPage { get; set; }

        [DataMember(Name = "last_page_url", EmitDefaultValue = false)]
        public string LastPageUrl { get; set; }

        [DataMember(Name = "next_page_url", EmitDefaultValue = false)]
        public string NextPageUrl { get; set; }

        [DataMember(Name = "path", EmitDefaultValue = false)]
        public string Path { get; set; }

        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int PerPage { get; set; }

        [DataMember(Name = "prev_page_url", EmitDefaultValue = false)]
        public string PrevPageUrl { get; set; }

        [DataMember(Name = "to", EmitDefaultValue = false)]
        public int To { get; set; }

        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int Total { get; set; }
    }
}
