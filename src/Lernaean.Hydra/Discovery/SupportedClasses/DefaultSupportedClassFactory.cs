using System;
using System.Collections.Generic;
using System.Linq;
using Hydra.Core;
using Hydra.Discovery.SupportedOperations;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;

namespace Hydra.Discovery.SupportedClasses
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedClassFactory"/>
    /// </summary>
    public class DefaultSupportedClassFactory : ISupportedClassFactory
    {
        private readonly ISupportedPropertySelectionPolicy _propSelector;
        private readonly ISupportedPropertyFactory _propFactory;
        private readonly ISupportedClassMetaProvider _classMetaProvider;
        private readonly IEnumerable<ISupportedOperations> _operations;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSupportedClassFactory"/> class.
        /// </summary>
        public DefaultSupportedClassFactory(
            ISupportedPropertySelectionPolicy propSelector,
            ISupportedPropertyFactory propFactory,
            ISupportedClassMetaProvider classMetaProvider,
            IEnumerable<ISupportedOperations> operations)
        {
            _propSelector = propSelector;
            _propFactory = propFactory;
            _classMetaProvider = classMetaProvider;
            _operations = operations;
        }

        /// <summary>
        /// Creates a supported class with supported properties and operations
        /// </summary>
        public Class Create(Type supportedClassType, IReadOnlyDictionary<Type, Uri> classIds)
        {
            var supportedClassId = classIds[supportedClassType];
            var classMeta = _classMetaProvider.GetMeta(supportedClassType);
            var supportedClass = new Class(supportedClassId)
            {
                Title = classMeta.Title,
                Description = classMeta.Description
            };

            var supportedProperties =
                supportedClassType.GetProperties()
                    .Where(_propSelector.ShouldIncludeProperty)
                    .Select(sp => _propFactory.Create(sp, classIds));

             var operations = from source in _operations
                              where source.Type == supportedClassType
                              from op in source.GetTypeOperations()
                              select new Operation(op.Method)
                              {
                                  Returns = (IriRef)supportedClass.Id
                              };
            supportedClass.SupportedOperations = operations.ToList();

            supportedClass.SupportedProperties = supportedProperties.ToList();

            return supportedClass;
        }
    }
}
