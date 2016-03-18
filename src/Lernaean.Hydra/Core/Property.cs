using JsonLD.Entities;
using Newtonsoft.Json;
using Vocab;

namespace Hydra.Core
{
    /// <summary>
    /// Represents an RDF property for the purpose of documenting <see cref="SupportedProperty"/>
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Gets or sets the RDFS range.
        /// </summary>
        [JsonProperty(Rdfs.range)]
        public IriRef Range { get; set; }
    }
}