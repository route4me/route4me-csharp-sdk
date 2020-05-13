using System.Collections.Generic;

namespace Route4MeSDK.QueryTypes
{
    public sealed class NoteParameters : GenericParameters
    {
        public NoteParameters()
        {
            Format = "json";
        }

        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
        public int AddressId { get; set; }

        [HttpQueryMemberAttribute(Name = "dev_lat")]
        public double Latitude { get; set; }

        [HttpQueryMemberAttribute(Name = "dev_lng")]
        public double Longitude { get; set; }

        [HttpQueryMemberAttribute(Name = "device_type")]
        public string DeviceType { get; set; }

        [HttpQueryMemberAttribute(Name = "format")]
        public string Format { get; set; }

        /// <summary>
        /// An acitivity type related to the note.
        /// API equivalent: strUpdateType.
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// Text content of the note.
        /// API equivalent: strNoteContents.
        /// </summary>
        public string StrNoteContents { get; set; }

        /// <summary>
        /// A temporary filename of a prepared for uploading file.
        /// API equivalent: strFileName.
        /// </summary>
        public string StrFileName { get; set; }

        /// <summary>
        /// Form data parameter. 
        /// Example item: "custom_note_type[412]": "do a service", 
        /// where 412 is "note_custom_type_id"
        /// </summary>
        public Dictionary<string, string> CustomNoteTypes { get; set; }
    }
}
