using Argolis.Hydra.Discovery.SupportedOperations;
using TestHydraApi;
using TestHydraApp.Bootstrap;

namespace TestHydraApp.Hydra
{
    public class IssueSupportedOperations : SupportedOperations<Issue>
    {
        public IssueSupportedOperations(NancyContextWrapper current)
        {
            this.Class.SupportsGet();

            if (current.Context.CurrentUser?.IsInRole("Admin") == true)
            {
                this.Class.SupportsDelete();
            }

            this.Property(issue => issue.ProjectId).SupportsGet();
        }
    }
}