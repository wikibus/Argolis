using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hydra.Core;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;

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
                from op in source.GetSupportedClassOperations()
                select new Operation(op.Method)
                {
                    Returns = (IriRef)classIds[supportedClassType]
                };
        }

        /// <summary>
        /// Creates the supported operations for supported property.
        /// </summary>
        public IEnumerable<Operation> CreateOperations(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds)
        {
            IriRef? mappedType = _rangeRetrieval.GetRange(prop, classIds);

            return from operation in _operations
                where operation.Type == prop.ReflectedType
                from opMeta in operation.GetSupportedPropertyOperations(prop)
                select new Operation(opMeta.Method)
                {
                    Returns = mappedType ?? (IriRef)Hydra.Resource
                };
        }
    }
}
