using Argolis.Models;

namespace TestNancyApp.Hydra
{
    public class BaseProvider : IBaseUriProvider
    {
        public string BaseResourceUri => "http://localhost:61186/";
    }
}