using Argolis.Hydra;
using Argolis.Hydra.Discovery.SupportedOperations;
using Argolis.Models;
using Nancy;
using Nancy.Bootstrapper;
using TestNancyApp.Hydra;
using TestNancyApp.Modules;

namespace TestNancyApp.Bootstrap
{
    public class HydraRegistrations : Registrations
    {
        public HydraRegistrations(ITypeCatalog catalog)
            : base(catalog)
        {
            Register<IHydraDocumentationSettings>(new HydraDocumentationSettings());

            // todo: move to Lernaean.Hydra.Nancy when bug NancyFx/Nancy#2384 is fixed
            RegisterAll<ISupportedOperations>(Lifetime.PerRequest);
            Register<IBaseUriProvider>(new BaseProvider());
        }
    }
}