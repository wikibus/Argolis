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
            var description = GetDescription(property);

            return new SupportedPropertyMeta
            {
                Title = property.Name,
                Description = description,
                Writeable = isReadonly == false
            };
        }

        private static string GetDescription(PropertyInfo property)
        {
            var readOnlyAttribute = property.GetCustomAttribute<DescriptionAttribute>();

            return readOnlyAttribute != null ? readOnlyAttribute.Description : null;
        }

        private static bool HasReadonlyAttribute(PropertyInfo property)
        {
            var readOnlyAttribute = property.GetCustomAttribute<ReadOnlyAttribute>();

            return readOnlyAttribute != null && readOnlyAttribute.IsReadOnly;
        }
    }
}