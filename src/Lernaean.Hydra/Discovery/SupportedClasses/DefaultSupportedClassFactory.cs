using System;
using System.Collections.Generic;
using System.Linq;
using Hydra.Core;
using Hydra.Discovery.SupportedOperations;
using Hydra.Discovery.SupportedProperties;

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
        private readonly ISupportedOperationFactory _operationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSupportedClassFactory"/> class.
        /// </summary>
        public DefaultSupportedClassFactory(
            ISupportedPropertySelectionPolicy propSelector,
            ISupportedPropertyFactory propFactory,
            ISupportedClassMetaProvider classMetaProvider,
            ISupportedOperationFactory operationFactory)
        {
            _propSelector = propSelector;
            _propFactory = propFactory;
            _classMetaProvider = classMetaProvider;
            _operationFactory = operationFactory;
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

            supportedClass.SupportedOperations = _operationFactory.CreateOperations(supportedClassType, classIds).ToList();
            supportedClass.SupportedProperties = supportedProperties.ToList();

            return supportedClass;
        }
    }
}
