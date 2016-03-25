using System.Reflection;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Contract for retrieving the property identifier
    /// of a supported property predicate
    /// </summary>
    public interface IPropertyPredicateIdPolicy
    {
        /// <summary>
        /// Gets the property identifier.
        /// </summary>
        string GetPropertyId(PropertyInfo property);
    }
}
