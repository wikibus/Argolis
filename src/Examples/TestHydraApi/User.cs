using Hydra.Annotations;
using Newtonsoft.Json;
using Vocab;

namespace TestHydraApi
{
    [SupportedClass("http://example.api/o#User")]
    public class User
    {
        [JsonProperty(Foaf.givenName)]
        public string Name { get; set; }
        
        [JsonProperty(Foaf.lastName)]
        public string LastName { get; set; }
    }
}