using Hydra.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vocab;

namespace TestHydraApi
{
    [SupportedClass("http://example.api/o#User")]
    public class User
    {
        [JsonProperty("firstName")]
        public string Name { get; set; }
        
        public string LastName { get; set; }
        
        [JsonProperty("with_attribute")]
        public string NotInContextWithAttribute { get; set; }

        public static JObject Context
        {
            get
            {
                return new JObject(
                    "firstName".IsProperty(Foaf.givenName),
                    "lastName".IsProperty(Foaf.lastName)
                    );
            }
        }
    }
}