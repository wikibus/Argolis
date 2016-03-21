using System.Collections.Generic;
using System.Linq;
using Hydra.Core;
using Hydra.Discovery.SupportedClasses;

namespace Hydra.Discovery
{
    /// <summary>
    /// Discovers Types and converts them into Supported Classes to
    /// be included in the <see cref="ApiDocumentation"/>
    /// </summary>
    public class ApiDocumentationFactory : IApiDocumentationFactory
    {
        private readonly IHydraDocumentationSettings _settings;
        private readonly IEnumerable<IDocumentedTypeSelector> _sources;
        private readonly IRdfTypeProviderPolicy _rdfClassProvider;
        private readonly ISupportedClassFactory _classFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDocumentationFactory"/> class.
        /// </summary>
        public ApiDocumentationFactory(
            IHydraDocumentationSettings settings,
            IEnumerable<IDocumentedTypeSelector> sources,
            IRdfTypeProviderPolicy rdfClassProvider,
            ISupportedClassFactory classFactory)
        {
            _settings = settings;
            _sources = sources;
            _rdfClassProvider = rdfClassProvider;
            _classFactory = classFactory;
        }

        /// <summary>
        /// Creates the API documentation.
        /// </summary>
        public ApiDocumentation Create()
        {
            var apiDocumentation = new ApiDocumentation(_settings.EntryPoint);

            var types = (from type in _sources.SelectMany(source => source.FindTypes()).Distinct()
                         let classId = _rdfClassProvider.Create(type)
                         select type)
                        .ToDictionary(t => t, type => _rdfClassProvider.Create(type));

            var classes = types.Select(type => _classFactory.Create(type.Key, types));

            apiDocumentation.SupportedClasses = classes.ToList();

            return apiDocumentation;
        }
    }
}
