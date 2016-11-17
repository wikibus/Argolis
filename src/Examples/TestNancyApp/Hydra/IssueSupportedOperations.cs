using Hydra.Discovery.SupportedOperations;
using TestHydraApi;
using TestNancyApp.Bootstrap;

namespace TestNancyApp.Hydra
{
    public class IssueSupportedOperations : SupportedOperations<Issue>
    {
        public IssueSupportedOperations(NancyContextWrapper current)
        {
            Class.SupportsGet();

            if (current.Context.CurrentUser?.IsInRole("Admin") == true)
            {
                Class.SupportsDelete();
            }

            Property(issue => issue.ProjectId).SupportsGet();
        }
    }
}