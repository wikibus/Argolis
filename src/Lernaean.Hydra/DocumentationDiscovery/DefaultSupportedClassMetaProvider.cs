using System;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedClassMetaProvider"/>
    /// </summary>
    public class DefaultSupportedClassMetaProvider : ISupportedClassMetaProvider
    {
        /// <summary>
        /// Gets the basic information about a supported class from it's reflected data and attributes.
        /// </summary>
        public virtual SupportedClassMeta GetMeta(Type property)
        {
            return new SupportedClassMeta
            {
                Description = property.GetDescription()
            };
        }
    }
}