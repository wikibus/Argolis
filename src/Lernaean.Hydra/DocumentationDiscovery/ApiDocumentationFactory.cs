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
        private readonly ISupportedPropertyFactory _propFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDocumentationFactory"/> class.
        /// </summary>
        public ApiDocumentationFactory(
            IHydraDocumentationSettings settings,
            IRdfTypeProviderPolicy rdfClassProvider,
            ISupportedPropertyFactory propFactory)
        {
            _settings = settings;
            _rdfClassProvider = rdfClassProvider;
            _propFactory = propFactory;
        }

        /// <summary>
        /// Creates the API documentation.
        /// </summary>
        public ApiDocumentation Create()
        {
            var apiDocumentation = new ApiDocumentation(_settings.EntryPoint);

            var classes = from type in _settings.Sources.SelectMany(source => source.FindTypes()).Distinct()
                          let classId = _rdfClassProvider.Create(type)
                          let supportedProperties = type.GetProperties().Select(_propFactory.Create)
                          select new Class(classId.ToString())
                          {
                              SupportedProperties = supportedProperties
                          };

            apiDocumentation.SupportedClasses = classes.ToList();

            return apiDocumentation;
        }
    }
}