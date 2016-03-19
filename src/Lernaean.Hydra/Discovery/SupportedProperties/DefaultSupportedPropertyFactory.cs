using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hydra.Core;
using Hydra.Discovery.SupportedClasses;
using JsonLD.Entities;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedPropertyFactory"/>
    /// </summary>
    public class DefaultSupportedPropertyFactory : ISupportedPropertyFactory
    {
        private readonly IEnumerable<IPropertyRangeMappingPolicy> _propertyTypeMappings;
        private readonly ISupportedPropertyMetaProvider _metaProvider;
        private readonly IPropertyPredicateIdPolicy _fallbackPropertyPredicateId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSupportedPropertyFactory"/> class.
        /// </summary>
        public DefaultSupportedPropertyFactory(
            IEnumerable<IPropertyRangeMappingPolicy> propertyTypeMappings,
            ISupportedPropertyMetaProvider metaProvider,
            IPropertyPredicateIdPolicy fallbackPropertyPredicateId)
        {
            _propertyTypeMappings = propertyTypeMappings;
            _metaProvider = metaProvider;
            _fallbackPropertyPredicateId = fallbackPropertyPredicateId;
        }

        /// <summary>
        /// Creates a hydra <see cref="SupportedProperty" /> from a type's property
        /// using sensible defaults.
        /// </summary>
        public virtual SupportedProperty Create(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds)
        {
            Uri mappedType = (from mapping in _propertyTypeMappings
                              let mapType = mapping.MapType(prop, classIds)
                              where mapType != null
                              select mapType).FirstOrDefault();
            var meta = _metaProvider.GetMeta(prop);
            string propertyId = _fallbackPropertyPredicateId.GetPropertyId(prop, meta.Title, classIds[prop.DeclaringType]);

            var property = new SupportedProperty
            {
                Title = meta.Title,
                Description = meta.Description,
                Property =
                {
                    Id = propertyId,
                    Range = mappedType ?? (IriRef)Hydra.Resource
                }
            };

            return property;
        }
    }
}
