using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NullGuard;

namespace Hydra.Core
{
    /// <summary>
    /// A Hydra class
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class Class : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Class"/> class.
        /// </summary>
        /// <param name="typeId">The class URI.</param>
        public Class(string typeId)
        {
            Id = typeId;
        }

        /// <summary>
        /// Gets or sets the Class' identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the supported operations.
        /// </summary>
        [JsonProperty(Hydra.supportedOperation)]
        public IEnumerable<Operation> SupportedOperations { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the supported properties.
        /// </summary>
        [JsonProperty(Hydra.supportedProperty)]
        public IEnumerable<Property> SupportedProperties { [return: AllowNull] get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Hydra.Class; }
        }
    }
}
