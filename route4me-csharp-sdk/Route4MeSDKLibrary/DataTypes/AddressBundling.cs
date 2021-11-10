using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Class for the address bundling query
    /// </summary>
    [DataContract]
    public class AddressBundling : GenericParameters
    {
        /// <summary>
        ///     Address bundling mode
        /// </summary>
        [DataMember(Name = "mode", EmitDefaultValue = true)]
        [DefaultValue(AddressBundlingMode.Address)]
        [Range((int) AddressBundlingMode.Address, (int) AddressBundlingMode.Coordinates)]
        public AddressBundlingMode Mode { get; set; }

        /// <summary>
        ///     Address bundling mode parameters:
        ///     <para>If Mode=3, contains an array of the field names of the Address object</para>
        ///     <para>If Mode=4, contains an array of the custom fields of the Address object</para>
        /// </summary>
        [DataMember(Name = "mode_params", EmitDefaultValue = false)]
        public string[] ModeParams { get; set; }

        /// <summary>
        ///     Address bundling merge mode
        /// </summary>
        [DataMember(Name = "merge_mode", EmitDefaultValue = true)]
        [DefaultValue(AddressBundlingMergeMode.KeepAsSeparateDestinations)]
        [Range((int) AddressBundlingMergeMode.KeepAsSeparateDestinations,
            (int) AddressBundlingMergeMode.MergeIntoSingleDestination)]
        public AddressBundlingMergeMode MergeMode { get; set; }

        /// <summary>
        ///     Service time rules of the address bundling (<seealso cref="ServiceTimeRulesClass">)
        /// </summary>
        [DataMember(Name = "service_time_rules", EmitDefaultValue = false)]
        public ServiceTimeRulesClass ServiceTimeRules { get; set; }
    }

    /// <summary>
    ///     Class for the address bundling service time rules
    /// </summary>
    [DataContract]
    public class ServiceTimeRulesClass
    {
        /// <summary>
        ///     Mode of a first item of the bundled addresses.
        /// </summary>
        [DataMember(Name = "first_item_mode", EmitDefaultValue = true)]
        [DefaultValue(AddressBundlingFirstItemMode.KeepOriginal)]
        [Range((int) AddressBundlingFirstItemMode.KeepOriginal, (int) AddressBundlingFirstItemMode.CustomTime)]
        public AddressBundlingFirstItemMode FirstItemMode { get; set; }

        /// <summary>
        ///     First item mode parameters.
        ///     If FirstItemMode=AddressBundlingFirstItemMode.CustomTime, contains custom service time in seconds.
        /// </summary>
        [DataMember(Name = "first_item_mode_params", EmitDefaultValue = false)]
        public int[] FirstItemModeParams { get; set; }

        /// <summary>
        ///     Mode of the non-first items of the bundled addresses.
        /// </summary>
        [DataMember(Name = "additional_items_mode", EmitDefaultValue = true)]
        [DefaultValue(AddressBundlingAdditionalItemsMode.KeepOriginal)]
        [Range((int) AddressBundlingAdditionalItemsMode.KeepOriginal,
            (int) AddressBundlingAdditionalItemsMode.InheritFromPrimary)]
        public AddressBundlingAdditionalItemsMode AdditionalItemsMode { get; set; }

        /// <summary>
        ///     Additional items mode parameters:
        ///     <para>
        ///         if AdditionalItemsMode=AddressBundlingAdditionalItemsMode.CustomTime, contains an array of the custom service
        ///         times
        ///     </para>
        /// </summary>
        [DataMember(Name = "additional_items_mode_params", EmitDefaultValue = false)]
        public int[] AdditionalItemsModeParams { get; set; }
    }
}