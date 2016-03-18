using System.ComponentModel;
using System.Reflection;

namespace Hydra.Discovery
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
    }
}