using Hydra.Discovery.SupportedOperations;
using Nancy.Security;
using TestHydraApi;
using TestNancyApp.Bootstrap;

namespace TestNancyApp.Hydra
{
    public class IssueSupportedOperations : SupportedOperations<Issue>
    {
        public IssueSupportedOperations(NancyContextWrapper current)
        {
            Class.SupportsGet();

            if (current.Context.CurrentUser.HasClaim("Admin"))
            {
                Class.SupportsDelete();
            }

            Property(issue => issue.ProjectId).SupportsGet();
        }
    }
}