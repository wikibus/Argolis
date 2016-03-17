using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hydra.Core;
using JsonLD.Entities;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedPropertyFactory"/>
    /// </summary>
    public class DefaultSupportedPropertyFactory : ISupportedPropertyFactory
    {
        private readonly IEnumerable<IPropertyTypeMapping> _propertyTypeMappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSupportedPropertyFactory"/> class.
        /// </summary>
        /// <param name="propertyTypeMappings">The property type mappings.</param>
        public DefaultSupportedPropertyFactory(IEnumerable<IPropertyTypeMapping> propertyTypeMappings)
        {
            _propertyTypeMappings = propertyTypeMappings;
        }

        /// <summary>
        /// Creates a hydra <see cref="SupportedProperty" /> from a type's property
        /// using sensible defaults.
        /// </summary>
        public SupportedProperty Create(PropertyInfo prop)
        {
            Uri mappedType = _propertyTypeMappings.Select(mapping => mapping.MapType(prop)).FirstOrDefault(mapType => mapType != null);

            var property = new SupportedProperty();
            if (mappedType != null)
            {
                property.Predicate = mappedType;
            }

            return property;
        }
    }
}