using Argolis.Hydra.Discovery.SupportedOperations;
using Argolis.Hydra.Nancy;
using TestHydraApi;

namespace TestHydraApp.Hydra
{
    public class IssueSupportedOperations : SupportedOperations<Issue>
    {
        public IssueSupportedOperations(NancyContextWrapper context)
        {
            this.Class.SupportsGet();

            if (context.Current.CurrentUser?.IsInRole("Admin") == true)
            {
                this.Class.SupportsDelete();
            }

            this.Property(issue => issue.ProjectId).SupportsGet();
        }
    }
}