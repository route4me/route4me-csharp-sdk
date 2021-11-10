using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The CustomNoteType class
    /// </summary>
    [DataContract]
    public sealed class CustomNoteType
    {
        /// <summary>
        ///     The custom note type ID
        /// </summary>
        [DataMember(Name = "note_custom_type_id", EmitDefaultValue = false)]
        public int NoteCustomTypeID { get; set; }

        /// <summary>
        ///     The custom type
        /// </summary>
        [DataMember(Name = "note_custom_type", EmitDefaultValue = false)]
        public string NoteCustomType { get; set; }

        /// <summary>
        ///     The root owner member ID
        /// </summary>
        [DataMember(Name = "root_owner_member_id", EmitDefaultValue = false)]
        public int RootOwnerMemberID { get; set; }

        /// <summary>
        ///     An array of the custom type note values
        /// </summary>
        [DataMember(Name = "note_custom_type_values")]
        public string[] NoteCustomTypeValues { get; set; }
    }
}