using Argolis.Hydra;
using JsonLD.Entities;

namespace TestNancyApp.Hydra
{
    public class HydraDocumentationSettings : IHydraDocumentationSettings
    {
        public string DocumentationPath
        {
            get { return "api"; }
        }

        public IriRef EntryPoint
        {
            get { return (IriRef)"http://localhost:61186/entrypoint"; }
        }
    }
}