using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Manifest of a route address. Subclass of the class Address.
    /// </summary>
    [DataContract]
    public sealed class AddressManifest
    {
        /// <summary>
        ///     How much time is to be spent on service from the start in seconds.
        /// </summary>
        [DataMember(Name = "running_service_time", EmitDefaultValue = false)]
        public long? RunningServiceTime { get; set; }

        /// <summary>
        ///     How much time is spent driving from the start in seconds.
        /// </summary>
        [DataMember(Name = "running_travel_time", EmitDefaultValue = false)]
        public long? RunningTravelTime { get; set; }

        /// <summary>
        ///     Running wait time.
        /// </summary>
        [DataMember(Name = "running_wait_time", EmitDefaultValue = false)]
        public long? RunningWaitTime { get; set; }

        /// <summary>
        ///     Distance traversed before reaching this address.
        /// </summary>
        [DataMember(Name = "running_distance", EmitDefaultValue = false)]
        public double? RunningDistance { get; set; }

        /// <summary>
        ///     Expected fuel consumption from the start.
        /// </summary>
        [DataMember(Name = "fuel_from_start", EmitDefaultValue = false)]
        public double? FuelFromStart { get; set; }

        /// <summary>
        ///     Expected fuel cost from start.
        /// </summary>
        [DataMember(Name = "fuel_cost_from_start", EmitDefaultValue = false)]
        public double? FuelCostFromStart { get; set; }

        /// <summary>
        ///     Projected arrival time UTC unixtime.
        /// </summary>
        [DataMember(Name = "projected_arrival_time_ts", EmitDefaultValue = false)]
        public long? ProjectedArrivalTimeTs { get; set; }

        /// <summary>
        ///     Estimated departure time UTC unixtime.
        /// </summary>
        [DataMember(Name = "projected_departure_time_ts", EmitDefaultValue = false)]
        public long? ProjectedDepartureTimeTs { get; set; }

        /// <summary>
        ///     Time when the address was marked as visited UTC unixtime.
        ///     This is actually equal to timestamp_last_visited most of the time.
        /// </summary>
        [DataMember(Name = "actual_arrival_time_ts", EmitDefaultValue = false)]
        public long? ActualArrivalTimeTs { get; set; }

        /// <summary>
        ///     Time when the address was mared as departed UTC.
        ///     This is actually equal to timestamp_last_departed most of the time.
        /// </summary>
        [DataMember(Name = "actual_departure_time_ts", EmitDefaultValue = false)]
        public long? ActualDepartureTimeTs { get; set; }

        /// <summary>
        ///     Estimated arrival time based on the current route progress,
        ///     i.e. based on the last known actual_arrival_time.
        /// </summary>
        [DataMember(Name = "estimated_arrival_time_ts", EmitDefaultValue = false)]
        public long? EstimatedArrivalTimeTs { get; set; }

        /// <summary>
        ///     Estimated departure time based on the current route progress.
        /// </summary>
        [DataMember(Name = "estimated_departure_time_ts", EmitDefaultValue = false)]
        public long? EstimatedDepartureTimeTs { get; set; }

        /// <summary>
        ///     Scheduled arrival time.
        /// </summary>
        [DataMember(Name = "scheduled_arrival_time_ts", EmitDefaultValue = false)]
        public long? ScheduledArrivalTimeTs { get; set; }

        /// <summary>
        ///     Scheduled departure time.
        /// </summary>
        [DataMember(Name = "scheduled_departure_time_ts", EmitDefaultValue = false)]
        public long? ScheduledDepartureTimeTs { get; set; }

        /// <summary>
        ///     This is the difference between the originally projected arrival time and Actual Arrival Time.
        /// </summary>
        [DataMember(Name = "time_impact", EmitDefaultValue = false)]
        public long? TimeImpact { get; set; }

        /// <summary>
        ///     Distance traversed before reaching this address.
        /// </summary>
        [DataMember(Name = "udu_running_distance", EmitDefaultValue = false)]
        public double? UduRunningDistance { get; set; }
    }
}