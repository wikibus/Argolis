using System.ComponentModel;
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
            var isReadonly = property.SetMethod.IsPrivate || HasReadonlyAttribute(property);

            return new SupportedPropertyMeta
            {
                Title = property.Name,
                Writeable = isReadonly == false
            };
        }

        private static bool HasReadonlyAttribute(PropertyInfo property)
        {
            var readOnlyAttribute = property.GetCustomAttribute<ReadOnlyAttribute>();

            return readOnlyAttribute != null && readOnlyAttribute.IsReadOnly;
        }
    }
}