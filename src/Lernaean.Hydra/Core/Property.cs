using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using JsonLD.Entities;
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
        [JsonProperty(Hydra.required)]
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property can be read.
        /// </summary>
        [JsonProperty(Hydra.readable)]
        public bool Readable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property can be written.
        /// </summary>
        [JsonProperty(Hydra.writeable)]
        public bool Writeable { get; set; }

        /// <summary>
        /// Gets the supported operations.
        /// </summary>
        [JsonProperty(Hydra.supportedOperation)]
        public ICollection<Operation> SupportedOperations { get; private set; }

        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        [JsonProperty(Hydra.property)]
        public IriRef Predicate { get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Hydra.SupportedProperty; }
        }
    }
}
