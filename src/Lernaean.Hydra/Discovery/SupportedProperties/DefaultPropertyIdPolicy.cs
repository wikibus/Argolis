using System;
using System.Reflection;
using JsonLD.Entities;
using Newtonsoft.Json;

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

        private readonly ContextResolver _contextResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPropertyIdPolicy"/> class.
        /// </summary>
        public DefaultPropertyIdPolicy(IContextProvider contextProvider)
        {
            _contextResolver = new ContextResolver(contextProvider);
        }

        /// <summary>
        /// Gets the property identifier from the @context
        /// or as concatenation of class and property name.
        /// </summary>
        public string GetPropertyId(PropertyInfo property, Uri classId)
        {
            var context = _contextResolver.GetContext(property.ReflectedType);

            if (context != null)
            {
                var mappedTerm = ContextHelpers.GetExpandedIri(context, property.GetJsonPropertyName());

                if (mappedTerm != null)
                {
                    return mappedTerm;
                }
            }

            return GetFallbackPropertyId(property, property.GetJsonPropertyName(), classId);
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
