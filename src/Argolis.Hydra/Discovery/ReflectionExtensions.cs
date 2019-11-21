using System.ComponentModel;
using System.Reflection;
using Argolis.Hydra.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;

namespace Argolis.Hydra.Discovery
{
    /// <summary>
    /// Helpers for reading stuff from reflection
    /// </summary>
    internal static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the description of a reflected element.
        /// </summary>
        internal static string GetDescription(this MemberInfo property)
        {
            var readOnlyAttribute = property.GetCustomAttribute<DescriptionAttribute>();

            return readOnlyAttribute != null ? readOnlyAttribute.Description : null;
        }

        /// <summary>
        /// Gets the property URI annotated by <see cref="JsonPropertyAttribute"/> or <see cref="PropertyAttribute"/>
        /// </summary>
        internal static string GetProperty(this PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<PropertyAttribute>();

            return attribute != null ? attribute.Id : property.GetJsonPropertyName();
        }
    }
}
