using Hydra.Discovery.SupportedOperations;
using TestHydraApi;

namespace TestNancyApp.Hydra
{
    public class UserSupportedOperations : SupportedOperations<User>
    {
        public UserSupportedOperations()
        {
            Class.SupportsGet();
        }
    }
}