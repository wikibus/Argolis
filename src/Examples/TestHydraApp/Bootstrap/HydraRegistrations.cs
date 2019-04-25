using Argolis.Hydra;
using Argolis.Hydra.Discovery.SupportedOperations;
using Argolis.Models;
using Nancy;
using Nancy.Bootstrapper;
using TestHydraApp.Hydra;

namespace TestHydraApp.Bootstrap
{
    public class HydraRegistrations : Registrations
    {
        public HydraRegistrations(ITypeCatalog catalog)
            : base(catalog)
        {
            this.Register<IHydraDocumentationSettings>(new HydraDocumentationSettings());

            // todo: move to Lernaean.Hydra.Nancy when bug NancyFx/Nancy#2384 is fixed
            this.RegisterAll<ISupportedOperations>(Lifetime.PerRequest);
            this.Register<IBaseUriProvider>(new BaseProvider());
        }
    }
}