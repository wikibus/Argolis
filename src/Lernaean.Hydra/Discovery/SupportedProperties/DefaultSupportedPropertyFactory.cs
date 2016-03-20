using System;
using System.Collections.Generic;
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
        private readonly IPropertyRangeRetrievalPolicy _rangeRetrieval;
        private readonly ISupportedPropertyMetaProvider _metaProvider;
        private readonly IPropertyPredicateIdPolicy _propertyPredicateIdPolicy;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSupportedPropertyFactory"/> class.
        /// </summary>
        public DefaultSupportedPropertyFactory(
            IPropertyRangeRetrievalPolicy rangeRetrieval,
            ISupportedPropertyMetaProvider metaProvider,
            IPropertyPredicateIdPolicy propertyPredicateIdPolicy)
        {
            _rangeRetrieval = rangeRetrieval;
            _metaProvider = metaProvider;
            _propertyPredicateIdPolicy = propertyPredicateIdPolicy;
        }

        /// <summary>
        /// Creates a hydra <see cref="SupportedProperty" /> from a type's property
        /// using sensible defaults.
        /// </summary>
        public virtual SupportedProperty Create(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds)
        {
            IriRef? mappedType = _rangeRetrieval.GetRange(prop, classIds);
            var meta = _metaProvider.GetMeta(prop);
            string propertyId = _propertyPredicateIdPolicy.GetPropertyId(prop, classIds[prop.ReflectedType]);

            var property = new SupportedProperty
            {
                Title = meta.Title,
                Description = meta.Description,
                Writeable = meta.Writeable,
                Readable = meta.Readable,
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
