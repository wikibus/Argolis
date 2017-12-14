using Argolis.Hydra.Core;
using Argolis.Hydra.Resources;
using Newtonsoft.Json;

namespace TestHydraApi
{
    public class IssueCollection : Collection<Issue>
    {
        [JsonProperty(Vocab.Hydra.BaseUri + "search")]
        public IriTemplate Search { get; set; }
    }
}