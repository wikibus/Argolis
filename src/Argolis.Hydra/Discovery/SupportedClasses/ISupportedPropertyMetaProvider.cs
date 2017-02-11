using System.Reflection;
using Argolis.Hydra.Core;
using Argolis.Hydra.Discovery.SupportedProperties;

namespace Argolis.Hydra.Discovery.SupportedClasses
{
    /// <summary>
    /// Contract for returning basic information about a <see cref="SupportedProperty"/>
    /// </summary>
    public interface ISupportedPropertyMetaProvider
    {
        /// <summary>
        /// Gets the basic information about a supported property.
        /// </summary>
        /// <returns>
        /// An object containing the title, description, write- and readability
        /// </returns>
        SupportedPropertyMeta GetMeta(PropertyInfo property);
    }
}
