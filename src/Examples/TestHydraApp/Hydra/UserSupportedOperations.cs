using Argolis.Hydra.Discovery.SupportedOperations;
using TestHydraApi;

namespace TestHydraApp.Hydra
{
    public class UserSupportedOperations : SupportedOperations<User>
    {
        public UserSupportedOperations()
        {
            this.Class.SupportsGet();
        }
    }
}