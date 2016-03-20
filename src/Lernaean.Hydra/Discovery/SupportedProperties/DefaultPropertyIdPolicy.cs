using System;
using System.Reflection;
using JsonLD.Core;
using JsonLD.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Default <see cref="IPropertyPredicateIdPolicy"/> with a sensible
    /// fallback based on containing class
    /// </summary>
    public class DefaultPropertyIdPolicy : IPropertyPredicateIdPolicy
    {
        private const string SlashClassIdAppendFormat = "{0}#{1}";
        private const string HashClassIdAppendFormat = "{0}/{1}";

        private readonly JsonLdContractResolver _contractResolver;
        private readonly ContextResolver _contextResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPropertyIdPolicy"/> class.
        /// </summary>
        public DefaultPropertyIdPolicy(IContextProvider contextProvider)
        {
            _contractResolver = new JsonLdContractResolver();
            _contextResolver = new ContextResolver(contextProvider);
        }

        /// <summary>
        /// Gets the property identifier from the @context
        /// or as concatenation of class and property name.
        /// </summary>
        public string GetPropertyId(PropertyInfo property, Uri classId)
        {
            var jsonProperty = property.GetCustomAttribute<JsonPropertyAttribute>();
            var context = _contextResolver.GetContext(property.ReflectedType);

            if (context != null)
            {
                string mappedTerm;
                if (jsonProperty != null && jsonProperty.PropertyName != null)
                {
                    mappedTerm = ContextHelpers.GetExpandedIri(context, jsonProperty.PropertyName);
                }
                else
                {
                    mappedTerm = ContextHelpers.GetExpandedIri(context, _contractResolver.GetResolvedPropertyName(property.Name));
                }

                if (mappedTerm != null)
                {
                    return mappedTerm;
                }
            }

            string propertyName;
            if (jsonProperty != null && jsonProperty.PropertyName != null)
            {
                propertyName = jsonProperty.PropertyName;
            }
            else
            {
                propertyName = _contractResolver.GetResolvedPropertyName(property.Name);
            }

            return GetFallbackPropertyId(property, propertyName, classId);
        }

        /// <summary>
        /// Gets the fallback property identifier if <see cref="JsonPropertyAttribute"/>
        /// wasn't present on <paramref name="property"/>.
        /// </summary>
        protected virtual string GetFallbackPropertyId(PropertyInfo property, string propertyName, Uri classId)
        {
            var format = HashClassIdAppendFormat;

            if (string.IsNullOrWhiteSpace(classId.Fragment))
            {
                format = SlashClassIdAppendFormat;
            }

            return string.Format(format, classId, propertyName);
        }
    }
}
