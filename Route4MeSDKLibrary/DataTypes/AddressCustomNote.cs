using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class AddressCustomNote
    {
        [DataMember(Name = "note_custom_entry_id", EmitDefaultValue = false)]
        public int NoteCustomEntryID { get; set; }

        [DataMember(Name = "note_id", EmitDefaultValue = false)]
        public string NoteID { get; set; }

        [DataMember(Name = "note_custom_type_id", EmitDefaultValue = false)]
        public int NoteCustomTypeID { get; set; }

        [DataMember(Name = "note_custom_value")]
        public string NoteCustomValue { get; set; }

        [DataMember(Name = "note_custom_type")]
        public string NoteCustomType { get; set; }
    }
}