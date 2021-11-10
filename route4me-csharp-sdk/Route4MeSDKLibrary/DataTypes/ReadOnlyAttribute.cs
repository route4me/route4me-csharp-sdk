using System;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The class creates custom attribute IsReadOnly for marking
    ///     the Route4Me object properties as read-only.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field)]
    public class ReadOnlyAttribute : Attribute
    {
        private bool isReadOnly;

        public ReadOnlyAttribute(bool IsReadOnly)
        {
            isReadOnly = IsReadOnly;
        }

        public virtual bool IsReadOnly
        {
            get => isReadOnly;
            set => isReadOnly = value;
        }
    }
}