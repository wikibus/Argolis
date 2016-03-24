using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hydra.Core;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using Vocab;

namespace Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedOperationFactory"/>
    /// </summary>
    public class DefaultSupportedOperationFactory : ISupportedOperationFactory
    {
        private readonly IEnumerable<ISupportedOperations> _operations;
        private readonly IPropertyRangeRetrievalPolicy _rangeRetrieval;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSupportedOperationFactory"/> class.
        /// </summary>
        public DefaultSupportedOperationFactory(
            IEnumerable<ISupportedOperations> operations,
            IPropertyRangeRetrievalPolicy rangeRetrieval)
        {
            _operations = operations;
            _rangeRetrieval = rangeRetrieval;
        }

        /// <summary>
        /// Creates the supported operations for supported class.
        /// </summary>
        public IEnumerable<Operation> CreateOperations(Type supportedClassType, IReadOnlyDictionary<Type, Uri> classIds)
        {
            return from source in _operations
                   where source.Type == supportedClassType
                   from opMeta in source.GetSupportedClassOperations()
                   select CreateOperation(opMeta, (IriRef)classIds[supportedClassType]);
        }

        /// <summary>
        /// Creates the supported operations for supported property.
        /// </summary>
        public IEnumerable<Operation> CreateOperations(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds)
        {
            IriRef mappedType = _rangeRetrieval.GetRange(prop, classIds) ?? (IriRef)Hydra.Resource;

            return from operation in _operations
                   where operation.Type == prop.ReflectedType
                   from meta in operation.GetSupportedPropertyOperations(prop)
                   select CreateOperation(meta, mappedType);
        }

        /// <summary>
        /// Creates the operation replacing meta values with defaults.
        /// </summary>
        /// <example>
        /// GET always expects owl:Nothing, PUT always returns owl:Nothing, etc.
        /// </example>
        protected virtual Operation CreateOperation(OperationMeta meta, IriRef modelOrPropertyType)
        {
            switch (meta.Method)
            {
                case HttpMethod.Delete:
                    meta.Returns = meta.Returns ?? (IriRef)Owl.Nothing;
                    goto case HttpMethod.Head;
                case HttpMethod.Get:
                    meta.Returns = modelOrPropertyType;
                    goto case HttpMethod.Head;
                case HttpMethod.Head:
                case HttpMethod.Trace:
                    meta.Expects = (IriRef)Owl.Nothing;
                    break;
                case HttpMethod.Put:
                    meta.Expects = modelOrPropertyType;
                    meta.Returns = (IriRef?)Owl.Nothing;
                    break;
            }

            return new Operation(meta.Method)
            {
                Title = meta.Title,
                Description = meta.Description,
                Returns = meta.Returns.GetValueOrDefault(modelOrPropertyType),
                Expects = meta.Expects.GetValueOrDefault((IriRef)Owl.Nothing)
            };
        }
    }
}
