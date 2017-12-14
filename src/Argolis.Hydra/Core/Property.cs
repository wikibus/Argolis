using System.Collections.Generic;
using JetBrains.Annotations;
using JsonLD.Entities;
using Newtonsoft.Json;
using NullGuard;
using Vocab;

namespace Argolis.Hydra.Core
{
    /// <summary>
    /// Represents an RDF property
    /// </summary>
    public class Property
    {
        private readonly ISet<string> types = new HashSet<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        public Property()
        {
            this.SupportedOperations = new List<Operation>();

            this.types.Add(Rdf.Property);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="extraTypes">Types in addition to rdf:Property</param>
        public Property(IEnumerable<string> extraTypes)
            : this()
        {
            this.types.UnionWith(extraTypes);
        }

        /// <summary>
        /// Gets or sets the RDFS range.
        /// </summary>
        [JsonProperty(Rdfs.range)]
        public IriRef Range { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the supported operations.
        /// </summary>
        [JsonProperty(Vocab.Hydra.supportedOperation)]
        public ICollection<Operation> SupportedOperations { get; set; }

        /// <summary>
        /// Gets the property types
        /// </summary>
        public IEnumerable<string> Types => this.types;
    }
}
