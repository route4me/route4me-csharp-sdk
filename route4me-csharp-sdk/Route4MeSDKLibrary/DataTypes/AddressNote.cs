using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The class for address note
    /// </summary>
    [DataContract]
    public sealed class AddressNote
    {
        /// <summary>
        ///     An unique ID of a note
        /// </summary>
        [DataMember(Name = "note_id", EmitDefaultValue = false)]
        public int NoteId { get; set; }

        /// <summary>
        ///     The route ID
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     The route destination ID
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public int RouteDestinationId { get; set; }

        /// <summary>
        ///     An unique ID of an uploaded file
        /// </summary>
        [DataMember(Name = "upload_id")]
        public string UploadId { get; set; }

        /// <summary>
        ///     When the note was added
        /// </summary>
        [DataMember(Name = "ts_added", EmitDefaultValue = false)]
        public long? TimestampAdded { get; set; }

        /// <summary>
        ///     The position latitude where the address note was added
        /// </summary>
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        /// <summary>
        ///     The position longitude where the address note was added
        /// </summary>
        [DataMember(Name = "lng")]
        public double Longitude { get; set; }

        /// <summary>
        ///     The activity type
        ///     See available activity types here:
        ///     <see cref="https://github.com/route4me/route4me-json-schemas/blob/master/Activity.dtd#L23" />
        /// </summary>
        [DataMember(Name = "activity_type")]
        public string ActivityType { get; set; }

        /// <summary>
        ///     The note text contents
        /// </summary>
        [DataMember(Name = "contents")]
        public string Contents { get; set; }

        /// <summary>
        ///     An upload type of the note
        /// </summary>
        [DataMember(Name = "upload_type")]
        public string UploadType { get; set; }

        /// <summary>
        ///     An upload url - where a file-note was uploaded.
        /// </summary>
        [DataMember(Name = "upload_url")]
        public string UploadUrl { get; set; }

        /// <summary>
        ///     An extension of the uploaded file.
        /// </summary>
        [DataMember(Name = "upload_extension")]
        public string UploadExtension { get; set; }

        /// <summary>
        ///     The device a note was uploaded from
        /// </summary>
        [DataMember(Name = "device_type")]
        public string DeviceType { get; set; }

        /// <summary>
        ///     Custom address notes
        /// </summary>
        [DataMember(Name = "custom_types")]
        public AddressCustomNote[] CustomTypes { get; set; }
    }
}