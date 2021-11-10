using System.Collections.Generic;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    ///     Parameters for the address note(s) request.
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class NoteParameters : GenericParameters
    {
        /// <summary>Initializes a new instance of the <see cref="NoteParameters" /> class.</summary>
        public NoteParameters()
        {
            Format = "json";
        }

        /// <summary>
        ///     Unique ID of a route.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Unique ID of a route destination.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "address_id", EmitDefaultValue = false)]
        public int AddressId { get; set; }

        /// <summary>
        ///     Latitude of a device position.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "dev_lat")]
        public double Latitude { get; set; }

        /// <summary>
        ///     Longitude of a device position.
        ///     <remarks>
        ///         <para>Query parameter.</para>
        ///     </remarks>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "dev_lng")]
        public double Longitude { get; set; }

        /// <summary>
        ///     The type of device making this request.
        ///     <para>Available values:</para>
        ///     <value>web</value>
        ///     ,
        ///     <value>iphone</value>
        ///     ,
        ///     <value>ipad</value>
        ///     ,
        ///     <value>android_phone</value>
        ///     ,
        ///     <value>android_tablet</value>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "device_type")]
        public string DeviceType { get; set; }

        /// <summary>
        ///     Response format.
        ///     <para>Available values:</para>
        ///     <value>json, xml</value>
        ///     ,
        /// </summary>
        [HttpQueryMemberAttribute(Name = "format")]
        public string Format { get; set; }

        /// <summary>
        ///     Update type of a note file.
        ///     <para>Available values:</para>
        ///     <value>'DRIVER_IMG', 'VEHICLE_IMG', 'ADDRESS_IMG', 'CSV_FILE', 'XLS_FILE', 'ANY_FILE'</value>
        ///     ,
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        ///     Text content of the note.
        ///     API equivalent: strNoteContents.
        /// </summary>
        public string StrNoteContents { get; set; }

        /// <summary>
        ///     A temporary filename of a prepared for uploading file.
        ///     API equivalent: strFileName.
        /// </summary>
        public string StrFileName { get; set; }

        /// <summary>
        ///     Form data parameter.
        ///     Example item: "custom_note_type[412]": "do a service",
        ///     where 412 is "note_custom_type_id"
        /// </summary>
        public Dictionary<string, string> CustomNoteTypes { get; set; }
    }
}