using Argolis.Models;

namespace TestNancyApp.Modules
{
    public class BaseProvider : IBaseUriProvider
    {
        public string BaseResourceUri => "http://localhost:61186/";
    }
}