using System.Reflection;
using Hydra.Core;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Contract for classes 
    /// </summary>
    public interface ISupportedPropertyFactory
    {
        /// <summary>
        /// Creates a hydra <see cref="SupportedProperty"/> from a type's property.
        /// </summary>
        SupportedProperty Create(PropertyInfo prop);
    }
}