using System;
using System.Collections.Generic;
using System.Linq;
using Hydra.Core;
using Hydra.Discovery.SupportedClasses;
using Hydra.Discovery.SupportedProperties;

namespace Hydra.Discovery
{
    /// <summary>
    /// Discovers Types and converts them into Supported Classes to
    /// be included in the <see cref="ApiDocumentation"/>
    /// </summary>
    public class ApiDocumentationFactory
    {
        private readonly IHydraDocumentationSettings _settings;
        private readonly IEnumerable<IDocumentedTypeSelector> _sources;
        private readonly IRdfTypeProviderPolicy _rdfClassProvider;
        private readonly ISupportedPropertySelectionPolicy _propSelector;
        private readonly ISupportedPropertyFactory _propFactory;
        private readonly ISupportedClassMetaProvider _classMetaProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDocumentationFactory"/> class.
        /// </summary>
        public ApiDocumentationFactory(
            IHydraDocumentationSettings settings,
            IEnumerable<IDocumentedTypeSelector> sources,
            IRdfTypeProviderPolicy rdfClassProvider,
            ISupportedPropertySelectionPolicy propSelector,
            ISupportedPropertyFactory propFactory,
            ISupportedClassMetaProvider classMetaProvider)
        {
            _settings = settings;
            _sources = sources;
            _rdfClassProvider = rdfClassProvider;
            _propSelector = propSelector;
            _propFactory = propFactory;
            _classMetaProvider = classMetaProvider;
        }

        /// <summary>
        /// Creates the API documentation.
        /// </summary>
        public ApiDocumentation Create()
        {
            var apiDocumentation = new ApiDocumentation(_settings.EntryPoint);

            var classes = DiscoverSupportedClasses();
            var classIds = classes.ToDictionary(c => c.Key, c => c.Value.Id);

            foreach (var supportedClass in classes)
            {
                var supportedProperties =
                    supportedClass.Key.GetProperties()
                        .Where(_propSelector.ShouldIncludeProperty)
                        .Select(sp => _propFactory.Create(sp, classIds));

                supportedClass.Value.SupportedProperties = supportedProperties;
            }

            apiDocumentation.SupportedClasses = classes.Values;

            return apiDocumentation;
        }

        private Dictionary<Type, Class> DiscoverSupportedClasses()
        {
            return (from type in _sources.SelectMany(source => source.FindTypes()).Distinct()
                let classId = _rdfClassProvider.Create(type)
                let classMeta = _classMetaProvider.GetMeta(type)
                select new
                {
                    type,
                    @class = new Class(classId)
                    {
                        Title = classMeta.Title,
                        Description = classMeta.Description
                    }
                })
                .ToDictionary(t => t.type, t => t.@class);
        }
    }
}
