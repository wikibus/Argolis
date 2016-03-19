using System;
using System.Reflection;
using NullGuard;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Calculates a property id by appending the property name to
    /// declaring class identifier
    /// </summary>
    public class ConcatenatedClassIdAndPropertyName : IPropertyIdFallbackStrategy
    {
        private const string SlashClassIdAppendFormat = "{0}#{1}";
        private const string HashClassIdAppendFormat = "{0}/{1}";

        /// <summary>
        /// Gets the concatenated property identifier.
        /// </summary>
        [return: AllowNull]
        public string GetPropertyId(PropertyInfo property, string propertyName, Uri classId)
        {
            var format = HashClassIdAppendFormat;

            if (string.IsNullOrWhiteSpace(classId.Fragment))
            {
                format = SlashClassIdAppendFormat;
            }

            return string.Format(format, classId, propertyName);
        }
    }
}
