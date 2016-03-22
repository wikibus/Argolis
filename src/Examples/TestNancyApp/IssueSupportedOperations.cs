using Hydra.Discovery.SupportedOperations;
using TestHydraApi;

namespace TestNancyApp
{
    public class IssueSupportedOperations : SupportedOperations<Issue>
    {
        public IssueSupportedOperations()
        {
            SupportsGet();

            Property(issue => issue.ProjectId).SupportsGet();
        }
    }
}