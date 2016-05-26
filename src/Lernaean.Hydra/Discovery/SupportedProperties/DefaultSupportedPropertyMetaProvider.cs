using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Hydra.Annotations;
using Hydra.Discovery.SupportedClasses;
using Newtonsoft.Json.Serialization;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedPropertyMetaProvider"/>
    /// </summary>
    public class DefaultSupportedPropertyMetaProvider : ISupportedPropertyMetaProvider
    {
        private const string DefaultDescription = "The {0} property";
        private readonly CamelCasePropertyNamesContractResolver _propertyNames = new CamelCasePropertyNamesContractResolver();

        /// <summary>
        /// Gets the <see cref="SupportedPropertyMeta"/> based on property features
        /// and common attributes.
        /// </summary>
        public virtual SupportedPropertyMeta GetMeta(PropertyInfo property)
        {
            var isReadonly = property.SetMethod == null || property.SetMethod.IsPrivate || HasReadonlyAttribute(property);
            var title = _propertyNames.GetResolvedPropertyName(property.Name);
            var description = property.GetDescription() ?? string.Format(DefaultDescription, title);
            var isWriteOnly = property.GetMethod == null ||
                              property.GetMethod.IsPrivate ||
                              property.GetCustomAttribute<WriteOnlyAttribute>() != null;
            var isRequired = property.GetCustomAttribute<RequiredAttribute>() != null;

            return new SupportedPropertyMeta
            {
                Title = title,
                Description = description,
                Writeable = isReadonly == false,
                Readable = isWriteOnly == false,
                Required = isRequired
            };
        }

        private static bool HasReadonlyAttribute(PropertyInfo property)
        {
            var readOnlyAttribute = property.GetCustomAttribute<ReadOnlyAttribute>();

            return readOnlyAttribute != null && readOnlyAttribute.IsReadOnly;
        }
    }
}
