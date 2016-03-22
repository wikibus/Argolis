using Hydra.Discovery.SupportedOperations;
using TestHydraApi;

namespace TestNancyApp
{
    public class IssueSupportedOperations : SupportedOperations<Issue>
    {
        public IssueSupportedOperations()
        {
            CanGet();

            Property(issue => issue.ProjectId).CanGet();
        }
    }
}