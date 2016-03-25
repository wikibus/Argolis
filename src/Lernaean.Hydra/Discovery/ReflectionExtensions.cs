using System;
using System.ComponentModel;
using System.Linq.Expressions;
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

        /// <summary>
        /// Gets the property from LINQ expression.
        /// </summary>
        /// <typeparam name="T">target type</typeparam>
        /// <typeparam name="TReturn">The property return type.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="System.ArgumentException">Parameter must be a property access expression</exception>
        internal static PropertyInfo GetProperty<T, TReturn>(this Expression<Func<T, TReturn>> propertyExpression)
        {
             if (!(propertyExpression.Body is MemberExpression))
            {
                throw new ArgumentException("Parameter must be a property access expression", nameof(propertyExpression));
            }

            var memberExpression = (MemberExpression)propertyExpression.Body;
            return (PropertyInfo)memberExpression.Member;
        }
    }
}
