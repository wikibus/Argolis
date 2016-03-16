using System.Collections.Generic;
using Hydra.DocumentationDiscovery;
using Hydra.Nancy;
using JsonLD.Entities;
using TestHydraApi;

namespace TestNancyApp
{
    public class HydraDocumentationSettings : IHydraDocumentationSettings
    {
        public string DocumentationPath
        {
            get { return "api"; }
        }

        public IEnumerable<IDocumentedTypeSelector> Sources
        {
            get
            {
                yield return new AssemblyDocumentedTypeSelector(typeof(Issue).Assembly);
            }
        }

        public IriRef EntryPoint
        {
            get { return (IriRef)"http://localhost:61186/entrypoint"; }
        }
    }
}