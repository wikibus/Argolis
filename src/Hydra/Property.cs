using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using NullGuard;

namespace Hydra
{
    /// <summary>
    /// A Hydra property
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class Property
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        public Property()
        {
            SupportedOperations = new Collection<Operation>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Property"/> is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [read only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read only]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("readonly")]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [write only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [write only]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("writeonly")]
        public bool WriteOnly { get; set; }

        /// <summary>
        /// Gets the supported operations.
        /// </summary>
        [JsonProperty("supportedOperation")]
        public ICollection<Operation> SupportedOperations { get; private set; }

        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        [JsonProperty("property")]
        public string Predicate { get; set; }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        public string Range { get; set; }
    }
}
