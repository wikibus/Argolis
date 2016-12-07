using System;
using System.Collections.Generic;
using System.Linq;
using Argolis.Hydra.Core;
using Argolis.Hydra.Discovery.SupportedOperations;
using Argolis.Hydra.Discovery.SupportedProperties;

namespace Argolis.Hydra.Discovery.SupportedClasses
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedClassFactory"/>
    /// </summary>
    public class DefaultSupportedClassFactory : ISupportedClassFactory
    {
        private readonly ISupportedPropertySelectionPolicy propSelector;
        private readonly ISupportedPropertyFactory propFactory;
        private readonly ISupportedClassMetaProvider classMetaProvider;
        private readonly ISupportedOperationFactory operationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSupportedClassFactory"/> class.
        /// </summary>
        public DefaultSupportedClassFactory(
            ISupportedPropertySelectionPolicy propSelector,
            ISupportedPropertyFactory propFactory,
            ISupportedClassMetaProvider classMetaProvider,
            ISupportedOperationFactory operationFactory)
        {
            this.propSelector = propSelector;
            this.propFactory = propFactory;
            this.classMetaProvider = classMetaProvider;
            this.operationFactory = operationFactory;
        }

        /// <summary>
        /// Creates a supported class with supported properties and operations
        /// </summary>
        public Class Create(Type supportedClassType, IReadOnlyDictionary<Type, Uri> classIds)
        {
            var supportedClassId = classIds[supportedClassType];
            var classMeta = this.classMetaProvider.GetMeta(supportedClassType);
            var supportedClass = new Class(supportedClassId)
            {
                Title = classMeta.Title,
                Description = classMeta.Description
            };

            var supportedProperties =
                supportedClassType.GetProperties()
                    .Where(this.propSelector.ShouldIncludeProperty)
                    .Select(sp => this.propFactory.Create(sp, classIds));

            supportedClass.SupportedOperations = this.operationFactory.CreateOperations(supportedClassType, classIds).ToList();
            supportedClass.SupportedProperties = supportedProperties.ToList();

            return supportedClass;
        }
    }
}
