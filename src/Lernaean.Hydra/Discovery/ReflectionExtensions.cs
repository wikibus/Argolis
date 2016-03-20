using System.ComponentModel;
using System.Reflection;
using JsonLD.Entities;
using Newtonsoft.Json;

namespace Hydra.Discovery
{
    /// <summary>
    /// Helpers for reading stuff from reflection
    /// </summary>
    internal static class ReflectionExtensions
    {
        private static readonly JsonLdContractResolver ContractResolver = new JsonLdContractResolver();

        /// <summary>
        /// Gets the description of a reflected element.
        /// </summary>
        internal static string GetDescription(this MemberInfo property)
        {
            var readOnlyAttribute = property.GetCustomAttribute<DescriptionAttribute>();

            return readOnlyAttribute != null ? readOnlyAttribute.Description : null;
        }

        /// <summary>
        /// Gets the name of the JSON key the <paramref name="property"/>
        /// will be serialized to.
        /// </summary>
        internal static string GetJsonPropertyName(this PropertyInfo property)
        {
            var jsonProperty = property.GetCustomAttribute<JsonPropertyAttribute>();
            if (jsonProperty != null && jsonProperty.PropertyName != null)
            {
                return jsonProperty.PropertyName;
            }

            return ContractResolver.GetResolvedPropertyName(property.Name);
        }
    }
}
