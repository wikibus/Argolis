using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Hydra.Core
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
            Id = typeId;
            SupportedOperations = new Operation[0];
            SupportedProperties = new SupportedProperty[0];
        }

        /// <summary>
        /// Gets or sets the Class' identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the supported operations.
        /// </summary>
        [JsonProperty(Hydra.supportedOperation)]
        public IEnumerable<Operation> SupportedOperations { get; set; }

        /// <summary>
        /// Gets or sets the supported properties.
        /// </summary>
        [JsonProperty(Hydra.supportedProperty)]
        public IEnumerable<SupportedProperty> SupportedProperties { get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Hydra.Class; }
        }
    }
}
