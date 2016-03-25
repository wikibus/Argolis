using System;
using System.Linq;
using System.Linq.Expressions;
using Hydra.Discovery;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using JsonLD.Entities.Context;
using Newtonsoft.Json.Linq;

namespace Hydra.Serialization
{
    /// <summary>
    /// Automatic @context based on properties and class identifier
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    /// <seealso cref="Newtonsoft.Json.Linq.JObject" />
    public class AutoContext<T> : JObject
    {
        private const string SlashClassIdAppendFormat = "{0}#{1}";
        private const string HashClassIdAppendFormat = "{0}/{1}";

        private static readonly JObject EmptyContext = new JObject();
        private readonly ISupportedPropertySelectionPolicy _selectionPolicy = new AllPublicPropertiesSelectionPolicy();

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoContext{T}"/> class.
        /// </summary>
        public AutoContext(Uri classId) : this(EmptyContext, classId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoContext{T}"/> class
        /// by extending definitions from <paramref name="context"/>
        /// </summary>
        public AutoContext(JObject context, Uri classId) : base(context)
        {
            foreach (var property in typeof(T).GetProperties().Where(_selectionPolicy.ShouldIncludeProperty))
            {
                var contextKey = property.GetJsonPropertyName();
                if (this[contextKey] == null)
                {
                    Add(contextKey, GetPropertyId(contextKey, classId));
                }
            }
        }

        /// <summary>
        /// Allows setting up additional context mapping properties for
        /// a mapping such as type coercion of language
        /// </summary>
        /// <typeparam name="TProp">The return type of property.</typeparam>
        /// <exception cref="System.ArgumentException">
        /// if selected property is not found in the @context
        /// </exception>
        public AutoContext<T> Property<TProp>(Expression<Func<T, TProp>> propertyExpression, Func<PropertyBuilder, JProperty> setupMapping)
        {
            var property = propertyExpression.GetProperty();
            var contextKey = property.GetJsonPropertyName();

            if (this[contextKey] == null)
            {
                var message = string.Format("Cannot find property '{0}' in @context", contextKey);
                throw new ArgumentException(message, nameof(propertyExpression));
            }

            var currentMappedPredicate = GetPropertyKey(this[contextKey]);
            if (currentMappedPredicate == null)
            {
                var message = string.Format("Invalid value in @context mapped to property {0}", contextKey);
                throw new InvalidOperationException(message);
            }

            var newMapping = setupMapping(contextKey.IsProperty(currentMappedPredicate));
            this[contextKey] = newMapping.Value;

            return this;
        }

        /// <summary>
        /// Gets the fallback property identifier.
        /// </summary>
        protected virtual string GetPropertyId(string propertyName, Uri classId)
        {
            var format = HashClassIdAppendFormat;

            if (string.IsNullOrWhiteSpace(classId.Fragment))
            {
                format = SlashClassIdAppendFormat;
            }

            return string.Format(format, classId, propertyName);
        }

        private static string GetPropertyKey(JToken jToken)
        {
            if (jToken is JObject)
            {
                return ((JObject)jToken)[JsonLdKeywords.Id].ToString();
            }

            if (jToken is JValue)
            {
                return jToken.ToString();
            }

            return null;
        }
    }
}
