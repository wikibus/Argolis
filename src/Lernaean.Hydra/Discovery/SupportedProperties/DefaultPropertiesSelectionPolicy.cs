using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using JsonLD.Entities;
using Newtonsoft.Json;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Selects all public properties except Id
    /// </summary>
    public class DefaultPropertiesSelectionPolicy : ISupportedPropertySelectionPolicy
    {
        /// <summary>
        /// Determines whether the the given <paramref name="property" /> should
        /// be included as hydra:SupportedClass.
        /// </summary>
        public bool ShouldIncludeProperty(PropertyInfo property)
        {
            var propertyName = property.GetJsonPropertyName();

            return IsKeyword(propertyName) == false
                && property.GetCustomAttributes<JsonIgnoreAttribute>().Any() == false
                && property.GetCustomAttributes<IgnoreDataMemberAttribute>().Any() == false;
        }

        private static bool IsKeyword(string propertName)
        {
            return propertName == JsonLdKeywords.Id
                   || propertName == JsonLdKeywords.Type
                   || propertName == JsonLdKeywords.Context;
        }
    }
}
