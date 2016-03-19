using JetBrains.Annotations;
using JsonLD.Entities;
using Newtonsoft.Json;
using NullGuard;
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
        public IriRef Range { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { [return: AllowNull] get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Rdf.Property; }
        }
    }
}