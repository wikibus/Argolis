using Hydra;
using Nancy.Bootstrapper;

namespace TestNancyApp
{
    public class TestAppRegistrations : Registrations
    {
        public TestAppRegistrations()
        {
            Register<IHydraDocumentationSettings>(new HydraDocumentationSettings());
        }
    }
}