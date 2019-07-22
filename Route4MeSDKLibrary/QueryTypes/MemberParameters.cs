using System;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes
{
    /// <summary>
    /// Parameters for the member request (authentication, registration, session validation).
    /// </summary>
    /// <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    public sealed class MemberParameters : GenericParameters
    {
        /// <summary>
        /// Session GUID of a user.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "session_guid", EmitDefaultValue = false)]
        public string SessionGuid { get; set; }

        /// <summary>
        /// Response format as query parameter.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "format", EmitDefaultValue = false)]
        public string ResponseFormat { get; set; }

        /// <summary>
        /// Unique ID of a member.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "member_id", EmitDefaultValue = false)]
        public Nullable<int> MemberId { get; set; }

        /// <summary>
        /// The plan type that the user selected.
        /// <para>Available values: <value>'free', 'basic', 'pro', 'premium', 'personal'.</value></para>
        /// </summary>
        [HttpQueryMemberAttribute(Name = "plan", EmitDefaultValue = false)]
        public string Plan { get; set; }

        /// <summary>
        /// The plan type that the user selected.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "member_type", EmitDefaultValue = false)]
        public Nullable<int> MemberType { get; set; }

        /// <summary>
        /// Member email
        /// </summary>
        [DataMember(Name = "strEmail", EmitDefaultValue = false)]
        public string StrEmail { get; set; }

        /// <summary>
        /// Member password
        /// </summary>
        [DataMember(Name = "strPassword", EmitDefaultValue = false)]
        public string StrPassword { get; set; }

        /// <summary>
        /// Response format.
        /// <para>Available values: <value>'json', 'xml'.</value></para>
        /// </summary>
        [DataMember(Name = "format", EmitDefaultValue = false)]
        public string Format { get; set; }

        /// <summary>
        /// Response format.
        /// <para>Available values:</para>
        /// <value>'Airport Shuttle Service', 'Alarm and Security', 'Appliance Install/Repair', 'Asset Recovery', </value>
        /// <para><value>'Auto Parts/Repair', 'Beauty Supply', 'Cable/Satellite Sales', 'Cable/Satellite Installation', </value></para>
        /// <value>'Carpet Cleaning', 'Charitable Foundation', 'Distribution', 'Driveway Sealcoating', </value>
        /// <para><value>'Education / Tutor', 'Electricians', 'Farming and Agriculture', 'Federal Government', </value></para>
        /// <value>'Fire and Water Restoration', 'Fire Extinguishers', 'Fleet Maintenance and Repair', 'Fleet/Trucking', </value>
        /// <para><value>'Florists', 'Food - Catering', 'Gutter Cleaning', 'Home Health Care', </value></para>
        /// <value>'HVAC - Heating and AC', 'Inspections', 'Janitorial', 'Laboratory Courier', </value>
        /// <para><value>'Laundromat / Cleaners', 'Law Enforcement', 'Maintenance', 'Medical Equipment Installation', </value></para>
        /// <value>'Municipal Government', 'Non-Profit Organization', 'Patio and Deck', 'Pest Control', </value>
        /// <para><value>'Pet Sitting', 'Political Organization', 'Pool Maintenance', 'Printing and Press', </value></para>
        /// <value>'Real Estate', 'Residential Cleaning', 'Retail Furniture', 'Satellite Dish Installation', </value>
        /// <para><value>'Secret Shopper', 'Septic Tank Cleaning', 'Siding Installation Service', 'State Government', </value></para>
        /// <value>'Taxi/Limo Service', 'Technicians', 'Telecommunications', 'Vehicle Transport', </value>
        /// <para><value>'Waste Collection', 'Other'</value></para>
        /// </summary>
        [DataMember(Name = "strIndustry", EmitDefaultValue = false)]
        public string StrIndustry { get; set; }

        /// <summary>
        /// First name of a member.
        /// </summary>
        [DataMember(Name = "strFirstName", EmitDefaultValue = false)]
        public string StrFirstName { get; set; }

        /// <summary>
        /// Last name of a member.
        /// </summary>
        [DataMember(Name = "strLastName", EmitDefaultValue = false)]
        public string StrLastName { get; set; }

        /// <summary>
        /// If equal to 1, a user agreed to the Route4Me terms.
        /// <para>Available values: <value>1, 0.</value></para>
        /// </summary>
        [DataMember(Name = "chkTerms", EmitDefaultValue = false)]
        public Nullable<int> ChkTerms { get; set; }

        /// <summary>
        /// The type of device making this request.
        /// <para>Available values:</para>
        /// <value>web</value>, 
        /// <value>iphone</value>, 
        /// <value>ipad</value>, 
        /// <value>android_phone</value>, 
        /// <value>android_tablet</value>
        /// </summary>
        [DataMember(Name = "device_type", EmitDefaultValue = false)]
        public string DeviceType { get; set; }

        /// <summary>
        /// User Password.
        /// </summary>
        [DataMember(Name = "strPassword_1", EmitDefaultValue = false)]
        public string StrPassword_1 { get; set; }

        /// <summary>
        /// User confirmation Password.
        /// </summary>
        [DataMember(Name = "strPassword_2", EmitDefaultValue = false)]
        public string StrPassword_2 { get; set; }

        /// <summary>
        /// User subaccount type.
        /// <para>Available values:</para>
        /// <value>PRIMARY_ACCOUNT, SUB_ACCOUNT_ADMIN, SUB_ACCOUNT_REGIONAL_MANAGER,</value>
        /// <para><value>SUB_ACCOUNT_DISPATCHER, SUB_ACCOUNT_PLANNER, SUB_ACCOUNT_DRIVER,</value></para>
        /// <value>SUB_ACCOUNT_ANALYSTSUB_ACCOUNT_VENDORSUB_ACCOUNT_CUSTOMER_SERVICE.</value>
        /// </summary>
        [DataMember(Name = "strSubAccountType", EmitDefaultValue = true)]
        public string strSubAccountType { get; set; }

        /// <summary>
        /// If true, marketing proposal is dissabled.
        /// </summary>
        [DataMember(Name = "blDisableMarketing", EmitDefaultValue = true)]
        public bool blDisableMarketing { get; set; }

        /// <summary>
        /// If true, an account activation email is dissabled.
        /// </summary>
        [DataMember(Name = "blDisableAccountActivationEmail", EmitDefaultValue = true)]
        public bool blDisableAccountActivationEmail { get; set; }
    }
}
