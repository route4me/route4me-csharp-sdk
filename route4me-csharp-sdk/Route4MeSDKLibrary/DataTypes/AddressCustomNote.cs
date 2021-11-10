using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The class for the custom note of a route destination.
    /// </summary>
    [DataContract]
    public sealed class AddressCustomNote
    {
        /// <summary>
        ///     A unique ID (40 chars) of a custom note entry.
        /// </summary>
        [DataMember(Name = "note_custom_entry_id", EmitDefaultValue = false)]
        public string NoteCustomEntryID { get; set; }

        /// <summary>
        ///     The custom note ID.
        /// </summary>
        [DataMember(Name = "note_id", EmitDefaultValue = false)]
        public string NoteID { get; set; }

        /// <summary>
        ///     The custom note type ID.
        /// </summary>
        [DataMember(Name = "note_custom_type_id", EmitDefaultValue = false)]
        public string NoteCustomTypeID { get; set; }

        /// <summary>
        ///     The custom note value.
        /// </summary>
        [DataMember(Name = "note_custom_value")]
        public string NoteCustomValue { get; set; }

        /// <summary>
        ///     The custom note type.
        /// </summary>
        [DataMember(Name = "note_custom_type")]
        public string NoteCustomType { get; set; }
    }
}