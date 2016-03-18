using System.Linq;
using Hydra.Core;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Discovers Types and converts them into Supported Classes to
    /// be included in the <see cref="ApiDocumentation"/>
    /// </summary>
    public class ApiDocumentationFactory
    {
        private readonly IHydraDocumentationSettings _settings;
        private readonly IRdfTypeProviderPolicy _rdfClassProvider;
        private readonly ISupportedPropertySelectionPolicy _propSelector;
        private readonly ISupportedPropertyFactory _propFactory;
        private readonly ISupportedClassMetaProvider _classMetaProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDocumentationFactory"/> class.
        /// </summary>
        public ApiDocumentationFactory(
            IHydraDocumentationSettings settings,
            IRdfTypeProviderPolicy rdfClassProvider,
            ISupportedPropertySelectionPolicy propSelector,
            ISupportedPropertyFactory propFactory,
            ISupportedClassMetaProvider classMetaProvider)
        {
            _settings = settings;
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

            var classes = from type in _settings.Sources.SelectMany(source => source.FindTypes()).Distinct()
                          let classId = _rdfClassProvider.Create(type)
                          let supportedProperties = type.GetProperties().Where(_propSelector.ShouldIncludeProperty).Select(_propFactory.Create)
                          let classMeta = _classMetaProvider.GetMeta(type)
                          select new Class(classId.ToString())
                          {
                              SupportedProperties = supportedProperties,
                              Description = classMeta.Description
                          };

            apiDocumentation.SupportedClasses = classes.ToList();

            return apiDocumentation;
        }
    }
}