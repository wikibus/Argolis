using System.Reflection;
using JsonLD.Entities;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Selects all public properties except Id
    /// </summary>
    public class AllPublicPropertiesSelectionPolicy : ISupportedPropertySelectionPolicy
    {
        /// <summary>
        /// Determines whether the the given <paramref name="property" /> should
        /// be included as hydra:SupportedClass.
        /// </summary>
        public bool ShouldIncludeProperty(PropertyInfo property)
        {
            var propertName = property.GetJsonPropertyName();

            return propertName != JsonLdKeywords.Id
                && propertName != JsonLdKeywords.Type
                && propertName != JsonLdKeywords.Context;
        }
    }
}
