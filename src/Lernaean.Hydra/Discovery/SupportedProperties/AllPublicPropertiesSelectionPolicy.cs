using System.Reflection;
using Hydra.Discovery.SupportedClasses;

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
            return property.Name != "Id";
        }
    }
}