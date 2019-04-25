using Argolis.Models;

namespace TestHydraApp.Hydra
{
    public class BaseProvider : IBaseUriProvider
    {
        public string BaseResourceUri => "http://localhost:61186/";
    }
}