using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    /// Order inventory class
    /// </summary>
    [DataContract]
    public sealed class OrderInventory
    {
        /// <summary>
        /// Unique inventory ID
        /// </summary>
        [DataMember(Name = "inventory_id", EmitDefaultValue = false)]
        public int? InventoryId { get; set; }

        /// <summary>
        /// Unique order ID"
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public int? OrderId { get; set; }

        /// <summary>
        /// Order inventory name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Order inventory quantity
        /// </summary>
        [DataMember(Name = "quantity", EmitDefaultValue = false)]
        public int? Quantity { get; set; }

        /// <summary>
        /// Total weight of the order inventory.
        /// </summary>
        [DataMember(Name = "total_weight", EmitDefaultValue = false)]
        public double? TotalWeight { get; set; }

        /// <summary>
        /// Total volume of the inventory.
        /// </summary>
        [DataMember(Name = "total_volume", EmitDefaultValue = false)]
        public double? TotalVolume { get; set; }

        /// <summary>
        /// Total cost of the inventory.
        /// </summary>
        [DataMember(Name = "total_cost", EmitDefaultValue = false)]
        public double? TotalCost { get; set; }

        /// <summary>
        /// Total price of the inventory.
        /// </summary>
        [DataMember(Name = "total_price", EmitDefaultValue = false)]
        public double? TotalPrice { get; set; }

        /// <summary>
        /// When the inventory created.
        /// </summary>
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }

        /// <summary>
        /// When the inventory updated.
        /// </summary>
        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public string UpdatedAt { get; set; }
    }
}
