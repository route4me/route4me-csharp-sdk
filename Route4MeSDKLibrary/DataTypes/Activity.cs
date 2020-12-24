using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Activity Type Class
    /// </summary>
    [DataContract]
    public sealed class Activity : GenericParameters
    {
        /// <summary>
        /// Activity ID
        /// </summary>
        [DataMember(Name = "activity_id", EmitDefaultValue = false)]
        public string ActivityId { get; set; }

        /// <summary>
        /// Activity Type. See <see cref="https://github.com/route4me/route4me-json-schemas/blob/master/Activity.dtd#L23"/> 
        /// </summary>
        [DataMember(Name = "activity_type", EmitDefaultValue = false)]
        public string ActivityType { get; set; }

        /// <summary>
        /// Activity timestamp - The time when the activity occurred.
        /// </summary>
        [DataMember(Name = "activity_timestamp", EmitDefaultValue = false)]
        public long? ActivityTimestamp { get; set; }

        /// <summary>
        /// Activity message
        /// </summary>
        [DataMember(Name = "activity_message", EmitDefaultValue = false)]
        public string ActivityMessage { get; set; }

        /// <summary>
        /// Member ID
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public string MemberId { get; set; }

        /// <summary>
        /// Parent route ID
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        /// Parent route name
        /// </summary>
        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        public string RouteName { get; set; }

        /// <summary>
        /// Route destination ID
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public string RouteDestinationId { get; set; }

        /// <summary>
        /// Note ID
        /// </summary>
        [DataMember(Name = "note_id", EmitDefaultValue = false)]
        public string NoteId { get; set; }

        /// <summary>
        /// Note type
        /// </summary>
        [DataMember(Name = "note_type", EmitDefaultValue = false)]
        public string NoteType { get; set; }

        /// <summary>
        /// Note contents
        /// </summary>
        [DataMember(Name = "note_contents", EmitDefaultValue = false)]
        public string NoteContents { get; set; }

        /// <summary>
        /// URL of the uploaded note.
        /// </summary>
        [DataMember(Name = "note_file", EmitDefaultValue = false)]
        public string NoteFile { get; set; }

        /// <summary>
        /// Member-owner of the activity.
        /// </summary>
        [DataMember(Name = "member", EmitDefaultValue = false)]
        public ActivityMember Member { get; set; }

        /// <summary>
        /// A route destination name
        /// </summary>
        [DataMember(Name = "destination_name", EmitDefaultValue = false)]
        public string DestinationName { get; set; }

        /// <summary>
        /// A route destination alias
        /// </summary>
        [DataMember(Name = "destination_alias", EmitDefaultValue = false)]
        public string DestinationAlias { get; set; }
    }

    [DataContract]
    public class ActivityMember : GenericParameters
    {
        /// <summary>
        /// Member ID
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public int? MemberId { get; set; }

        /// <summary>
        /// First name of an user created an activity.
        /// </summary>
        [DataMember(Name = "member_first_name", EmitDefaultValue = false)]
        public string MemberFirstName { get; set; }

        /// <summary>
        /// Last name of an user created an activity.
        /// </summary>
        [DataMember(Name = "member_last_name", EmitDefaultValue = false)]
        public string MemberLastName { get; set; }

        /// <summary>
        /// Email of an user created an activity.
        /// </summary>
        [DataMember(Name = "member_email", EmitDefaultValue = false)]
        public string MemberEmail { get; set; }
    }
}
