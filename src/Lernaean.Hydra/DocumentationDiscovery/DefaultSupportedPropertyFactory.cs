using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hydra.Core;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedPropertyFactory"/>
    /// </summary>
    public class DefaultSupportedPropertyFactory : ISupportedPropertyFactory
    {
        private readonly IEnumerable<IPropertyTypeMapping> _propertyTypeMappings;
        private readonly ISupportedPropertyMetaProvider _metaProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSupportedPropertyFactory"/> class.
        /// </summary>
        public DefaultSupportedPropertyFactory(
            IEnumerable<IPropertyTypeMapping> propertyTypeMappings,
            ISupportedPropertyMetaProvider metaProvider)
        {
            _propertyTypeMappings = propertyTypeMappings;
            _metaProvider = metaProvider;
        }

        /// <summary>
        /// Creates a hydra <see cref="SupportedProperty" /> from a type's property
        /// using sensible defaults.
        /// </summary>
        public SupportedProperty Create(PropertyInfo prop)
        {
            Uri mappedType = _propertyTypeMappings.Select(mapping => mapping.MapType(prop)).FirstOrDefault(mapType => mapType != null);
            var meta = _metaProvider.GetMeta(prop);

            var property = new SupportedProperty
            {
                Label = meta.Title,
                Description = meta.Description
            };

            if (mappedType != null)
            {
                property.Predicate = mappedType;
            }

            return property;
        }
    }
}