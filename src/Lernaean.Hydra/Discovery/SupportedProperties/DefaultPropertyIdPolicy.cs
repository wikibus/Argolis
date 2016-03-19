using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Default <see cref="IPropertyPredicateIdPolicy"/> with a sensible
    /// fallback based on containing class
    /// </summary>
    public class DefaultPropertyIdPolicy : IPropertyPredicateIdPolicy
    {
        private const string SlashClassIdAppendFormat = "{0}#{1}";
        private const string HashClassIdAppendFormat = "{0}/{1}";

        /// <summary>
        /// Gets the concatenated property identifier from the <see cref="JsonPropertyAttribute"/>
        /// or as concatenation of class and property name.
        /// </summary>
        public string GetPropertyId(PropertyInfo property, string propertyName, Uri classId)
        {
            var jsonProperty = property.GetCustomAttribute<JsonPropertyAttribute>();

            if (jsonProperty != null && jsonProperty.PropertyName != null)
            {
                return jsonProperty.PropertyName;
            }

            return GetFallbackPropertyId(property, propertyName, classId);
        }

        /// <summary>
        /// Gets the fallback property identifier if <see cref="JsonPropertyAttribute"/>
        /// wasn't present on <paramref name="property"/>.
        /// </summary>
        protected virtual string GetFallbackPropertyId(PropertyInfo property, string propertyName, Uri classId)
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
