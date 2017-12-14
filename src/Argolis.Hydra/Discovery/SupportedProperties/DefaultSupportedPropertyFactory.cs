using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Argolis.Hydra.Core;
using Argolis.Hydra.Discovery.SupportedClasses;
using Argolis.Hydra.Discovery.SupportedOperations;
using JsonLD.Entities;

namespace Argolis.Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedPropertyFactory"/>
    /// </summary>
    public class DefaultSupportedPropertyFactory : ISupportedPropertyFactory
    {
        private readonly IPropertyRangeRetrievalPolicy rangeRetrieval;
        private readonly ISupportedPropertyMetaProvider metaProvider;
        private readonly IPropertyPredicateIdPolicy propertyPredicateIdPolicy;
        private readonly ISupportedOperationFactory operationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSupportedPropertyFactory"/> class.
        /// </summary>
        public DefaultSupportedPropertyFactory(
            IPropertyRangeRetrievalPolicy rangeRetrieval,
            ISupportedPropertyMetaProvider metaProvider,
            IPropertyPredicateIdPolicy propertyPredicateIdPolicy,
            ISupportedOperationFactory operationFactory)
        {
            this.rangeRetrieval = rangeRetrieval;
            this.metaProvider = metaProvider;
            this.propertyPredicateIdPolicy = propertyPredicateIdPolicy;
            this.operationFactory = operationFactory;
        }

        /// <summary>
        /// Creates a hydra <see cref="SupportedProperty" /> from a type's property
        /// using sensible defaults.
        /// </summary>
        public virtual SupportedProperty Create(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds)
        {
            IriRef? mappedType = this.rangeRetrieval.GetRange(prop, classIds);
            var meta = this.metaProvider.GetMeta(prop);
            string propertyId = this.propertyPredicateIdPolicy.GetPropertyId(prop);

            if (propertyId == null)
            {
                throw new ApiDocumentationException(string.Format("Property {0} is not included in the context", prop));
            }

            var propertyTypes = new List<string>();
            if (meta.IsLink)
            {
                propertyTypes.Add(Vocab.Hydra.Link);
            }

            var property = new SupportedProperty
            {
                Title = meta.Title,
                Description = meta.Description,
                Writeable = meta.Writeable,
                Readable = meta.Readable,
                Required = meta.Required,
                Property = new Property(propertyTypes)
                {
                    Id = propertyId,
                    Range = mappedType ?? (IriRef)Vocab.Hydra.Resource,
                    SupportedOperations = this.operationFactory.CreateOperations(prop, classIds).ToList()
                }
            };

            return property;
        }
    }
}
