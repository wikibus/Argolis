using System.Reflection;
using Hydra.Core;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedPropertyFactory"/>
    /// </summary>
    public class DefaultSupportedPropertyFactory : ISupportedPropertyFactory
    {
        /// <summary>
        /// Creates a hydra <see cref="Property" /> from a type's property
        /// using sensible defaults.
        /// </summary>
        public Property Create(PropertyInfo prop)
        {
            return new Property();
        }
    }
}