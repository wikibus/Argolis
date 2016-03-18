using System.ComponentModel;
using System.Reflection;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedPropertyMetaProvider"/>
    /// </summary>
    public class DefaultSupportedPropertyMetaProvider : ISupportedPropertyMetaProvider
    {
        private const string DefaultDescription = "The {0} property";

        /// <summary>
        /// Gets the <see cref="SupportedPropertyMeta"/> based on property features
        /// and common attributes.
        /// </summary>
        public virtual SupportedPropertyMeta GetMeta(PropertyInfo property)
        {
            var isReadonly = property.SetMethod.IsPrivate || HasReadonlyAttribute(property);
            var description = property.GetDescription() ?? string.Format(DefaultDescription, property.Name);

            return new SupportedPropertyMeta
            {
                Title = property.Name,
                Description = description,
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