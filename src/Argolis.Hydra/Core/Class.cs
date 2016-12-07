using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Argolis.Hydra.Core
{
    /// <summary>
    /// A Hydra class
    /// </summary>
    public class Class : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Class"/> class.
        /// </summary>
        /// <param name="typeId">The class URI.</param>
        public Class(Uri typeId)
        {
            this.Id = typeId;
            this.SupportedOperations = new Operation[0];
            this.SupportedProperties = new SupportedProperty[0];
        }

        /// <summary>
        /// Gets or sets the Class' identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the supported operations.
        /// </summary>
        [JsonProperty(Vocab.Hydra.supportedOperation)]
        public ICollection<Operation> SupportedOperations { get; set; }

        /// <summary>
        /// Gets or sets the supported properties.
        /// </summary>
        [JsonProperty(Vocab.Hydra.supportedProperty)]
        public ICollection<SupportedProperty> SupportedProperties { get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Vocab.Hydra.Class; }
        }
    }
}
