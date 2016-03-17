using System.Reflection;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedPropertyMetaProvider"/>
    /// </summary>
    public class DefaultSupportedPropertyMetaProvider : ISupportedPropertyMetaProvider
    {
        /// <summary>
        /// Gets the <see cref="SupportedPropertyMeta"/> based on property features
        /// and common attributes.
        /// </summary>
        public SupportedPropertyMeta GetMeta(PropertyInfo property)
        {
            return new SupportedPropertyMeta
            {
                Title = property.Name
            };
        }
    }
}