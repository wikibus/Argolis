﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Argolis.Hydra.Core;
using Argolis.Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using Vocab;

namespace Argolis.Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedOperationFactory"/>
    /// </summary>
    public class DefaultSupportedOperationFactory : ISupportedOperationFactory
    {
        private readonly IEnumerable<ISupportedOperations> operations;
        private readonly IPropertyRangeRetrievalPolicy rangeRetrieval;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSupportedOperationFactory"/> class.
        /// </summary>
        public DefaultSupportedOperationFactory(
            IEnumerable<ISupportedOperations> operations,
            IPropertyRangeRetrievalPolicy rangeRetrieval)
        {
            this.operations = operations;
            this.rangeRetrieval = rangeRetrieval;
        }

        /// <summary>
        /// Creates the supported operations for supported class.
        /// </summary>
        public IEnumerable<Operation> CreateOperations(Type supportedClassType, IReadOnlyDictionary<Type, Uri> classIds)
        {
            return from source in this.operations
                   where source.Type == supportedClassType
                   from opMeta in source.GetSupportedClassOperations()
                   select this.CreateOperation(opMeta, (IriRef)classIds[supportedClassType]);
        }

        /// <summary>
        /// Creates the supported operations for supported property.
        /// </summary>
        public IEnumerable<Operation> CreateOperations(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds)
        {
            IriRef mappedType = this.rangeRetrieval.GetRange(prop, classIds) ?? (IriRef)Vocab.Hydra.Resource;

            return from operation in this.operations
                   where operation.Type == prop.ReflectedType
                   from meta in operation.GetSupportedPropertyOperations(prop)
                   select this.CreateOperation(meta, mappedType);
        }

        /// <summary>
        /// Creates the operation replacing meta values with defaults.
        /// </summary>
        /// <example>
        /// GET always expects owl:Nothing, PUT always returns owl:Nothing, etc.
        /// </example>
        protected virtual Operation CreateOperation(OperationMeta meta, IriRef modelOrPropertyType)
        {
            var expects = meta.Expects;
            var returns = meta.Returns;

            switch (meta.Method)
            {
                case HttpMethod.Delete:
                    returns = meta.Returns ?? (IriRef)Owl.Nothing;
                    goto case HttpMethod.Head;
                case HttpMethod.Get:
                    returns = modelOrPropertyType;
                    goto case HttpMethod.Head;
                case HttpMethod.Head:
                case HttpMethod.Trace:
                    expects = (IriRef)Owl.Nothing;
                    break;
                case HttpMethod.Put:
                    expects = modelOrPropertyType;
                    returns = (IriRef?)Owl.Nothing;
                    break;
            }

            return new Operation(meta.Method)
            {
                Title = meta.Title,
                Description = meta.Description,
                Returns = returns.GetValueOrDefault(modelOrPropertyType),
                Expects = expects.GetValueOrDefault((IriRef)Owl.Nothing),
                Types = meta.Types.Select(iriRef => iriRef.Value)
            };
        }
    }
}
