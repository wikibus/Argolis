using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NullGuard;

namespace Hydra.Core
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
            SupportedOperations = new System.Collections.ObjectModel.Collection<Operation>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Property"/> is required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property can be read.
        /// </summary>
        public bool Readable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property can be written.
        /// </summary>
        public bool Writeable { get; set; }

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

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Hydra.SupportedProperty; }
        }
    }
}
