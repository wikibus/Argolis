using System.Collections.Generic;
using System.Linq;
using Argolis.Hydra.Core;
using Argolis.Hydra.Discovery.SupportedClasses;

namespace Argolis.Hydra.Discovery
{
    /// <summary>
    /// Discovers Types and converts them into Supported Classes to
    /// be included in the <see cref="ApiDocumentation"/>
    /// </summary>
    public class ApiDocumentationFactory : IApiDocumentationFactory
    {
        private readonly IHydraDocumentationSettings settings;
        private readonly IEnumerable<IDocumentedTypeSelector> sources;
        private readonly IRdfTypeProviderPolicy rdfClassProvider;
        private readonly ISupportedClassFactory classFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDocumentationFactory"/> class.
        /// </summary>
        public ApiDocumentationFactory(
            IHydraDocumentationSettings settings,
            IEnumerable<IDocumentedTypeSelector> sources,
            IRdfTypeProviderPolicy rdfClassProvider,
            ISupportedClassFactory classFactory)
        {
            this.settings = settings;
            this.sources = sources;
            this.rdfClassProvider = rdfClassProvider;
            this.classFactory = classFactory;
        }

        /// <summary>
        /// Creates the API documentation.
        /// </summary>
        public ApiDocumentation Create()
        {
            var apiDocumentation = new ApiDocumentation(this.settings.EntryPoint);

            var types = (from type in this.sources.SelectMany(source => source.FindTypes()).Distinct()
                         let classId = this.rdfClassProvider.Create(type)
                         select type)
                        .ToDictionary(t => t, type => this.rdfClassProvider.Create(type));

            var classes = types.Select(type => this.classFactory.Create(type.Key, types));

            apiDocumentation.SupportedClasses = classes.ToList();

            return apiDocumentation;
        }
    }
}
