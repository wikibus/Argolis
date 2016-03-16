using System.Collections.Generic;
using Hydra.DocumentationDiscovery;
using Hydra.Nancy;
using TestHydraApi;

namespace TestNancyApp
{
    public class HydraDocumentationSettings : IHydraDocumentationSettings
    {
        public string DocumentationPath
        {
            get { return "api"; }
        }

        public IEnumerable<ISupportedClassSource> Sources
        {
            get
            {
                yield return new AssemblySupportedClassSource(typeof(Issue).Assembly);
            }
        }
    }
}