using Hydra;
using Nancy.Bootstrapper;
using TestNancyApp.Hydra;

namespace TestNancyApp.Bootstrap
{
    public class HydraRegistrations : Registrations
    {
        public HydraRegistrations()
        {
            Register<IHydraDocumentationSettings>(new HydraDocumentationSettings());
        }
    }
}