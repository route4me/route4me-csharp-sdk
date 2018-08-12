using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class CustomNoteType
    {
        [DataMember(Name = "note_custom_type_id", EmitDefaultValue = false)]
        public int NoteCustomTypeID { get; set; }

        [DataMember(Name = "note_custom_type", EmitDefaultValue = false)]
        public string NoteCustomType { get; set; }

        [DataMember(Name = "root_owner_member_id", EmitDefaultValue = false)]
        public int RootOwnerMemberID { get; set; }

        [DataMember(Name = "note_custom_type_values")]
        public string[] NoteCustomTypeValues { get; set; }
    }
}

