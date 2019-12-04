using Argolis.Hydra.Discovery.SupportedOperations;
using Argolis.Hydra.Nancy;
using JsonLD.Entities;
using TestHydraApi;
using Vocab;

namespace TestHydraApp.Hydra
{
    public class IssueSupportedOperations : SupportedOperations<Issue>
    {
        public IssueSupportedOperations(NancyContextWrapper context)
        {
            this.Class.SupportsGet().TypedAs((IriRef)Schema.DiscoverAction);

            if (context.Current.CurrentUser?.IsInRole("Admin") == true)
            {
                this.Class.SupportsDelete();
            }

            this.Property(issue => issue.ProjectId).SupportsGet();
        }
    }
}