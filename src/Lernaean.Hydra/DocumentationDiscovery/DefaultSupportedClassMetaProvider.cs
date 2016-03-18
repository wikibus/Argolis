using System;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedClassMetaProvider"/>
    /// </summary>
    public class DefaultSupportedClassMetaProvider : ISupportedClassMetaProvider
    {
        private const string DefaultDescription = "The {0} class";

        /// <summary>
        /// Gets the basic information about a supported class from it's reflected data and attributes.
        /// </summary>
        public virtual SupportedClassMeta GetMeta(Type type)
        {
            return new SupportedClassMeta
            {
                Title = type.Name,
                Description = type.GetDescription() ?? string.Format(DefaultDescription, type.Name)
            };
        }
    }
}